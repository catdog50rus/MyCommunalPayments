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
        public PeriodViewModel PeriodModel { get; set; }

        public PeriodsBase()
        {
            PeriodModel = new PeriodViewModel();
        }
        
        /// <summary>
        /// Репозиторий данных
        /// </summary>
        [Inject]
        public IApiRepository<Period> Repository { get; set; }

        protected Period period = default;
        protected IEnumerable<Period> periods;

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

        protected override async Task OnInitializedAsync()
        {
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

            if (period == null)
            {
                period = new Period()
                {
                    Year = PeriodModel.Year.ToString(),
                    Month = PeriodModel.Month
                };

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
                period.Year = PeriodModel.Year.ToString();
                period.Month = PeriodModel.Month;
                await Repository.EditAsync(period);
                period = default;
            }
            CloseModal();
            await StateUpdate();
            ToastShow(toastMessage.Item1, toastMessage.Item2);
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(Period item)
        {
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
            periods = await Repository.GetAllAsync();
            periods = periods.OrderByDescending(p => p.ToSort());
        }

        protected void ToastShow(string mes, ToastLevel level)
        {
            message = mes;
            toast.ShowToast(level);
        }

        protected async Task Confirm()
        {
            confirm = true;

            await DeleteData();
        }

        private async Task DeleteData()
        {

            if (confirm && period != null)
            {
                await Repository.RemoveAsync(period.IdKey);
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
        [Required]
        public int Year { get; set; } = DateTime.Now.Year;
        [Required]
        public PeriodsName Month { get; set; }
    }
}
