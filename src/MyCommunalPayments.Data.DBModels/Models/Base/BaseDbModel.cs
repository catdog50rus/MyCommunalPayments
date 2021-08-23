using System;

namespace MyCommunalPayments.Data.DBModels.Models.Base
{
    public class BaseDbModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
    }
}
