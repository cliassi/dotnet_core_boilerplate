using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class UserMapper
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int SubGroupId { get; set; }
        public int AgencyId { get; set; }
    }
}
