using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCommunalPayments.Models.Models
{
    /// <summary>
    /// Квитанции на оплату услуг ЖКХ
    /// </summary>
    [Table("Invoices")]
    public class Invoice
    {
        [Key]
        public int IdInvoice { get; set; }
        /// <summary>
        /// Период за который выставлен счет
        /// </summary>
        public int IdPeriod { get; set; }
        [ForeignKey("IdPeriod")]
        public Period Period { get; set; }
        /// <summary>
        /// Id поставщика услуги
        /// </summary>
        public int IdProvider { get; set; }
        /// <summary>
        /// Навигационное свойство
        /// Внешний ключ
        /// </summary>
        [ForeignKey("IdProvider")]
        public Provider Provider { get; set; }
        /// <summary>
        /// Сумма счета
        /// </summary>

        public decimal InvoiceSum { get; set; }

        public bool Pay { get; set; }


        public override string ToString() => $"{IdPeriod}, {IdProvider}";

        public override bool Equals(object obj) => this.ToString() == obj.ToString();

        public override int GetHashCode() => base.GetHashCode();
 
    }
}
