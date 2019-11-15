using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class User : IdentityUser<int>
    {
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Role { get; set; }
        public string Scope { get; set; }
        public Nullable<int> OrganizationId {get; set;} 
        public Nullable<int> FacilityId {get; set;} 
        public Nullable<int> DepartmentId {get; set;} 
        public Nullable<int> StoreId {get; set;} 
        public ICollection<UserRole> UserRoles { get; set; }

        public static implicit operator User(IdentityUser v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator int(User v)
        {
            throw new NotImplementedException();
        }
    }
}