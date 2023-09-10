using System;

namespace LowCode.Models;

public class InternalUIView : IEntityBase
{
    public Guid? ViewId { get; set; }

    public string? ViewName { get; set; }

    public string? SearchFormJson { get; set; }

    public string? LayoutJson { get; set; }

    public bool? IsDefault { get; set; }

    public sbyte? Description { get; set; }

    /// <summary>
    /// LookupView = 0,SearchView = 1,BulkEditView = 2,AssociatedView = 3
    /// </summary>
    /// <value></value>
    public QueryType QueryType { get; set; } = QueryType.LookupView;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public Guid? EntityId { get; set; }

    public virtual InternalEntity Entity { get; set; } = null!;
}