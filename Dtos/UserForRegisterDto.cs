using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 32 characters")]
        public string Password { get; set; }

        [Required]
        public string KnownAs { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        [Required]
        public string Role { get; set; }

        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }

        public Nullable<int> OrganizationId { get; set; }
        public Nullable<int> FacilityId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> StoreId { get; set; }
    }
}