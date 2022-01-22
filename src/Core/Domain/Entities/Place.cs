using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public record Place : AuditableBaseEntity
    {

        public int NoPlace { get; set; }
        public float Price { get; set; }
        [Required]
        public Guid EventId { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; }

        [Required]
        public Guid OrderId { get; set; }   

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

    }
}
