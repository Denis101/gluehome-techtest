using System.Collections.Generic;

namespace GlueHome.Api.Mysql
{
  public interface IMysqlDataClient
  {
    T ExecuteQuery<T>(string query, IDataMapper<T> mapper);
    T ExecuteQuery<T>(string query, Dictionary<string, dynamic> parameters, IDataMapper<T> mapper);
  }
}