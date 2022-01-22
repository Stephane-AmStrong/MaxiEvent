using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public record Order : AuditableBaseEntity
    {
        public Order()
        {
            Payments = new HashSet<Payment>();
            //Places = new HashSet<Place>();
        }

        public DateTime Date { get; set; }
        [Required]
        public String AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
        //public virtual ICollection<Place> Places { get; set; }
    }
}
