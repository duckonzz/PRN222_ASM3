using BusinessObject.Entities;
using DataAccess.DTO;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IHubContext<ProductCategoryHub> _hub;

        public ProductService(IProductRepository repository, IHubContext<ProductCategoryHub> hub)
        {
            _repository = repository;
            _hub = hub;
        }

        public async Task AddProductAsync(Product product)
        {
            await _repository.AddAsync(product);
            await _hub.Clients.All.SendAsync("ProductCreated", new ProductSignalRDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                Weight = product.Weight,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock
            });
        }

        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _hub.Clients.All.SendAsync("ProductDeleted", id);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _repository.UpdateAsync(product);
            await _hub.Clients.All.SendAsync("ProductUpdated", new ProductSignalRDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                Weight = product.Weight,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock
            });
        }
    }
}