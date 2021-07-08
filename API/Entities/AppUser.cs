namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string GivenName { get; set; }
        public string sn { get; set; }
        public string cn { get; set; }
        public string EmployeeNumber { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }

    }
}