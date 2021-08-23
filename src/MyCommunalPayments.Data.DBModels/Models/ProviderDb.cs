using MyCommunalPayments.Data.DBModels.Models.Base;

namespace MyCommunalPayments.Data.DBModels.Models
{
    public class ProviderDb : BaseDbModel
    {
        public string NameProvider { get; set; }

        public string WebSite { get; set; }
    }
}
