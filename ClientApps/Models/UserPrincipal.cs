namespace ClientApps.Models
{
    public class UserPrincipal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string JwtToken { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}
