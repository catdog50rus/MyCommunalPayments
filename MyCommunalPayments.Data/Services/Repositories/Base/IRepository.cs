using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Получить список услуг
        /// </summary>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Добавить услугу
        /// </summary>
        void Add(T item);
        /// <summary>
        /// Изменить услугу
        /// </summary>
        void Edit(T item);
        /// <summary>
        /// Удалить услугу
        /// </summary>
        void Remove(T item);
        T GetById(int id);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T item);
        Task<T> EditAsync(T item);
        Task<T> RemoveAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> Search(string name);


    }
}
