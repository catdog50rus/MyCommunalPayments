using MyCommunalPayments.Data.DBModels.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCommunalPayments.Data.DBModels.Models
{
    public class InvoiceDb : BaseDbModel
    {
        public int IdPeriod { get; set; }

        public PeriodDb Period { get; set; }

        public int IdProvider { get; set; }

        public ProviderDb Provider { get; set; }

        public decimal InvoiceSum { get; set; }

        public bool Pay { get; set; }
    }
}
