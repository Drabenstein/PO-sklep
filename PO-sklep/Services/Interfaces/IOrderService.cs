using PO_sklep.DTO;
using System.Threading.Tasks;

namespace PO_sklep.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int?> CreateOrderAsync(int clientId, OrderDto order);
        Task<int?> CreateOrderAsync(string clientEmail, OrderDto order);
    }
}
