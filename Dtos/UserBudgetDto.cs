using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class UserBudgetDto
    {
        public List<User> users { get; set; }
        public int RecordCount  {get; set;}
        public int PageCount  {get; set; }
        public Nullable<int> GroupCount { get; set; }
        public Nullable<int> GroupPageCount { get; set; }
        public Nullable<int> SubGroupCount { get; set; }
        public Nullable<int> SubGroupPageCount { get; set; }
        public Nullable<int> AgencyCount { get; set; }
        public Nullable<int> AgencyPageCount { get; set; }
        public Nullable<int> ConsultantCount { get; set; }
        public Nullable<int> ConsultantPageCount { get; set; }
    }
}
