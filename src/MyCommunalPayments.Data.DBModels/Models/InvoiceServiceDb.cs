using MyCommunalPayments.Data.DBModels.Models.Base;

namespace MyCommunalPayments.Data.DBModels.Models
{
    public class InvoiceServiceDb : BaseDbModel
    {
        public int IdInvoice { get; set; }

        public InvoiceDb Invoice { get; set; }

        public int IdService { get; set; }

        public ServiceDb Service { get; set; }

        public int Amount { get; set; }
    }
}
