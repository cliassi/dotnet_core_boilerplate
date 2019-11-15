using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ChangePasswordDto
    {
        public String Password { get; set; }
        public String NewPassword { get; set; }
    }
}
