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

        /// <summary>
        /// Creates client with specified data asynchronously
        /// </summary>
        /// <param name="name">Name of client</param>
        /// <param name="surname">Surname of client</param>
        /// <param name="email">Email of client</param>
        /// <param name="address">Delivery address of client</param>
        /// <param name="dayOfBirth">Day of birth of client</param>
        /// <returns>Id of newly created client</returns>
        /// <exception cref="InvalidOperationException">Thrown if client cannot be created</exception>
        /// <exception cref="ArgumentNullException">Thrown if name or surname is null or whitespace</exception>
        public async Task<int> CreateClientAsync(string name, string surname, string email, string address, DateTime? dayOfBirth)
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

            if (dayOfBirth is { })
            {
                param.Add("@Data_urodzenia", dayOfBirth);
            }

            try
            {
                return await QueryAsync<int>(async db =>
                    {
                        return await db.ExecuteScalarAsync<int>(AddClientUsp,
                            param,
                            commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
                    }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(nameof(CreateClientAsync), ex);
            }
        }

        /// <summary>
        /// Creates client with specified email asynchronously
        /// </summary>
        /// <param name="email">Email of client</param>
        /// <returns>Id of newly created client</returns>
        /// <exception cref="InvalidOperationException">Thrown if client cannot be created</exception>
        /// <exception cref="ArgumentNullException">Thrown if email is null or whitespace</exception>
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
                        commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(nameof(CreateClientAsync), ex);
            }
        }

        /// <summary>
        /// Gets client by specified email asynchronously
        /// </summary>
        /// <param name="email">Client's email</param>
        /// <returns></returns>
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
                    commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets client by specified id asynchronously
        /// </summary>
        /// <param name="clientId">Client's id</param>
        /// <returns></returns>
        public async Task<Client> GetClientById(int clientId)
        {
            var param = new DynamicParameters();
            param.Add("@Id_klienta", clientId);

            return await QueryAsync<Client>(async db =>
            {
                return await db.QueryFirstOrDefaultAsync<Client>(GetClientByIdUsp,
                    param,
                    commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
            }).ConfigureAwait(false);
        }
    }
}