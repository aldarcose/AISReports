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
    // todo: Рефакторинг. добавить текущее подключение.
    public class DbWorker : IDbWorker, IDisposable
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public const string Gp1rbConnectionString =
            "Server=192.168.16.253;Port=5432;User Id=munkokizh;Password=15101986;Database=gp1rb;ApplicationName=plugins;";
        
        private static string _connectionString = BuildConnectionString();
        private static List<NpgsqlConnection> lst;
        
        private static string BuildConnectionString()
        {
            var dbAddr = AppSettings.Get("DbAddr");
            var dbPort = AppSettings.Get("DbPort");
            if (dbAddr!=null && dbPort!=null)
                return string.Format("Server={0};Port={1};User Id=munkokizh;Password=15101986;Database=ais_buryat;Pooling=true;MinPoolSize=1;MaxPoolSize=50;ApplicationName=plugins;Timeout=30;CommandTimeout=0;", dbAddr, dbPort);
            else
                return "Server=192.168.16.253;Port=5432;User Id=munkokizh;Password=15101986;Database=ais_buryat;Pooling=true;MinPoolSize=1;MaxPoolSize=50;ApplicationName=plugins;Timeout=30;CommandTimeout=0;";
        }

        static DbWorker()
        {
            lst = new List<NpgsqlConnection>();
        }
        
        public static string ConnectionString
        {
            get { return _connectionString; }
        }

        public IDbConnection Connection
        {
            get 
            {
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

        public List<DbResult> GetResults(DbQuery query)
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
            try
            {
                _conn.Open();
            }
            catch (Exception ex)
            {
                _logger.Error("Cannot open connection: {0}", ex.Message);

                NpgsqlConnection.ClearAllPools();
                throw ex;
            }
            return true;
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
