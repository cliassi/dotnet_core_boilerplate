using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Models;

namespace API.Controllers
{
    public class CommonMethods
    {
        public string GetScope(UserForListDto user)
        {
            if (user.OrganizationId == null)
            {
                return "Global";
            }
            else if (user.StoreId != null)
            {
                return "Store";
            }
            else if (user.FacilityId != null)
            {
                return "Facility";
            }
            else if (user.DepartmentId != null)
            {
                return "Department";
            }
            return "Organization";
        }

        public string GetScopeById(DataContext _context, int userId)
        {
            var user = _context.Users.Find(userId);
            if (user.OrganizationId == null)
            {
                return "Global";
            }
            else if (user.StoreId != null)
            {
                return "Store";
            }
            else if (user.FacilityId != null)
            {
                return "Facility";
            }
            else if (user.DepartmentId != null)
            {
                return "Department";
            }
            return "Organization";
        }

        public string GetScopeByUser(User user)
        {
            if (user.OrganizationId == null)
            {
                return "Global";
            }
            else if (user.StoreId != null)
            {
                return "Store";
            }
            else if (user.FacilityId != null)
            {
                return "Facility";
            }
            else if (user.DepartmentId != null)
            {
                return "Department";
            }
            return "Organization";
        }
    }
}
