using Shoes.Core.Entities.Concrete;
using Shoes.Entites;

namespace Shoes.Core.Security.Abstarct
{
    public interface ITokenService
    {
        Task<Token> CreateAccessTokenAsync(User User, List<string> roles);
        string CreateRefreshToken();
    }
}
