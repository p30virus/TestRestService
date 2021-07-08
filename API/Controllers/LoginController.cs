using API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using API.Interfaces;

namespace API.Controllers
{
    public class LoginController : ApiBaseController
    {
        private string _staticUser = "admin";
        private string _staticPassword = "admin";
        private readonly ITokenService _tokenService;

        public LoginController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        public ActionResult<TokenDto> Login(LoginDto loginDto)
        {
            if (loginDto.UserName != _staticUser || loginDto.Password != _staticPassword )
            {
                return Unauthorized("Invalid credentials");
            }

            var UserToken = new TokenDto
            {
                UserName = loginDto.UserName,
                Token = _tokenService.CreateToken(loginDto)
            };
            return Ok(UserToken);
        }
    }
}