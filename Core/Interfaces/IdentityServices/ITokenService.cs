using Core.Entities.Identity;

namespace Core.Interfaces.IdentityServices
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}