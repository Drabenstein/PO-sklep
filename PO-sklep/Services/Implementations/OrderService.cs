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

        public async Task<int?> CreateOrderAsync(int clientId, OrderDto order)
        {
            var orderItems = _mapper.Map<IEnumerable<OrderItem>>(order.OrderItems);
            try
            {
                return order.PaymentTypeId is { }
                    ? await _orderRepository.CreateOrderAsync(
                        clientId,
                        order.DeliveryMethodId,
                        order.PaymentTypeId.Value,
                        orderItems)
                    : await _orderRepository.CreateOrderAsync(
                        clientId,
                        order.DeliveryMethodId,
                        orderItems);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public async Task<int?> CreateOrderAsync(string clientEmail, OrderDto order)
        {
            if(string.IsNullOrWhiteSpace(clientEmail))
            {
                throw new ArgumentNullException(nameof(clientEmail));
            }

            var client = await _clientRepository.GetClientByEmail(clientEmail);
            int clientId = client is null
                ? await _clientRepository.CreateClientAsync(clientEmail)
                : client.Id;
            return await CreateOrderAsync(clientId, order);
        }
    }
}
