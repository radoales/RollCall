namespace RollCall.MVC.Services
{
    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, string role, string secret);
    }
}
