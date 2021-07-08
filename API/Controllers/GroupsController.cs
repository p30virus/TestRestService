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
    public class GroupsController : ApiBaseController
    {
         private readonly DataContext _context;
        public GroupsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Groups")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AppGroup>>> GetGroups()
        {
            return Ok( await _context.Groups.ToListAsync());
        }

        [HttpGet("GroupsMembership/{userName}")]
        [Authorize]
        public async Task<ActionResult<UserMembershipDto>> GetGroupsMembership(string userName)
        {
            var exist = await _context.Users.AnyAsync( x => x.UserName == userName.ToLower());
            if (!exist)  return BadRequest("User doesnt Exist");
            var _users = await _context.Users.FirstOrDefaultAsync( x => x.UserName == userName.ToLower() );
            var _userReturn = new UserMembershipDto 
            {
                UserName = _users.UserName,
                Membership = new List<AppGroupMembership>()
            };
            var _Membership = new List<AppGroupMembership>();
            _Membership = await _context.GroupsMembership.Where( x => x.UserName == userName.ToLower()).ToListAsync();
            if(_Membership != null) _userReturn.Membership = _Membership;
            return Ok(_userReturn);
        }

        [HttpPost("AddMembership")]
        [Authorize]
        public async Task<ActionResult<UserMembershipDto>> AddMembership(UserMembershipModifyDto userDto)
        {
            var exist = await _context.Users.AnyAsync( x => x.UserName == userDto.UserName.ToLower());
            if (!exist)  return BadRequest("User doesnt Exist");
            var _users = await _context.Users.FirstOrDefaultAsync( x => x.UserName == userDto.UserName.ToLower() );
            var _userReturn = new UserMembershipDto 
            {
                UserName = _users.UserName,
                Membership = new List<AppGroupMembership>()
            };
            foreach (var item in userDto.Membership)
            {
                var _GroupExist = await _context.Groups.AnyAsync( x => x.GroupName == item);
                if (!_GroupExist) continue;
                var _MembershipExist = await _context.GroupsMembership.AnyAsync( x => (x.GroupName == item && x.UserName == _userReturn.UserName));
                if (_MembershipExist) continue;
                var _Membership = new AppGroupMembership 
                {
                    UserName = _users.UserName,
                    GroupName = item
                };
                _context.GroupsMembership.Add(_Membership);
            }
            await _context.SaveChangesAsync();
            var _returnMembership = await _context.GroupsMembership.Where( x => x.UserName == _userReturn.UserName.ToLower()).ToListAsync();
            if(_returnMembership != null) _userReturn.Membership = _returnMembership;
            return Ok(_userReturn);
        }

         [HttpPost("RemoveMembership")]
         [Authorize]
        public async Task<ActionResult<UserMembershipDto>> RemoveMembership(UserMembershipModifyDto userDto)
        {
            var exist = await _context.Users.AnyAsync( x => x.UserName == userDto.UserName.ToLower());
            if (!exist)  return BadRequest("User doesnt Exist");
            var _users = await _context.Users.FirstOrDefaultAsync( x => x.UserName == userDto.UserName.ToLower() );
            var _userReturn = new UserMembershipDto 
            {
                UserName = _users.UserName,
                Membership = new List<AppGroupMembership>()
            };
            foreach (var item in userDto.Membership)
            {
                var _GroupExist = await _context.Groups.AnyAsync( x => x.GroupName == item);
                if (!_GroupExist) continue;
                var _Membership = await _context.GroupsMembership.FirstOrDefaultAsync(  x => (x.GroupName == item && x.UserName == _userReturn.UserName) );
                if (_Membership == null) continue;
                _context.GroupsMembership.Remove(_Membership);
            }
            await _context.SaveChangesAsync();
            var _returnMembership = await _context.GroupsMembership.Where( x => x.UserName == _userReturn.UserName.ToLower()).ToListAsync();
            if(_returnMembership != null) _userReturn.Membership = _returnMembership;
            return Ok(_userReturn);
        }
    }
}