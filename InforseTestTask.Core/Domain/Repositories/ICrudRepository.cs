using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.Domain.Repositories
{
    public interface ICrudRepository<T>
    {
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T?> FindByIdAsync(long id);
        Task DeleteAsync(long id);
        Task<ICollection<T>> FindAllAsync();

    }
}
