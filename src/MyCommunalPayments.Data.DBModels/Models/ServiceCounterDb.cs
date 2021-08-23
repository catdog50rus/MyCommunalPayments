using MyCommunalPayments.Data.DBModels.Models.Base;

namespace MyCommunalPayments.Data.DBModels.Models
{
    public class ServiceCounterDb : BaseDbModel
    {
        public int IdService { get; set; }
        public ServiceDb Service { get; set; }
        public string DateCount { get; set; }
        public int ValueCounter { get; set; }
    }
}
