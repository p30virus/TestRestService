using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class UserMembershipModifyDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public List<string> Membership { get; set; }
    }
}