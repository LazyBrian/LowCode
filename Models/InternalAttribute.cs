using System;
using Microsoft.SqlServer.Management.Smo;
namespace LowCode.Models
{
    public class InternalAttribute : IEntityBase
    {
        public InternalAttribute()
        {
        }

        public Guid? AttributeId { get; set; }

        public string? LogicalName { get; set; }

        public string? DisplayName { get; set; }

        public int AttributeMask { get; set; }

        public string? DefaultValue { get; set; }

        public bool? IsCustomField { get; set; }

        public bool? IsPKAttribute { get; set; }

        public int? MaxLength { get; set; }

        public decimal? MinValue { get; set; }

        public decimal? MaxValue { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }
        
        public DateTime? CreatedOn { get; set; }
        
        public int? ModifiedBy { get; set; }
        
        public DateTime? ModifiedOn { get; set; }

        public Guid? EntityId { get; set; }

        public Guid? AttributeTypeId { get; set; }

        public virtual InternalAttributeType AttributeType { get; set; } = null!;

        public virtual InternalEntity Entity { get; set; } = null!;
    }
}