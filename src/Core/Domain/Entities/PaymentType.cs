using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public record PaymentType : AuditableBaseEntity
    {
        public PaymentType()
        {
            Payments = new HashSet<Payment>();
        }

        public string Name { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
