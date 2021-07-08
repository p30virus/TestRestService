using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public string GivenName { get; set; }
        public string sn { get; set; }
        public string cn { get; set; }
        public string EmployeeNumber { get; set; }
        public string Email { get; set; }
    }
}