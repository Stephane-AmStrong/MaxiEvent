using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public record Payment : AuditableBaseEntity
    {
        public string Name { get; set; }
        public float AmountPaid { get; set; }
        public float AmountRest { get; set; }
        public DateTime? PaymentDate { get; set; }

        [Required]
        public Guid PaymentTypeId { get; set; }

        [ForeignKey("PaymentTypeId")]
        public PaymentType PaymentType { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

    }
}
