using Microsoft.EntityFrameworkCore;
using System;
namespace LowCode.Models
{
    public class InternalEntity : IEntityBase
    {
        public Guid? EntityId { get; set; }

        public string? LogicalName { get; set; }

        public string? DisplayName { get; set; }

        public bool? IsCustomEntity { get; set; }

        public int EntityMask { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<InternalAttribute> Attributes { get; set; } = new List<InternalAttribute>();
    }
}