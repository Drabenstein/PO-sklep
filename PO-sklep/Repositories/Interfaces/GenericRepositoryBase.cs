using PO_sklep.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PO_sklep.Repositories.Interfaces
{
    public abstract class GenericRepositoryBase<T> where T : class
    {
        protected ConnectionConfig _connectionConfig;

        protected GenericRepositoryBase(ConnectionConfig connectionConfig)
        {
            _connectionConfig = connectionConfig ?? throw new ArgumentNullException(nameof(connectionConfig));
        }

        protected async Task ExecuteAsync(Func<IDbConnection, Task> query)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            using var connection = new SqlConnection(_connectionConfig.ConnectionString);
            await query.Invoke(connection).ConfigureAwait(false);

        }

        protected async Task<T> QueryAsync<T>(Func<IDbConnection, Task<T>> query)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            using var connection = new SqlConnection(_connectionConfig.ConnectionString);
            return await query.Invoke(connection);
        }
    }
}
