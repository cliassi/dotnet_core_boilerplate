using System.Collections.Generic;
using System.Linq;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using API.Models;
using API.Controllers;

namespace API.Data
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        private DataContext _context;

        public Seed(DataContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedUsers()
        {
            if (!_userManager.Users.Any())
            {
                //Seed Roles
                var roles = new List<Role>
                {
                    new Role{Name = "Admin"},
                };

                foreach (var role in roles)
                {
                    _roleManager.CreateAsync(role).Wait();
                }
                //Add Admin User
                var adminUser = new User
                {
                    UserName = "Admin",
                    KnownAs = "Administrator"
                };
                adminUser.Role = "Admin";
                IdentityResult result = _userManager.CreateAsync(adminUser, "password").Result;

                //if (result.Succeeded)
                //{
                //    var admin = _userManager.FindByNameAsync("Admin").Result;
                //    _userManager.AddToRolesAsync(admin, new[] { "Admin" }).Wait();
                //}                

                //Seed User Data
                //var userData = System.IO.File.ReadAllText("Data/UsersSeedData.json");
                //var users = JsonConvert.DeserializeObject<List<UserDto>>(userData);

                //foreach (var user in users)
                //{
                //    if (user.Password.Length < 6)
                //    {
                //        //user.Password = "password";
                //    }
                //    User u = new User();
                //    u.UserName = user.Username;
                //    u.KnownAs = user.KnownAs;
                //    u.Created = user.Created;
                //    u.LastActive = user.LastActive;
                //    u.Role = user.Role;

                //    result = _userManager.CreateAsync(u,user.Password).Result;
                //    //if (result.Succeeded)
                //    //{
                //    //    var userAdded = _userManager.FindByNameAsync(u.UserName).Result;
                //    //    _userManager.AddToRolesAsync(userAdded, new[] { user.Role }).Wait();
                //    //}
                //}   

                SeedOthers();
            }
            TaskController taskController = new TaskController(_context);
            taskController.UpdateDatabase();
        }

        public void SeedOthers()
        {
            
        }
    }

    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string KnownAs { get; set; }
        public int Id { get; set; }
        public int Owner { get; set; }
        public string Role { get; set; }
    }
}