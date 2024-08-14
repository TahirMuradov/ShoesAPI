using Shoes.Core.Entities.Concrete;

namespace Shoes.Core.Security.Abstarct
{
    public interface ITokenService
    {
        Task<Token> CreateAccessTokenAsync(AppUser User, List<string> roles);
        string CreateRefreshToken();
    }
}
