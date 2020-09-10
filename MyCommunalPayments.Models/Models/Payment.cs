using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCommunalPayments.Models.Models
{
    /// <summary>
    /// Оплата за услуги ЖКХ
    /// </summary>
    [Table("Payments")]
    public class Payment
    {
        [Key]
        public int IdPayment { get; set; }
        /// <summary>
        /// Дата платежа
        /// </summary>
        public DateTime DatePayment { get; set; } = DateTime.Today;
        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal PaymentSum { get; set; }
        /// <summary>
        /// Путь к платежке
        /// </summary>
        public string OrderPath { get; set; }
    }
}
