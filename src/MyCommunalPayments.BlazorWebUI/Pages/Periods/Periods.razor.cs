using Microsoft.AspNetCore.Components;
using MyCommunalPayments.BlazorWebUI.Components;
using MyCommunalPayments.BlazorWebUI.Shared;
using MyCommunalPayments.Data.Services.ApiServices;
using MyCommunalPayments.Data.Services.Toast;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Periods
{
    public class PeriodsBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно

        /// <summary>
        /// Данные модели представления
        /// </summary>
        public PeriodViewModel PeriodModel { get; set; } = new PeriodViewModel();
        
        /// <summary>
        /// Репозиторий данных
        /// </summary>
        [Inject]
        public IApiRepository<Period> Repository { get; set; }

        protected Period period = default;
        protected IEnumerable<Period> periods;

        protected int[] pageSizeList = new int[] { 10, 25, 50 };
        private int pageOfSet = 0;
        private int pageSize;
        protected int totalItems = 0;

        //Модальное окно
        protected Modal modal;
        protected void CloseModal()
        {
            PeriodModel = new PeriodViewModel();
            period = default;
            modal.Close();
        }
        protected void OpenModal()
        {
            modal.Open();
        }

        //Уведомление
        protected Toast toast;
        protected string message;
        private bool confirm;

        protected async Task SetPageOfSet(int[] page)
        {
            pageOfSet = page[0];
            pageSize = page[1];
            await StateUpdate();
        }

        protected override async Task OnInitializedAsync()
        {
            pageSize = pageSizeList[0];
            await StateUpdate();
            NavMenu.SetSubMenu(true);
        }

        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить или отредактировать
        /// </summary>
        protected async Task AddAsync()
        {
            (string, ToastLevel) toastMessage = ("Данные обновлены", ToastLevel.Success);

            //Проверяем существует ли текущий период
            if (period == null)
            {
                //Создаем и инициализируем модель
                period = new Period()
                {
                    Year = PeriodModel.Year.ToString(),
                    Month = PeriodModel.Month
                };

                //Если период уникальный добавляем в БД
                if (periods.FirstOrDefault(p => p.Equals(period)) == null)
                {
                    await Repository.AddAsync(period);
                }
                else
                {
                    toastMessage = ("Такой период уже существует!", ToastLevel.Error);
                }

            }
            else
            {
                //Меняем текущий период и записываем изменения в БД
                period.Year = PeriodModel.Year.ToString();
                period.Month = PeriodModel.Month;
                await Repository.EditAsync(period);
                period = default;
            }
            CloseModal();
            totalItems = 0;
            await StateUpdate();
            ToastShow(toastMessage.Item1, toastMessage.Item2);
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(Period item)
        {
            //Готовим модель представления
            period = item;
            modal.Open();
            PeriodModel.Year = int.Parse(period.Year);
            PeriodModel.Month = period.Month;
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="item"></param>
        protected void Remove(Period item)
        {
            period = item;
            ToastShow("Внимание! Данные о периоде будут безвозвратно удалены. Вы уверенны?", ToastLevel.Warning);
            
        }

        #endregion

        private async Task StateUpdate()
        {
            if (totalItems == 0)
            {
                periods = (await Repository.GetAllAsync()).OrderByDescending(p => p.ToSort());
                totalItems = periods.Count();
                periods = periods.Skip(pageOfSet).Take(pageSize);
            }
            else
            {
                periods = (await Repository.GetAllAsync()).OrderByDescending(p => p.ToSort()).Skip(pageOfSet).Take(pageSize); ;
            }
            
        }

        protected void ToastShow(string mes, ToastLevel level)
        {
            message = mes;
            toast.ShowToast(level);
        }

        /// <summary>
        /// Удаляем данные после подтверждения
        /// </summary>
        /// <returns></returns>
        protected async Task Confirm()
        {
            confirm = true;

            await DeleteData();
        }

        /// <summary>
        /// Реализация удаления из БД
        /// </summary>
        /// <returns></returns>
        private async Task DeleteData()
        {

            if (confirm && period != null)
            {
                await Repository.RemoveAsync(period.IdKey);
                totalItems = 0;
                await StateUpdate();
            }
            confirm = false;

        }
    }

    /// <summary>
    /// Модель представления 
    /// </summary>
    public class PeriodViewModel
    {
        /// <summary>
        /// Год
        /// </summary>
        [Required]
        public int Year { get; set; } = DateTime.Now.Year;
        /// <summary>
        /// Месяц
        /// </summary>
        [Required]
        public PeriodsName Month { get; set; }
    }
}
