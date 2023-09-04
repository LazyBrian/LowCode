using System;
namespace LowCode.Models
{
    public class Attribute
    {
        public int AttributeId { get; set; }

        public string LogicalName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public int EntityId { get; set; }

        public Entity Entity { get; set; }
    }
}