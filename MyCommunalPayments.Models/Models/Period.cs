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

        public override bool Equals(object obj)
        {
            if (this.ToString() == obj.ToString()) return true;
            else return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int ToSort()
        {
            //Пример 01.2021 = 202101
            //       12.2020 = 202012
            return int.Parse(Year)*100 + (int)Month;
        }
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
