using Microsoft.EntityFrameworkCore;
using System;
namespace LowCode.Models
{
    public class Entity
    {
        public int EntityId { get; set; }

        public string LogicalName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public IList<Attribute> Attributes { get; set; }
    }
}