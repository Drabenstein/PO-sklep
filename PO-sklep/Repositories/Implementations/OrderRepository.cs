using Dapper;
using PO_sklep.Helpers;
using PO_sklep.Models;
using PO_sklep.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PO_sklep.Repositories.Implementations
{
    public class OrderRepository : GenericRepositoryBase<Zamowienie>, IOrderRepository
    {
        private const string SubmitOrderUsp = "uspSubmitOrder";

        public OrderRepository(ConnectionConfig connectionConfig) : base(connectionConfig) { }

        public async Task<int> CreateOrderAsync(int clientId, int deliveryMethodId, IEnumerable<ZamowienieProdukt> orderItems)
        {
            var param = CreateParameters(clientId, deliveryMethodId, null, orderItems);
            return await CreateOrderHelperAsync(param);
        }

        public async Task<int> CreateOrderAsync(int clientId, int deliveryMethodId, int paymentTypeId, IEnumerable<ZamowienieProdukt> orderItems)
        {
            var param = CreateParameters(clientId, deliveryMethodId, paymentTypeId, orderItems);
            return await CreateOrderHelperAsync(param);
        }

        private async Task<int> CreateOrderHelperAsync(DynamicParameters param)
        {
            try
            {
                var id = await QueryAsync<int>(async db =>
                {
                    var id = await db.QueryAsync<int>(SubmitOrderUsp,
                        param,
                        commandType: CommandType.StoredProcedure);
                    return id.SingleOrDefault();
                });
                return id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(nameof(CreateOrderAsync), ex);
            }
        }

        private DynamicParameters CreateParameters(int clientId, int deliveryMethodId, int? paymentTypeId, IEnumerable<ZamowienieProdukt> orderItems)
        {
            var param = new DynamicParameters();
            param.Add("@Id_klienta", clientId);
            param.Add("@Id_sposobu_dostawy", deliveryMethodId);

            if(paymentTypeId is { })
            {
                param.Add("@Id_typu_platnosci", paymentTypeId);
            }

            var dt = new DataTable();
            dt.Columns.Add("Id_produktu", typeof(int));
            dt.Columns.Add("Ilosc", typeof(int));

            foreach(var orderItem in orderItems)
            {
                dt.Rows.Add(orderItem.IdProduktu, orderItem.Ilosc);
            }

            param.Add("@Lista", dt.AsTableValuedParameter());

            return param;
        }
    }
}
