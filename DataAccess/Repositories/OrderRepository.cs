using BusinessObject.Entities;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly eStoreDuckContext _context;
        public OrderRepository(eStoreDuckContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(Order order)
        {
           await _context.Orders.AddAsync(order);
          await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                  .Include(o => o.OrderDetails) 
                  .Include(o => o.Member)       
                  .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsyn(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.Member)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Order>> GetOrdersByMemberIdAsync(int memberId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.Member)
                .Where(o => o.MemberId == memberId)
                .ToListAsync();
        }


    }
}
