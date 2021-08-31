using MyCommunalPayments.Data.DBModels.Models.Base;

namespace MyCommunalPayments.Data.DBModels.Models
{
    public class PaymentDb : BaseDbModel
    {
        public string DatePayment { get; set; }

        public decimal PaymentSum { get; set; }

        public int IdInvoice { get; set; }

        public InvoiceDb Invoice { get; set; }

        public bool Paid { get; set; }

        public int? IdOrder { get; set; }

        public OrderDb Order { get; set; }
    }
}
