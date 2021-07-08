using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UpdateUserDto
    {
        [Required]
        public string UserName { get; set; }

        public string GivenName { get; set; }
        public string sn { get; set; }
        public string cn { get; set; }
        public string EmployeeNumber { get; set; }
        public string Email { get; set; }
    }
}