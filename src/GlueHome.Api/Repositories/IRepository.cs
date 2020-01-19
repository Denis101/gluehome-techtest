using GlueHome.Api.Mysql;

namespace GlueHome.Api.Repositories
{
    public interface IRepository<T>
    {
        IDataMapper<T> Mapper { get; }
    }
}