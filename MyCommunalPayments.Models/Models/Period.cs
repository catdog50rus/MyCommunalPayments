using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyCommunalPayments.Models.Models
{
    [Table("Periods")]
    public class Period
    {
        [Key]
        public int IdKey { get; set; }
        public string Year { get; set; }
        public PeriodsName Month { get; set; }

        public override string ToString()
        {
            return $"{Month} {Year}";
        }

        public int ToSort()
        {
            return int.Parse(Year)*100 + (int)Month;
        }
    }

    public enum PeriodsName
    {
        Январь = 1,
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
