using BusinessObject.Entities;
using DataAccess.DTO;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IHubContext<ProductCategoryHub> _hub;

        public CategoryService(ICategoryRepository repository, IHubContext<ProductCategoryHub> hub)
        {
            _repository = repository;
            _hub = hub;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _repository.AddAsync(category);
            await _hub.Clients.All.SendAsync("CategoryCreated", new CategorySignalRDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            });
        }

        public async Task DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _repository.GetByIdAsync(id);
                if (category == null)
                    throw new Exception("Category not found");

                await _repository.DeleteAsync(id);
                await _hub.Clients.All.SendAsync("CategoryDeleted", id);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Cannot delete category because it is referenced by existing products.", ex);
            }
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _repository.UpdateAsync(category);
            await _hub.Clients.All.SendAsync("CategoryUpdated", new CategorySignalRDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            });
        }
    }
}