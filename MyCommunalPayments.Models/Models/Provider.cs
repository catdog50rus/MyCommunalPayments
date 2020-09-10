using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCommunalPayments.Models.Models
{
    /// <summary>
    /// Поставщики услуг ЖКХ
    /// </summary>
    [Table("Providers")]
    public class Provider
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int IdProvider { get; set; }
        /// <summary>
        /// Наименование поставщика услуги ЖКХ
        /// </summary>
        public string NameProvider { get; set; }
        /// <summary>
        /// Путь к личному кабинету поставщика
        /// </summary>
        public string WebSite { get; set; }

        public override string ToString()
        {
            return NameProvider;
        }

    }
}
