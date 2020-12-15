using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCommunalPayments.Models.Models
{
    [Table("InvoceServices")]
    public class InvoiceServices
    {
        [Key]
        public int IdInvoiceServices { get; set; }

        public int IdInvoice { get; set; }

        [ForeignKey("IdInvoice")]
        public Invoice Invoice { get; set; }

        public int IdService { get; set; }

        [ForeignKey("IdService")]
        public Service Service { get; set; }

        public int Amount { get; set; }

        public override string ToString()
        {
            return $"Квитанция: {IdInvoice}, Сервис: {IdService}";
        }

        public override bool Equals(object obj) => this.ToString() == obj.ToString();

        public override int GetHashCode() => base.GetHashCode();

    }
}
