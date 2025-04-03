using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Entities;
using DataAccess.DTO;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHubContext<ProductCategoryHub> _hub;

        public OrderService(IOrderRepository orderRepository, IHubContext<ProductCategoryHub> hub)
        {
            _orderRepository = orderRepository;
            _hub = hub;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            await _orderRepository.DeleteOrderAsync(orderId);
            await _hub.Clients.All.SendAsync("OrderDeleted", orderId);
        }
        public async Task<List<Order>> GetOrdersByMemberIdAsync(int memberId)
        {
            return await _orderRepository.GetOrdersByMemberIdAsync(memberId);
        }
        public async Task<List<SalesReportItemDTO>> GetSalesReportAsync(DateTime startDate, DateTime endDate)
        {
            return await _orderRepository.GetSalesReportAsync(startDate, endDate);
        }

        public async Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate)
        {
            return await _orderRepository.GetTotalSalesAsync(startDate, endDate);
        }

        public async Task<int> GetTotalOrdersAsync(DateTime startDate, DateTime endDate)
        {
            return await _orderRepository.GetTotalOrdersAsync(startDate, endDate);
        }
    }
}
