using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Queries.GetOrders;
using Application.Parameters;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<PagedList<Order>> GetPagedListAsync(GetOrdersQuery ordersQuery);

        Task<Order> GetByIdAsync(Guid id);
        Task<bool> ExistAsync(Order order);

        Task CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task UpdateAsync(IEnumerable<Order> orders);
        Task DeleteAsync(Order order);
    }
}
