using MinimalApiFaculdade.Models;

namespace MinimalApiFaculdade.Services
{
    public interface ITokenService
    {
        public string GerarToken(string key, string issuer, string audience, UserModel user);

    }
}
