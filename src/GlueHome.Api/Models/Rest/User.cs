namespace GlueHome.Api.Models.Rest
{
    public class User : IMember 
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}