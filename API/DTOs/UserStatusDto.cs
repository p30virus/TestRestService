using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserStatusDto
    {
        [Required]
        public string UserName { get; set; }
    }
}