using Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public record Category : AuditableBaseEntity
    {
        public Category()
        {
            Events = new HashSet<Event>();
        }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
