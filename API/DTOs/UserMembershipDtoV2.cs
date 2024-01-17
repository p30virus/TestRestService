using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class UserMembershipDtoV2
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string GivenName { get; set; }
        [Required]
        public string sn { get; set; }
        [Required]
        public string cn { get; set; }
        [Required]
        public string EmployeeNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public List<AppGroupMembershipV2> Membership { get; set; }
    }
}
