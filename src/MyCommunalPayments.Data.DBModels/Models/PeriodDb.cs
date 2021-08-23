using MyCommunalPayments.Data.DBModels.Models.Base;

namespace MyCommunalPayments.Data.DBModels.Models
{
    public class PeriodDb : BaseDbModel
    {
        public string Year { get; set; }
        public PeriodsName Month { get; set; }
    }

    public enum PeriodsName
    {
        None,
        Январь,
        Февраль,
        Март,
        Апрель,
        Май,
        Июнь,
        Июль,
        Август,
        Сентябрь,
        Октябрь,
        Ноябрь,
        Декабрь
    }
}
