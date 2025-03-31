using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace BusinessObject.Services
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId);
        Task<OrderDetail> GetOrderDetailAsync(int orderId, int productId);
        Task AddOrderDetailAsync(OrderDetail orderDetail);
        Task UpdateOrderDetailAsync(OrderDetail orderDetail);
        Task DeleteOrderDetailAsync(int orderId, int productId);
    }
}
