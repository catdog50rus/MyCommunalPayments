using MyCommunalPayments.Data.DBModels.Models.Base;

namespace MyCommunalPayments.Data.DBModels.Models
{
    public class ServiceDb : BaseDbModel
    {
        public string NameService { get; set; }
        public bool IsCounter { get; set; }
    }
}
