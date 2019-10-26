using System;

namespace Domain
{
    public interface IEntityRepository<T> where T : AbstractEntity
    {
        void Save(T entity);
        T Get(Guid id);
        T Get(string id);

        void Save(params T[] entities);
        void SaveAsync(params T[] entities);
    }
}