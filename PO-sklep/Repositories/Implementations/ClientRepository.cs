using Dapper;
using PO_sklep.Helpers;
using PO_sklep.Models;
using PO_sklep.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace PO_sklep.Repositories.Implementations
{
    public class ClientRepository : GenericRepositoryBase<Client>, IClientRepository
    {
        private const string AddClientUsp = "uspAddClient";
        private const string AddClientByEmailUsp = "uspAddClientByEmail";
        private const string GetClientByEmailUsp = "uspGetClientByEmail";
        private const string GetClientByIdUsp = "uspGetClientById";

        public ClientRepository(ConnectionConfig connectionConfig) : base(connectionConfig) { }

        public async Task<int> CreateClientAsync(string name, string surname, string email, string address, DateTime? birthday)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(surname))
            {
                throw new ArgumentNullException(nameof(surname));
            }

            var param = new DynamicParameters();
            param.Add("@Imie_klienta", name);
            param.Add("@Nazwisko_klienta", surname);

            if (email is { })
            {
                param.Add("@Email", email);
            }

            if (address is { })
            {
                param.Add("@Adres", address);
            }

            if (birthday is { })
            {
                param.Add("@Data_urodzenia", birthday);
            }

            try
            {
                return await QueryAsync<int>(async db =>
                    {
                        return await db.ExecuteScalarAsync<int>(AddClientUsp,
                            param,
                            commandType: System.Data.CommandType.StoredProcedure);
                    });
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(nameof(CreateClientAsync), ex);
            }
        }

        public async Task<int> CreateClientAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            var param = new DynamicParameters();
            param.Add("@Email", email);

            try
            {
                return await QueryAsync<int>(async db =>
                {
                    return await db.ExecuteScalarAsync<int>(AddClientByEmailUsp,
                        param,
                        commandType: System.Data.CommandType.StoredProcedure);
                });
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(nameof(CreateClientAsync), ex);
            }
        }

        public async Task<Client> GetClientByEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            var param = new DynamicParameters();
            param.Add("@Email", email);

            return await QueryAsync<Client>(async db =>
            {
                return await db.QueryFirstOrDefaultAsync<Client>(GetClientByEmailUsp,
                    param,
                    commandType: System.Data.CommandType.StoredProcedure);
            });
        }

        public async Task<Client> GetClientById(int clientId)
        {
            var param = new DynamicParameters();
            param.Add("@Id_klienta", clientId);

            return await QueryAsync<Client>(async db =>
            {
                return await db.QueryFirstOrDefaultAsync<Client>(GetClientByIdUsp,
                    param,
                    commandType: System.Data.CommandType.StoredProcedure);
            });
        }
    }
}