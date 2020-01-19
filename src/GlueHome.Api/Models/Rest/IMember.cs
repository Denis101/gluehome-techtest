namespace GlueHome.Api.Models.Rest
{
    public interface IMember
    {
        string Name { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
    }
}