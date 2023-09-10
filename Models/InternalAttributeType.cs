using System;
using System.Runtime;

namespace LowCode.Models;

public class InternalAttributeType : IEntityBase
{
    public Guid? AttributeTypeId { get; set; }

    public string? Name { get; set; }

    public SqlType SqlType { get; set; } = SqlType.Int;

    public string? CustomType { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
}