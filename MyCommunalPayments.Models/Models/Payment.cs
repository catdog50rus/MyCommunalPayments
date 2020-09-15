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
        public string DatePayment { get; set; }
        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal PaymentSum { get; set; }
        /// <summary>
        /// Путь к платежке
        /// </summary>
        public string OrderPath { get; set; }
        /// <summary>
        /// Счет за ЖКХ
        /// </summary>
        public int IdInvoice { get; set; }
        /// <summary>
        /// Счет за ЖКХ
        /// </summary>
        [ForeignKey("IdInvoice")]
        public Invoice Invoice { get; set; }
        /// <summary>
        /// Флаг была ли произведена оплата
        /// </summary>
        public bool Paid { get; set; }
    }
}
