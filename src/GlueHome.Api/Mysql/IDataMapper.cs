using MySql.Data.MySqlClient;

namespace GlueHome.Api.Mysql
{
  public interface IDataMapper<T>
  {
    T map(MySqlDataReader reader);
  }
}