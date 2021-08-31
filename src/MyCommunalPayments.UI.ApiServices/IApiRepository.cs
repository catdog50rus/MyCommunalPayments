using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCommunalPayments.UI.ApiServices
{
    public interface IApiRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T item);
        Task EditAsync(T item);
        Task RemoveAsync(int id);
        Task<T> GetByIdAsync(int id);
    }
}
