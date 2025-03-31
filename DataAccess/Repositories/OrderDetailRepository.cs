using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly eStoreDuckContext _context;

    public OrderDetailRepository(eStoreDuckContext context)
    {
        _context = context;
    }

    public async Task<List<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
    {
        return await _context.OrderDetails
            .Where(od => od.OrderId == orderId)
            .ToListAsync();
    }

    public async Task<OrderDetail> GetOrderDetailAsync(int orderId, int productId)
    {
        return await _context.OrderDetails
            .FirstOrDefaultAsync(od => od.OrderId == orderId && od.ProductId == productId);
    }

    public async Task AddOrderDetailAsync(OrderDetail orderDetail)
    {
        await _context.OrderDetails.AddAsync(orderDetail);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
    {
        _context.OrderDetails.Update(orderDetail);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderDetailAsync(int orderId, int productId)
    {
        var detail = await GetOrderDetailAsync(orderId, productId);
        if (detail != null)
        {
            _context.OrderDetails.Remove(detail);
            await _context.SaveChangesAsync();
        }
    }
}
