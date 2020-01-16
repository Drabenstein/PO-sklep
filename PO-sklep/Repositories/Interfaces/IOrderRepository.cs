﻿using PO_sklep.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PO_sklep.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> CreateOrderAsync(int clientId, int deliveryMethodId, IEnumerable<ZamowienieProdukt> products);
        Task<int> CreateOrderAsync(int clientId, int deliveryMethodId, int paymentTypeId, IEnumerable<ZamowienieProdukt> products);
    }
}