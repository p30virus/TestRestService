using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppGroupMembershipV2
    {
        public int Id { get; set; }
        [Required]
        public string GroupName { get; set; }
    }
}
