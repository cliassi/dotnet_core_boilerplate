using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Register
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
