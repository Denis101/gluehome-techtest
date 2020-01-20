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

        public MysqlContext(ILogger<MysqlContext> logger, string connectionString) {
            connection = new MySqlConnection(connectionString);
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

        public IEnumerable<T> ExecuteQueryBatch<T>(string query, IDataMapper<T> mapper)
        {
            return ExecuteCommandBatch(new MySqlCommand(query, connection), mapper);
        }

        public IEnumerable<T> ExecuteQueryBatch<T>(string query, Dictionary<string, dynamic> parameters, IDataMapper<T> mapper)
        {
            var cmd = new MySqlCommand(query, connection);
            foreach (KeyValuePair<string, dynamic> kv in parameters) {
                cmd.Parameters.AddWithValue(kv.Key, kv.Value);
            }

            return ExecuteCommandBatch(cmd, mapper);
        }

        private IEnumerable<T> ExecuteCommandBatch<T>(MySqlCommand command, IDataMapper<T> mapper)
        {
            using (var reader = command.ExecuteReader())
            {
                do
                {
                    yield return mapper.map(reader);
                } 
                while (reader.NextResult());
            }
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}