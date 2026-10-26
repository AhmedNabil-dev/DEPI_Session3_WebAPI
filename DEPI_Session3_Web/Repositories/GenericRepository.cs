using DEPI_Session1_WedAPI.Migrations;
using DEPI_Session3_Web.Domain;
using DEPI_WebApi_Session2.Infrestraction.Data;
using Microsoft.EntityFrameworkCore;

namespace DEPI_Session3_Web.Repositories
{
    public class GenericRepository<T> : IGenericRepositroy<T> where T : BaseEntity
    {
        private readonly ApplicationDBContext _dbContext;

        public GenericRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> CreateAsync(T entity)
        {
            object value = await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity is null)
                return false;

            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _dbContext.Set<T>().ToListAsync();


        public async Task<T> GetByIdAsync(int id)
            => await _dbContext.Set<T>().FindAsync(id);

        //=> await _dbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

        public async Task<bool> UpdateAsync(T entity)
        {
            var model = await GetByIdAsync(entity.Id);

            if (model is null)
                return false;

            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
