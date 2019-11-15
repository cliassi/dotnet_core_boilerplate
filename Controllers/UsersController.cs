using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using API.Data;
using API.Dtos;
using API.Helpers;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    //[ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

        public UsersController(IMapper mapper, DataContext context, UserManager<User> userManager)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        public class UserSearchDto
        {
            public Nullable<int> OrganizationId { get; set; }
            public Nullable<int> FacilityId { get; set; }
            public Nullable<int> DepartmentId { get; set; }
            public Nullable<int> StoreId { get; set; }
        }
        [HttpPost]
        public async Task<ActionResult> GetUserList([FromBody] UserSearchDto userSearchDto)
        {
            List<User> users = new List<User>();
            if (userSearchDto.OrganizationId != null)
            {
                users = await _context.Users.Where(u => u.OrganizationId == userSearchDto.OrganizationId).ToListAsync();
            }
            else if (userSearchDto.FacilityId != null)
            {
                users = await _context.Users.Where(u => u.FacilityId == userSearchDto.FacilityId).ToListAsync();
            }
            else if (userSearchDto.DepartmentId != null)
            {
                users = await _context.Users.Where(u => u.DepartmentId == userSearchDto.DepartmentId).ToListAsync();
            }
            else if (userSearchDto.StoreId != null)
            {
                users = await _context.Users.Where(u => u.StoreId == userSearchDto.StoreId).ToListAsync();
            }
            else
            {
                users = await _context.Users.Where(u => u.OrganizationId == null).ToListAsync();
            }

            return Ok(users);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            User _user = _context.Users.Find(userId);
            return Ok(new
            {
                user = _user,
                role = _user.Role
            });
        }
        [HttpPost("change_password")]
        public async Task<ActionResult> changePassword([FromBody]ChangePasswordDto passwords)
        {
            //return Ok(passwords);

            try
            {
                int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                User user = await _context.Users.Where(u => u.Id == currentUserId).FirstOrDefaultAsync();


                if (user == null)
                {
                    return NotFound();
                }
                else
                {

                    IdentityResult result = await _userManager.ChangePasswordAsync(user, passwords.Password, passwords.NewPassword);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest(result.Errors.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok();
            }


        }
        
        [HttpGet(Name = "getusers")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return await _context.Users.Where(sb => sb.Id == currentUserId).ToListAsync();
        }

        [HttpGet("getgsas")]
        public async Task<ActionResult<List<User>>> GetGSAs()
        {
            return await _context.Users.Where(sb => sb.Role == "GSA").ToListAsync();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}