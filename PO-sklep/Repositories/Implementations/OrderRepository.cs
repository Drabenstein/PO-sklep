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
    public class OrderRepository : GenericRepositoryBase<Order>, IOrderRepository
    {
        private const string SubmitOrderUsp = "uspSubmitOrder";

        public OrderRepository(ConnectionConfig connectionConfig) : base(connectionConfig) { }

        /// <summary>
        /// Creates order asynchronously
        /// </summary>
        /// <param name="clientId">Client's id</param>
        /// <param name="deliveryMethodId">Order delivery method id</param>
        /// <param name="orderItems">Order items</param>
        /// <returns>Id of newly created order</returns>
        /// <exception cref="ArgumentNullException">Thrown if order items is null</exception>
        public async Task<int> CreateOrderAsync(int clientId, int deliveryMethodId, IEnumerable<OrderItem> orderItems)
        {
            if(orderItems is null)
            {
                throw new ArgumentNullException(nameof(orderItems));
            }

            var param = CreateParameters(clientId, deliveryMethodId, null, orderItems);
            return await CreateOrderHelperAsync(param).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates order asynchronously
        /// </summary>
        /// <param name="clientId">Client's id</param>
        /// <param name="deliveryMethodId">Order delivery method id</param>
        /// <param name="paymentTypeId">Payment type id</param>
        /// <param name="orderItems">Order items</param>
        /// <returns>Id of newly created order</returns>
        /// <exception cref="ArgumentNullException">Thrown if order items is null</exception>
        public async Task<int> CreateOrderAsync(int clientId, int deliveryMethodId, int paymentTypeId, IEnumerable<OrderItem> orderItems)
        {
            if (orderItems is null)
            {
                throw new ArgumentNullException(nameof(orderItems));
            }

            var param = CreateParameters(clientId, deliveryMethodId, paymentTypeId, orderItems);
            return await CreateOrderHelperAsync(param).ConfigureAwait(false);
        }

        private async Task<int> CreateOrderHelperAsync(DynamicParameters param)
        {
            try
            {
                var id = await QueryAsync<int>(async db =>
                {
                    var id = await db.QueryAsync<int>(SubmitOrderUsp,
                        param,
                        commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                    return id.SingleOrDefault();
                }).ConfigureAwait(false);
                return id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(nameof(CreateOrderAsync), ex);
            }
        }

        private DynamicParameters CreateParameters(int clientId, int deliveryMethodId, int? paymentTypeId, IEnumerable<OrderItem> orderItems)
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
                dt.Rows.Add(orderItem.ProductId, orderItem.Count);
            }

            param.Add("@Lista", dt.AsTableValuedParameter());

            return param;
        }
    }
}
