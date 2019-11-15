using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class UserAPIDto
    {
        public int Id { get; set; }
        public string KnownAs { get; set; }
        public string Role { get; set; }
    }
}
