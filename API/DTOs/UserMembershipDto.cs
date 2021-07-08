using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class UserMembershipDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public List<AppGroupMembership> Membership { get; set; }
    }
}