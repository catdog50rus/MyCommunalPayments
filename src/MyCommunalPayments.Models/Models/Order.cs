using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyCommunalPayments.Models.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int IdOrder { get; set; }

        public string FileName { get; set; }

        public byte[] OrderScreen { get; set; }
    }
}
