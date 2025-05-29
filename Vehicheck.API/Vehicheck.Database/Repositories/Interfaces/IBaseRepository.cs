using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync(bool includeDeletedEntities = false);
        Task<T?> GetFirstOrDefaultAsync(int primaryKey, bool includeDeletedEntities = false);
        void Insert(params T[] records);
        void Update(params T[] records);
        void SoftDelete(params T[] records);
        Task SaveChangesAsync();
    }
}