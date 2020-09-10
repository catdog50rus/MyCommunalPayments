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
        public string Month { get; set; }

        public override string ToString()
        {
            return Month;
        }
    }
}
