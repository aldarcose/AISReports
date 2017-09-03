using Npgsql;
using SharedDbWorker.Classes;
using System;
using System.Collections.Generic;
using System.Data;

namespace SharedDbWorker
{
    public class Connection : IDisposable
    {
        // Соединение
        private NpgsqlConnection conn;
        // Транзакция
        private NpgsqlTransaction tran;
        // Параметры соединения
        private ConnectionParameters parameters;

        public Connection(ConnectionParameters parameters)
        {
            this.conn = MakeConnection(parameters.ConnectionString);
            this.parameters = parameters;
            
            OpenConnection();
        }

        private bool IsConnectionAvailable()
        {
            return conn != null && conn.State != ConnectionState.Closed;
        }

        private void OpenConnection()
        {
            if (IsConnectionAvailable())
                return;

            if (conn == null)
                conn = MakeConnection(parameters.ConnectionString);
            conn.Open();
        }

        private NpgsqlConnection MakeConnection(string connectionString)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            return connection;
        }

        public List<DbResult> GetResults(DbQuery query, IProgressControl pc = null)
        {
            var results = new List<DbResult>();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = query.Sql;

                if (pc != null)
                {
                    int total = GetCountObjects(query.Sql);
                    pc.SetMaximum(total);
                    pc.SetStatus("Идет выполнение запроса...");
                }

                int index = 0;
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
                            Dispose();
                            throw ex;
                        }
                    }

                    index++;
                    if (pc != null) pc.SetProgress(index);
                    results.Add(res);
                }
            }

            return results;
        }

        public object GetScalarResult(DbQuery query)
        {
            object result = null;
            using (var cmd = conn.CreateCommand())
            {
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
                    Dispose();
                    throw ex;
                }
            }

            return result;
        }

        public void ExecuteStatement(DbQuery query)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = query.Sql;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Dispose();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Получить количество объектов в запросе
        /// </summary>
        /// <param name="querySql">Запрос</param>
        /// <returns></returns>
        private int GetCountObjects(string querySql)
        {
            int result = 0;
            using (IDbCommand getCountCmd = conn.CreateCommand())
            {
                int indexOfOrderBy = querySql.LastIndexOf("order by", StringComparison.InvariantCultureIgnoreCase);
                if (indexOfOrderBy > 0)
                    querySql = querySql.Substring(0, indexOfOrderBy);
                getCountCmd.CommandText = string.Format("select count(*) from ({0}) t", querySql);
                result = Convert.ToInt32(getCountCmd.ExecuteScalar());
            }
            return result;
        }

        /// <summary>
        /// Удалить ресурс и очистить пул подключений, связанный с заданным подключением
        /// </summary>
        private void DisposeAndClearPool()
        {
            // Удаление транзакции
            if (tran != null)
            {
                tran.Dispose();
                tran = null;
            }

            // Удаление соединения
            conn.ClearPool();
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        public void Dispose()
        {
            DisposeAndClearPool();
        }
    }

    /// <summary>
    /// Параметры соединения
    /// </summary>
    public class ConnectionParameters
    {
        private static readonly ConnectionParameters instance = GetConnectionParameters();
        public static ConnectionParameters Instance
        {
            get { return instance; }
        }

        private string login;
        private string password;
        private string host;
        private string port;

        // Строка соединения
        private string connectionString;

        public string Login
        {
            get { return login; }
        }

        internal string Password
        {
            get { return password; }
        }

        public string Host
        {
            get { return host; }
        }

        public string Port
        {
            get { return port; }
        }

        public string ConnectionString
        {
            get { return connectionString; }
        }

        private static ConnectionParameters GetConnectionParameters()
        {
            var host = AppSettings.Get("DbAddr");
            var port = AppSettings.Get("DbPort");
            var login = AppSettings.Get("DbLogin");
            var password = AppSettings.Get("DbPassword");

            return new ConnectionParameters((string)host, (string)port, (string)login, (string)password);
        }

        private ConnectionParameters(string host, string port, string login, string password)
        {
            this.host = host;
            this.port = port;
            this.login = login;
            this.password = password;

            this.connectionString = String.Format(
                @"Server={0};Port={1};User Id={2};Password={3};Database=ais_buryat;Pooling=true;MinPoolSize=1;MaxPoolSize=50;ApplicationName=plugins;Timeout=30;CommandTimeout=0;",
                host, port, login, password);
        }
    }
}
