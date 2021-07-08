using API.DTOs;

namespace API.Interfaces
{
    public interface ITokenService
    {
         string CreateToken(LoginDto user);
    }
}
