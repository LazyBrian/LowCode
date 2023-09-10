using System;

namespace LowCode.Models;

public interface IEntityBase
{
    int? CreatedBy { get; set; }

    DateTime? CreatedOn { get; set; }

    int? ModifiedBy { get; set; }

    DateTime? ModifiedOn { get; set; }
}