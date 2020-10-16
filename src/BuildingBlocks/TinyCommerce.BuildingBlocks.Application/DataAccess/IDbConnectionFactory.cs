using System.Data;

namespace TinyCommerce.BuildingBlocks.Application.DataAccess
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
