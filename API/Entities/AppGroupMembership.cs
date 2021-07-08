using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppGroupMembership
    {
        public int Id { get; set; }
        [Required]
        public string GroupName { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}