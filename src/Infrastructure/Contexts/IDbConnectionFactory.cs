using System.Data;

namespace Infrastructure.Contexts;

public interface IDbConnectionFactory
{
    public Task<IDbConnection> CreateConnectionAsync();
}