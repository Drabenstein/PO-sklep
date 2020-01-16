using PO_sklep.Models;
using System;
using System.Threading.Tasks;

namespace PO_sklep.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<int> CreateClientAsync(string name, string surname, string email, string address, DateTime? birthday);
        Task<int> CreateClientAsync(string email);
        Task<Klient> GetClientById(int clientId);
        Task<Klient> GetClientByEmail(string email);
    }
}