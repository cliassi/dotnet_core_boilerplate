using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using API.Data;
using API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Mandrill;
using Mandrill.Model;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataContext _context;

        public AuthController(IConfiguration config,
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager,
            DataContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
            _context = context;
        }
        //[Authorize(Policy = "RequireAdminRole")]
        [HttpPost("quick_register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var adminUser = new User
            {
                UserName = userForRegisterDto.Username,
                KnownAs = userForRegisterDto.KnownAs
            };
            adminUser.Role = userForRegisterDto.Role;
            adminUser.OrganizationId = userForRegisterDto.OrganizationId;
            adminUser.FacilityId = userForRegisterDto.FacilityId;
            adminUser.DepartmentId = userForRegisterDto.DepartmentId;
            adminUser.StoreId = userForRegisterDto.StoreId;
            IdentityResult result = _userManager.CreateAsync(adminUser, userForRegisterDto.Password).Result;

            // var userToCreate = _mapper.Map<User>(userForRegisterDto);
            // var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            //foreach(var role in userForRegisterDto.Roles)
            //{
            //    await _userManager.AddToRoleAsync(userToCreate, role);
            //    Console.WriteLine(role);
            //}

            // var userToReturn = _mapper.Map<UserForDetailedDto>(userToCreate);

            if (result.Succeeded)
            {
                return Ok(userForRegisterDto);
                /* 
                //var api = new MandrillApi("GxEys5UGhAbpu_dHjVCXtg");
                //var message = new MandrillMessage("support@ugotransfers.com", userForRegisterDto.Email, "Welcome to TripConnX", "Mandril");
                List<MandrillTemplateContent> templateContent = new List<MandrillTemplateContent>();
                //var content = new MandrillTemplateContent();
                //content.Name = "firstname";
                //content.Content = userForRegisterDto.KnownAs;
                templateContent.Add(MTC("firstname", userForRegisterDto.KnownAs));
                //var mresult = await api.Messages.SendTemplateAsync(message, "CEO - Welcome New Members", templateContent);
                NotificationController nc = new NotificationController(_context);
                // await nc.SendTemplateEmailAsync("CEO - Welcome New Members", userForRegisterDto.Email, "Welcome to the Ugo Family", templateContent);
                if (userForRegisterDto.Role.Equals("Consultant"))
                {
                    templateContent = new List<MandrillTemplateContent>();
                    templateContent.Add(MTC("name", userToCreate.KnownAs));
                    templateContent.Add(MTC("username", userToCreate.UserName));
                    templateContent.Add(MTC("password", userForRegisterDto.Password));
                    // await nc.SendTemplateEmailAsync("New Members - Consultant Level  (With Details)", userForRegisterDto.Email, "Ugo Transfers - Registration Confirmation", templateContent);
                }
                else if (userForRegisterDto.Role.Equals("Agency"))
                {
                    templateContent = new List<MandrillTemplateContent>();
                    templateContent.Add(MTC("name", userToCreate.KnownAs));
                    templateContent.Add(MTC("username", userToCreate.UserName));
                    templateContent.Add(MTC("password", userForRegisterDto.Password));
                    // await nc.SendTemplateEmailAsync("New Members - Agency Level (With Details)", userForRegisterDto.Email, "Ugo Transfers - Registration Confirmation", templateContent);
                }
                else if (userForRegisterDto.Role.Equals("Sub-Group"))
                {
                    templateContent = new List<MandrillTemplateContent>();
                    templateContent.Add(MTC("name", userToCreate.KnownAs));
                    templateContent.Add(MTC("username", userToCreate.UserName));
                    templateContent.Add(MTC("password", userForRegisterDto.Password));
                    // await nc.SendTemplateEmailAsync("New Members - Sub Group Level (With Details)", userForRegisterDto.Email, "Ugo Transfers - Registration Confirmation", templateContent);
                }
                else if (userForRegisterDto.Role.Equals("Group"))
                {
                    templateContent = new List<MandrillTemplateContent>();
                    templateContent.Add(MTC("name", userToCreate.KnownAs));
                    templateContent.Add(MTC("username", userToCreate.UserName));
                    templateContent.Add(MTC("password", userForRegisterDto.Password));
                    // await nc.SendTemplateEmailAsync("New Members - Group Level  (With Details)", userForRegisterDto.Email, "Ugo Transfers - Registration Confirmation", templateContent);
                }
                */
                // return CreatedAtRoute("GetUser",
                //     new { controller = "Users", id = userToCreate.Id }, userToReturn);
            }

            return BadRequest(userForRegisterDto);
            // return BadRequest(result.Errors);
        }

        public MandrillTemplateContent MTC(string Name, string Content)
        {
            MandrillTemplateContent content = new MandrillTemplateContent();
            content.Name = Name;
            content.Content = Content;
            return content;
        }

        public class UserSeed
        {
            public string Name { get; set; }
            public string Role { get; set; }
            public string UserName { get; set; }
            public int Id { get; set; }
            public string Password { get; set; }
            public int Country { get; set; }
            public int GroupId { get; set; }
            public int SubGroupId { get; set; }
            public int AgencyId { get; set; }
            public string Email { get; set; }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterGSA(UserRegisterDto userRegisterDto)
        {
            User existing = _context.Users.Where(u => u.UserName.Equals(userRegisterDto.Email)).FirstOrDefault();
            if (existing != null)
            {
                return BadRequest("User already exists");
            }
            else
            {
                UserForRegisterDto userForRegisterDto = new UserForRegisterDto();
                userForRegisterDto.Username = userRegisterDto.Email;
                userForRegisterDto.Password = userRegisterDto.Password;
                userForRegisterDto.KnownAs = userRegisterDto.KnownAs;
                userForRegisterDto.Role = userRegisterDto.Role;
                userForRegisterDto.Email = userRegisterDto.Email;
                userForRegisterDto.OrganizationId = userRegisterDto.OrganizationId;
                userForRegisterDto.FacilityId = userRegisterDto.FacilityId;
                userForRegisterDto.DepartmentId = userRegisterDto.DepartmentId;
                userForRegisterDto.StoreId = userRegisterDto.StoreId;

                return await Register(userForRegisterDto);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDto.Username);

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userForLoginDto.Username.ToUpper());

                var userToReturn = _mapper.Map<UserForListDto>(appUser);
                userToReturn.Scope = new CommonMethods().GetScope(userToReturn);
                return Ok(new
                {
                    token = GenerateJwtToken(appUser).Result,
                    user = userToReturn,
                    role = appUser.Role
                });
            }

            return Unauthorized();
        }

        

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok("Logded Out");
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}