using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCommunalPayments.Models.Models
{
    /// <summary>
    /// Взаиморасчеты с поставщиками ЖКХ
    /// </summary>
    [Table("Transactions")]
    public class Transactions
    {
        [Key]
        public int IdTransaction { get; set; }
        /// <summary>
        /// Расчетный период
        /// Месяц года
        /// </summary>
        public string Period { get; set; }
        /// <summary>
        /// Квитанция
        /// </summary>
        public int Invoice { get; set; }
        /// <summary>
        /// Оплата
        /// </summary>
        public int Payment { get; set; }
    }
}
