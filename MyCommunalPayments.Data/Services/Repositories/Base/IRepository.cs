using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

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


    }
}
