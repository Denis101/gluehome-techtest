using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace GlueHome.Api.Mysql
{
    public class MysqlContext : IMysqlContext, IDisposable
    {
        private readonly ILogger<MysqlContext> logger;
        private readonly MySqlConnection connection;

        public MysqlContext(ILogger<MysqlContext> logger) {
            connection = new MySqlConnection("CONNECTION_STRING_YO");
            connection.Open();
        }

        public T ExecuteQuery<T>(string query, IDataMapper<T> mapper)
        {
            return ExecuteCommand(new MySqlCommand(query, connection), mapper);
        }

        public T ExecuteQuery<T>(string query, Dictionary<string, dynamic> parameters, IDataMapper<T> mapper)
        {
            var cmd = new MySqlCommand(query, connection);
            foreach (KeyValuePair<string, dynamic> kv in parameters) {
                cmd.Parameters.AddWithValue(kv.Key, kv.Value);
            }

            return ExecuteCommand(cmd, mapper);
        }

        private T ExecuteCommand<T>(MySqlCommand command, IDataMapper<T> mapper)
        {
            using (var reader = command.ExecuteReader())
            {
                return mapper.map(reader);
            }
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}