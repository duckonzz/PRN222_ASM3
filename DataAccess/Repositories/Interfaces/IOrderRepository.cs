using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsyn(int orderId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
        Task<List<Order>> GetOrdersByMemberIdAsync(int memberId);
    }
}
