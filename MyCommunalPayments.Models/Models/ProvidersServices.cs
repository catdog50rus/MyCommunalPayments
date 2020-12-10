using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyCommunalPayments.Models.Models
{
    [Table("ProvidersServices")]
    public class ProvidersServices
    {
        [Key]
        public int Id { get; set; }

        public int IdProvider { get; set; }

        [ForeignKey("IdProvider")]
        [Required]
        public Provider Provider { get; set; }

        public int IdService { get; set; }

        [ForeignKey("IdService")]
        [Required]
        public Service Service { get; set; }

    }
}
