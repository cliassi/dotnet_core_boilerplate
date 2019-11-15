using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class SystemTask
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
    }
}
