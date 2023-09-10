using System;

namespace LowCode.Models;

public class InternalUIForm : IEntityBase
{
    public Guid? FormId { get; set; }

    public string? FormName { get; set; }

    public string? FormJson { get; set; }

    public string? Description { get; set; }

    public bool? IsDefault { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public Guid? EntityId { get; set; }

    public virtual InternalEntity Entity { get; set; } = null!;

}