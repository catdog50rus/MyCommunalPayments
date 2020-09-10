using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCommunalPayments.Models.Models
{
    /// <summary>
    /// Услуга
    /// </summary>
    [Table("Services")]
    public class Service
    {
        [Key]
        public int IdService { get; set; }
        /// <summary>
        /// Наименование услуги ЖКХ
        /// </summary>
        public string NameService { get; set; }
        /// <summary>
        /// Предусмотрен ли счетчик услуги
        /// </summary>
        public bool IsCounter { get; set; }

    }
}
