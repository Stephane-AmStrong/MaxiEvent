using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesViewModel
    {
        public virtual Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
