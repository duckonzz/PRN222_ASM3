using BusinessObject.Entities;
using DataAccess.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task AddProductAsync(Product product)
        {
            await _repository.AddAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteAsync(id);
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
        }
    }
}