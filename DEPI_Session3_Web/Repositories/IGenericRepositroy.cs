using DEPI_Session3_Web.Domain;

namespace DEPI_Session3_Web.Repositories
{
    public interface IGenericRepositroy<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
