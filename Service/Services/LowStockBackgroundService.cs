﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.Services.Interfaces;
using DataAccess.DTO;

namespace Service.Services
{
    public class LowStockBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<LowStockBackgroundService> _logger;
        private readonly IHubContext<ProductCategoryHub> _hubContext;
        private const int Threshold = 10;
        private const int DelayInMinutes = 1; 

        private readonly HashSet<int> _alertedProductIds = new();

        public LowStockBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<LowStockBackgroundService> logger,
            IHubContext<ProductCategoryHub> hubContext)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                    var products = await productService.GetAllProductsWithCategoryAsync();

                    var lowStockProducts = products
                        .Where(p => p.UnitsInStock < Threshold)
                        .ToList();

                    foreach (var product in lowStockProducts)
                    {
                        if (!_alertedProductIds.Contains(product.ProductId))
                        {
                            var dto = new LowStockAlertDTO
                            {
                                ProductId = product.ProductId,
                                ProductName = product.ProductName,
                                UnitsInStock = product.UnitsInStock
                            };

                            await _hubContext.Clients.All.SendAsync("LowStockAlert", dto, stoppingToken);

                            _logger.LogWarning($"LOW STOCK ALERT: {dto.ProductName} ({dto.UnitsInStock} units left)");
                            _alertedProductIds.Add(dto.ProductId);
                        }
                    }

                    // nếu sản phẩm đã được bổ sung lại, xóa khỏi danh sách đã alert
                    var replenishedIds = _alertedProductIds
                        .Where(id => products.FirstOrDefault(p => p.ProductId == id)?.UnitsInStock >= Threshold)
                        .ToList();

                    foreach (var id in replenishedIds)
                    {
                        _alertedProductIds.Remove(id);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while checking for low stock products");
                }

                await Task.Delay(TimeSpan.FromMinutes(DelayInMinutes), stoppingToken);
            }
        }
    }
}
