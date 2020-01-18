using AutoMapper;
using PO_sklep.DTO;
using PO_sklep.Models;
using PO_sklep.Repositories.Interfaces;
using PO_sklep.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PO_sklep.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IClientRepository clientRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Creates new order for client identified by provided id asynchronously
        /// </summary>
        /// <param name="clientId">Id representing client</param>
        /// <param name="order">New order data</param>
        /// <returns>Id of newly created order or null if creation failed</returns>
        /// <exception cref="ArgumentNullException">Thrown if order is null</exception>
        public async Task<int?> CreateOrderAsync(int clientId, OrderDto order)
        {
            if(order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var orderItems = _mapper.Map<IEnumerable<OrderItem>>(order.OrderItems);
            try
            {
                return order.PaymentTypeId is { }
                    ? await _orderRepository.CreateOrderAsync(
                        clientId,
                        order.DeliveryMethodId,
                        order.PaymentTypeId.Value,
                        orderItems).ConfigureAwait(false)
                    : await _orderRepository.CreateOrderAsync(
                        clientId,
                        order.DeliveryMethodId,
                        orderItems).ConfigureAwait(false);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        /// <summary>
        /// Creates new order for client identified by email asynchronously. If client with this email does not exist, it will be created
        /// </summary>
        /// <param name="clientEmail">Email identifying client</param>
        /// <param name="order">Order data</param>
        /// <returns>Id of newly created order or null if creation failed</returns>
        public async Task<int?> CreateOrderAsync(string clientEmail, OrderDto order)
        {
            if(string.IsNullOrWhiteSpace(clientEmail))
            {
                throw new ArgumentNullException(nameof(clientEmail));
            }

            var client = await _clientRepository.GetClientByEmail(clientEmail).ConfigureAwait(false);
            int clientId = client is null
                ? await _clientRepository.CreateClientAsync(clientEmail).ConfigureAwait(false)
                : client.Id;
            return await CreateOrderAsync(clientId, order).ConfigureAwait(false);
        }
    }
}
