using System;

namespace MyCommunalPayments.Data.DBModels.Models.Base
{
    public abstract class BaseDbModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
