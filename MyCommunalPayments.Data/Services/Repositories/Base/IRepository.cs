using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T item);
        Task<T> EditAsync(T item);
        Task<T> RemoveAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> Search(string name);
    }
}
