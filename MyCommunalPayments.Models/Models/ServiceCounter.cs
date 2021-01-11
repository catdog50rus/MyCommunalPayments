using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCommunalPayments.Models.Models
{
    [Table("ServicesCounters")]
    public class ServiceCounter
    {
        [Key]
        public int IdCounter { get; set; }
        public int IdService { get; set; }
        [ForeignKey("IdService")]
        public Service Service { get; set; }
        public string DateCount { get; set; }
        public int ValueCounter { get; set; }

        public override string ToString()
        {
            return $"{Service.NameService}: {ValueCounter}"; 
        }

        public int ToSort()
        {
            _ = int.TryParse(DateCount.Substring(6, 4), out int rrr);
            _ = int.TryParse(DateCount.Substring(3, 2), out int rr);
            _ = int.TryParse(DateCount.Substring(0, 2), out int r);
            return rrr * 10000000 + rr * 1000 + r;
        }

        public int MaxValue()
        {
            return ValueCounter;
        }

    }
}
