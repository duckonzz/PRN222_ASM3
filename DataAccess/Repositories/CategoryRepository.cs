using BusinessObject.Entities;
using DataAccess.Data;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly eStoreDuckContext _context;
        private readonly DbSet<Category> _dbSet;

        public CategoryRepository(eStoreDuckContext context)
        {
            _context = context;
            _dbSet = context.Set<Category>();
        }

        public async Task<List<Category>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<Category> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(Category category)
        {
            await _dbSet.AddAsync(category); await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _dbSet.Update(category); await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            if (category != null)
            {
                _dbSet.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}