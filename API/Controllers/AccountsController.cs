using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    
    public class AccountsController : ApiBaseController
    {
        private readonly DataContext _context;
        public AccountsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Users")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return Ok( await _context.Users.ToListAsync());
        }
        
        [HttpGet("User/{userName}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUser(string userName)
        {
            var exist = await _context.Users.AnyAsync( x => x.UserName == userName.ToLower());
            if (!exist)  return BadRequest("User doesnt Exist");

            var _users = await _context.Users.FirstOrDefaultAsync( x => x.UserName == userName.ToLower() );
            return Ok(_users);
        }

        [HttpPost("Register")]
        [Authorize]
        public async Task<ActionResult<AppUser>> CreateUser(RegisterUserDto userDto)
        {
            var exist = await _context.Users.AnyAsync( x => x.UserName == userDto.UserName.ToLower() );
            if (exist)  return BadRequest("User Already Exist");

            var _user = new AppUser
            {
                UserName = userDto.UserName.ToLower(),
                GivenName = userDto.GivenName,
                sn = userDto.sn,
                cn = userDto.cn,
                EmployeeNumber = userDto.EmployeeNumber,
                Email = userDto.Email,
                Status = 0
            };
            _context.Users.Add(_user);
            await _context.SaveChangesAsync();
            return Ok(_user);
        }

        [HttpPost("Update")]
        [Authorize]
        public async Task<ActionResult<AppUser>> UpdateUser(UpdateUserDto userDto)
        {
            var exist = await _context.Users.AnyAsync( x => x.UserName == userDto.UserName.ToLower() );
            if (!exist)  return BadRequest("User doesnt Exist");

            var _user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userDto.UserName.ToLower());
            _user.GivenName = userDto.GivenName;
            _user.sn = userDto.sn;
            _user.cn = userDto.cn;
            _user.EmployeeNumber = userDto.EmployeeNumber;
            _user.Email = userDto.Email;
            await _context.SaveChangesAsync();
            return Ok(_user);
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<ActionResult<AppUser>> ChangePassword(ChangePasswrdDto userDto)
        {
            var exist = await _context.Users.AnyAsync( x => x.UserName == userDto.UserName.ToLower() );
            if (!exist)  return BadRequest("User doesnt Exist");
            return Ok("Password Updated");
        }

        [HttpPost("Enable")]
        [Authorize]
        public async Task<ActionResult<AppUser>> EnableUser(UserStatusDto userDto)
        {
            var exist = await _context.Users.AnyAsync( x => x.UserName == userDto.UserName.ToLower() );
            if (!exist)  return BadRequest("User doesnt Exist");

            var _user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userDto.UserName.ToLower());
            _user.Status = 0;
            await _context.SaveChangesAsync();
            return Ok(_user);
        }

        [HttpPost("Disable")]
        [Authorize]
        public async Task<ActionResult<AppUser>> DisableUser(UserStatusDto userDto)
        {
            var exist = await _context.Users.AnyAsync( x => x.UserName == userDto.UserName.ToLower() );
            if (!exist)  return BadRequest("User doesnt Exist");

            var _user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userDto.UserName.ToLower());
            _user.Status = 1;
            await _context.SaveChangesAsync();
            return Ok(_user);
        }

        [HttpPost("Delete")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AppUser>>> DeleteUser(UserStatusDto userDto)
        {
            var exist = await _context.Users.AnyAsync( x => x.UserName == userDto.UserName.ToLower() );
            if (!exist)  return BadRequest("User doesnt Exist");

            var _usersToDelete = await _context.Users.Where( x => x.UserName == userDto.UserName.ToLower()).ToListAsync();
            foreach (var _user in _usersToDelete)
            {
                var _groupMember = await _context.GroupsMembership.Where( x => x.UserName == userDto.UserName.ToLower()).ToListAsync();
                foreach (var _meber in _groupMember)
                {
                    _context.GroupsMembership.Remove(_meber);
                }
                _context.Users.Remove(_user);
            }
            await _context.SaveChangesAsync();
            return Ok(_usersToDelete);
        }

    }
}
