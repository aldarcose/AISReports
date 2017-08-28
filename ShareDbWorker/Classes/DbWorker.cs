using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using SharedDbWorker.Classes;
using NLog;
using Dapper;
using System.Threading;
using System.Text.RegularExpressions;

namespace SharedDbWorker
{
    public class DbWorker : IDbWorker, IDisposable
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        //public const string AisConnectionString =
            //"Server=192.168.16.187;Port=9999;User Id=munkokizh;Password=15101986;Database=ais_buryat;Pooling=true;ApplicationName=plugins;Timeout=30";
        public const string Gp1rbConnectionString =
            "Server=192.168.16.253;Port=5432;User Id=munkokizh;Password=15101986;Database=gp1rb;ApplicationName=plugins;";
        

        private static string _connectionString = BuildConnectionString();
        
        private static string BuildConnectionString()
        {
            var dbAddr = AppSettings.Get("DbAddr");
            var dbPort = AppSettings.Get("DbPort");
            if (dbAddr!=null && dbPort!=null)
                return string.Format("Server={0};Port={1};User Id=munkokizh;Password=15101986;Database=ais_buryat;Pooling=true;MinPoolSize=1;MaxPoolSize=50;ApplicationName=plugins;Timeout=30;CommandTimeout=0;", dbAddr, dbPort);
                //return string.Format("Server={0};Port={1};User Id=postgres;Password=postgres;Database=ais;Pooling=true;MinPoolSize=1;MaxPoolSize=50;ApplicationName=plugins;Timeout=30", dbAddr, dbPort);
                //return string.Format("Server={0};Port={1};User Id=munkokizh;Password=15101986;Database=ais;Pooling=true;MinPoolSize=1;MaxPoolSize=50;ApplicationName=plugins;Timeout=30;CommandTimeout=0;", dbAddr, dbPort);
            else
                return "Server=192.168.16.253;Port=5432;User Id=munkokizh;Password=15101986;Database=ais_buryat;Pooling=true;MinPoolSize=1;MaxPoolSize=50;ApplicationName=plugins;Timeout=30;CommandTimeout=0;";
                //return string.Format("Server={0};Port={1};User Id=postgres;Password=postgres;Database=ais;Pooling=true;MinPoolSize=1;MaxPoolSize=50;ApplicationName=plugins;Timeout=30", "127.0.0.1", "5432");
                //return string.Format("Server={0};Port={1};User Id=munkokizh;Password=15101986;Database=ais;Pooling=true;MinPoolSize=1;MaxPoolSize=50;ApplicationName=plugins;Timeout=30;CommandTimeout=0;", "192.168.16.253", "5432");
        }
        
        public static string ConnectionString
        {
            get { return _connectionString; }
        }

        public IDbConnection Connection
        {
            get {
                return Open() ? _conn : null;
            }
        }

        public DbWorker()
        { 
        }

        public DbWorker(string connectionString) : this()
        {
            _connectionString = connectionString;
        }

        private NpgsqlConnection _conn;

        public int GetUserId(string login, string password)
        {
            var id = -1;
            var expirationDate = DateTime.MinValue;
            if (!Open())
            {
                throw new AuthException("Невозможно открыть подключение к БД!") ;
            }

            try
            {

                long? result = _conn.ExecuteScalar<long?>("Select get_user_id(@Login, @Password)", new { Login = login, Password = password });


                if (result == null)
                {
                    Close();
                    throw new AuthException("Неправильный логин или пароль!") { Login = login };
                }

                    
                DateTime? expDate = _conn.ExecuteScalar<DateTime?>("SELECT expiration_date FROM users_tab WHERE id = @Id", new {Id = result} );

                if (!expDate.HasValue || expDate.Value < DateTime.Now)
                {
                    Close();
                    throw new AuthException("Срок действия данного логина истек!") { Login = login };
                }
                return (int)result.Value;
            }finally
            {
                Close();
            }
        }

        /// <summary>
        /// Исполняет SQL
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <returns>Кол-во зайдествованных строк</returns>
        public int Execute(DbQuery query)
        {
            int handled = -1;
            if (Open())
            {
                using (var cmd = _conn.CreateCommand())
                {
                    //cmd.AllResultTypesAreUnknown = true;
                    cmd.CommandText = query.Sql;
                    foreach (var commandParam in query.CommandParams)
                    {
                        cmd.Parameters.AddWithValue(commandParam.Key, commandParam.Value);
                    }
                    try
                    {
                        handled = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        _logger.Debug("SQL Error: " + ex.Message);
                        Close();
                        throw ex;
                    }
                    
                }
                Close();
            }
            return handled;
        }

        public object GetScalarResult(DbQuery query)
        {
            object result = null;
            if (Open())
            {
                using (var cmd = _conn.CreateCommand())
                {
                    //cmd.AllResultTypesAreUnknown = true;
                    cmd.CommandText = query.Sql;
                    foreach (var commandParam in query.CommandParams)
                    {
                        cmd.Parameters.AddWithValue(commandParam.Key, commandParam.Value);
                    }
                    try
                    {
                        result = cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Close();
                        _logger.Debug("SQL Error: " + ex.Message);
                        return null;
                    }
                    
                }
                Close();
            }
            return result;
        }

        /// <summary>
        /// Gets first row
        /// </summary>
        public DbResult GetResult(DbQuery query)
        {
            DbResult result = null;
            if (Open())
            {
                using (var cmd = _conn.CreateCommand())
                {
                    //cmd.AllResultTypesAreUnknown = true;
                    cmd.CommandText = query.Sql;
                    foreach (var commandParam in query.CommandParams)
                    {
                        cmd.Parameters.AddWithValue(commandParam.Key, commandParam.Value);
                    }

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = new DbResult();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            try
                            {
                                result.Fields.Add(reader[i]);
                                result.FieldNames.Add(reader.GetName(i));
                            }
                            catch (Exception ex)
                            {
                                Close();
                                result.Fields.Add(null);
                                result.FieldNames.Add(reader.GetName(i));
                                _logger.Debug("SQL Error: " + ex.Message);
                            }
                        }
                    }
                }
                Close();
            }
            return result;
        }

        /// <summary>
        /// Получить количество объектов в запросе
        /// </summary>
        /// <param name="querySql">Запрос</param>
        /// <returns></returns>
        private int GetCountObjects(string querySql)
        {
            int result = 0;
            using (IDbCommand getCountCmd = _conn.CreateCommand())
            {
                int indexOfOrderBy = querySql.LastIndexOf("order by", StringComparison.InvariantCultureIgnoreCase);
                if (indexOfOrderBy > 0)
                    querySql = querySql.Substring(0, indexOfOrderBy);
                getCountCmd.CommandText = string.Format("select count(*) from ({0}) t", querySql);
                result = Convert.ToInt32(getCountCmd.ExecuteScalar());
            }
            return result;
        }

        public List<DbResult> GetResults(DbQuery query, IProgressControl pc = null)
        {
            var results = new List<DbResult>();
            if (Open())
            {
                using (var cmd = _conn.CreateCommand())
                {
                    //cmd.AllResultTypesAreUnknown = true;
                    cmd.CommandText = query.Sql;
                    foreach (var commandParam in query.CommandParams)
                    {
                        cmd.Parameters.AddWithValue(commandParam.Key, commandParam.Value);
                    }

                    if (pc != null)
                    {
                        int total = GetCountObjects(query.Sql);
                        pc.SetMaximum(total);
                        pc.SetStatus("Идет выполнение запроса...");
                    }

                    int index = 0;
                    _logger.Debug("Запрос: " + cmd.CommandText);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var res = new DbResult();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            try
                            {
                                res.Fields.Add(reader[i]);
                                res.FieldNames.Add(reader.GetName(i));
                            }
                            catch (Exception ex)
                            {
                                Close();
                                res.Fields.Add(null);
                                res.FieldNames.Add(reader.GetName(i));
                                _logger.Debug("SQL Error: " + ex.Message);
                            }
                        }

                        index++;
                        if (pc != null) pc.SetProgress(index);
                        results.Add(res);
                    }

                    _logger.Debug("Получено записей: " + index);
                }
                Close();
            }
            return results;
        }


        public bool Open()
        {
            _conn = new NpgsqlConnection(ConnectionString);
            bool error = false;
            try
            {
                _conn.Open();
            }
            catch (Exception ex)
            {
                _logger.Error("Cannot open connection: {0}", ex.Message);

                NpgsqlConnection.ClearAllPools();
                error = true;
            }

            if (error)
            {
                // Делаем 3 попытки реконнекта с интервалом в 10 секунд
                Thread.Sleep(3000);
                Exception exception = null;
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        error = false;
                        ReConnect();
                    }
                    catch(Exception ex)
                    {
                        error = true;
                        Thread.Sleep(10000);
                        exception = ex;
                    }
                }
                if (error) throw exception;
            }
            return true;
        }

        private void ReConnect()
        {
            Close();
            _conn = new NpgsqlConnection(ConnectionString);
            _conn.Open();
        }

        public void Close()
        {
            _conn.Close();
            _conn.ClearPool();
            _conn.Dispose();
        }

        public void Dispose()
        {

            if (_conn != null)
            {
                _conn.Close();
                _conn.Dispose();
                _conn.ClearPool();
            }
        }
    }

    public class AuthException : Exception
    {
        public AuthException() { }
        public AuthException(string message)
        {
            this.Error = message;
        }

        public string Error { get; set; }
        public string Login { get; set; }
    }
}
