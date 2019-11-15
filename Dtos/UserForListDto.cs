using System;

namespace API.Dtos
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Role { get; set; }
        public Nullable<int> OrganizationId { get; set; }
        public Nullable<int> FacilityId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> StoreId { get; set; }
        public string Scope { get; set; }
    }
}