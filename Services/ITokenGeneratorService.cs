namespace MVCCoreApplication.Services
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(int id, string name);
        bool IsTokenValid(string userSecretKey, string userIssuer, string userAudience, string userToken);
        //bool IsTokenValid(string token);
    }
}
