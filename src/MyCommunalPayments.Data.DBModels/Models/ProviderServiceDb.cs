using MyCommunalPayments.Data.DBModels.Models.Base;

namespace MyCommunalPayments.Data.DBModels.Models
{
    public class ProviderServiceDb : BaseDbModel
    {
        public int IdProvider { get; set; }
        public ProviderDb Provider { get; set; }
        public int IdService { get; set; }
        public ServiceDb Service { get; set; }
    }
}
