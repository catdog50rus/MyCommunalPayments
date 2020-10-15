using Microsoft.AspNetCore.Components;
using MyCommunalPayments.BlazorWebUI.Shared;
using MyCommunalPayments.Data.Services.ApiServices;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Periods
{
    public class PeriodsBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно
        [Inject]
        public IApiRepository<Period> Repository { get; set; }

        protected Period period = default;
        protected IEnumerable<Period> periods;
        protected string year;
        protected PeriodsName month;

        //Модальное окно
        protected Modal modal;// { get; set; }
        protected void CloseModal()
        {
            year = default;
            month = default;
            period = default;
            modal.Close();
        }
        protected void OpenModal()
        {
            modal.Open();
        }

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
        protected async Task Add()
        {
            if (!string.IsNullOrWhiteSpace(year))
            {

                if (period == null)
                {
                    period = new Period()
                    {
                        Year = year,
                        Month = month
                    };

                    await Repository.AddAsync(period);
                }
                else
                {
                    period.Year = year;
                    period.Month = month;
                    await Repository.EditAsync(period);
                    period = default;
                }
            }

            CloseModal();
            await StateUpdate();
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(Period item)
        {
            period = item;
            modal.Open();
            year = item.Year;
            month = item.Month;
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="item"></param>
        protected async Task Remove(Period item)
        {
            await Repository.RemoveAsync(item.IdKey);
            await StateUpdate();
        }

        #endregion

        private async Task StateUpdate()
        {
            periods = await Repository.GetAllAsync();
            periods = periods.OrderByDescending(p => p.ToSort());
        }
    }
}
