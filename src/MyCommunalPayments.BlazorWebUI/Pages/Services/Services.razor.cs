using Microsoft.AspNetCore.Components;
using MyCommunalPayments.BlazorWebUI.Components;
using MyCommunalPayments.BlazorWebUI.Shared;
using MyCommunalPayments.Data.Services.Toast;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MyCommunalPayments.UI.ApiServices;

namespace MyCommunalPayments.BlazorWebUI.Pages.Services.Base
{
    public class ServicesBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно

        /// <summary>
        /// Модель представления
        /// </summary>
        protected ServiceViewModel ServiceModel = new ServiceViewModel();

        [Inject]
        public IApiRepository<Service> Repository { get; set; }

        //Услуги
        protected Service service;
        protected IEnumerable<Service> Services { get; set; }

        //Модальное окно
        protected Modal modal;
        protected string modalTitle = "Добавление новой услуги ЖКХ";
        protected void CloseModal()
        {
            ServiceModel = new ServiceViewModel();
            service = default;
            modal.Close();

        }
        protected void OpenModal()
        {
            modal.Open();
        }

        //Уведомление
        private bool confirm;
        protected Toast toast;
        protected string message;

        protected override async Task OnInitializedAsync()
        {
            
            await StateUpdate();
            NavMenu.SetSubMenu(true);
            service = default;
        }

        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить дело
        /// </summary>
        protected async Task AddAsync()
        {
            (string, ToastLevel) toastMessage = ("Данные обновлены", ToastLevel.Success);

            //Проверяем, есть ли текущая модель
            if (service == null)
            {
                //Создаем и инициализируем модель
                service = new Service()
                {
                    NameService = ServiceModel.Name,
                    IsCounter = ServiceModel.IsCounter
                };
                //Если модель уникальна, записываем ее в БД
                if (Services.FirstOrDefault(s => s.Equals(service)) == null)
                {
                    await Repository.AddAsync(service);
                }
                else 
                {
                    toastMessage = ("Такой поставщик уже существует!", ToastLevel.Error);
                }
                
            }
            else
            {
                //Меняем модель и записываем в БД
                service.NameService = ServiceModel.Name;
                service.IsCounter = ServiceModel.IsCounter;
                await Repository.EditAsync(service);
            }

            CloseModal();
            await StateUpdate();
            ToastShow(toastMessage.Item1, toastMessage.Item2);
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        public void Edit(Service item)
        {
            service = item;
            ServiceModel.Name = service.NameService;
            ServiceModel.IsCounter = service.IsCounter;
            OpenModal();
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        protected async Task RemoveAsync(Service item)
        {
            await Repository.RemoveAsync(item.IdService);
            await StateUpdate();
        }

        #endregion

        protected void ToastShow(string mes, ToastLevel level)
        {
            message = mes;
            toast.ShowToast(level);
        }

        /// <summary>
        /// Подтверждение удаления
        /// </summary>
        /// <returns></returns>
        protected async Task Confirm()
        {
            confirm = true;

            await DeleteData();
        }

        /// <summary>
        /// Реализация удаления
        /// </summary>
        /// <returns></returns>
        private async Task DeleteData()
        {

            if (confirm && service != null)
            {
                await Repository.RemoveAsync(service.IdService);
                await StateUpdate();
            }
            confirm = false;

        }

        private async Task StateUpdate()
        {
            Services = await Repository.GetAllAsync();
        }
    }

    /// <summary>
    /// Модель представления
    /// </summary>
    public class ServiceViewModel
    {
        /// <summary>
        /// Наименование услуги
        /// </summary>
        [Required]
        [MaxLength(75, ErrorMessage ="Превышена максимальная длина наименования")]
        [MinLength(5, ErrorMessage = "Наименование не может быть короче 5 символов")]
        public string Name { get; set; }

        /// <summary>
        /// Флаг, подразумевает ли услуга счетчик
        /// </summary>
        public bool IsCounter { get; set; }
    }
}
