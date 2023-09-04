using System;

namespace LowCode.Infrastructure.Models
{
    public interface ICreation
    {
        public int CreateBy { get; set; }

        public DateTime CreateOn { get; set; }
    }

}