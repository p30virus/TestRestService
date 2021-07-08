using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
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
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return Ok( await _context.Users.ToListAsync());
        }

        [HttpPost("Register")]
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
        public async Task<ActionResult<AppUser>> ChangePassword(ChangePasswrdDto userDto)
        {
            var exist = await _context.Users.AnyAsync( x => x.UserName == userDto.UserName.ToLower() );
            if (!exist)  return BadRequest("User doesnt Exist");
            return Ok("Password Updated");
        }

        [HttpPost("Enable")]
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