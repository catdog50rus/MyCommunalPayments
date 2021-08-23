using MyCommunalPayments.Data.DBModels.Models.Base;

namespace MyCommunalPayments.Data.DBModels.Models
{
    public class OrderDb : BaseDbModel
    {
        public string FileName { get; set; }

        public byte[] OrderScreen { get; set; }
    }
}
