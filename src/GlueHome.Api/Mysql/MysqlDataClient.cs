using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace GlueHome.Api.Mysql
{
    public class MysqlDataClient : IMysqlDataClient, IDisposable
  {
    private readonly MySqlConnection _connection;

    public MysqlDataClient() {
      _connection = new MySqlConnection("CONNECTION_STRING_YO");
      _connection.Open();
    }

    public T ExecuteQuery<T>(string query, IDataMapper<T> mapper)
    {
      return ExecuteCommand(new MySqlCommand(query, _connection), mapper);
    }

    public T ExecuteQuery<T>(string query, Dictionary<string, dynamic> parameters, IDataMapper<T> mapper)
    {
      var cmd = new MySqlCommand(query, _connection);
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
      _connection.Close();
    }
  }
}