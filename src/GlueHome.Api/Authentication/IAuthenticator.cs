namespace GlueHome.Api.Authentication
{
    public interface IAuthenticator
    {
        bool IsAuthenticated(string username, string password);
    }
}