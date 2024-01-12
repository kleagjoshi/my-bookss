using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using my_bookss.Data;
using my_bookss.Data.Models;
using my_bookss.Data.ViewModels.Authentication;

namespace my_bookss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        //inject some services

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,AppDbContext context,IConfiguration configuration)
        {
            _userManager= userManager;
            _roleManager= roleManager;
            _context= context;
            _configuration= configuration;
        }

        //send data from client side
        [HttpPost("register-user")]
        public async Task<IActionResult> Register([FromBody] RegisterVM payload)
        {
            //check if the user exists

            var userExists = await _userManager.FindByEmailAsync(payload.Email);

            if(userExists != null)
            {
                return BadRequest($"user {payload.Email} already exists");
            }

            ApplicationUser newUser = new ApplicationUser()
            {
                Email = payload.Email,
                UserName = payload.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result= await _userManager.CreateAsync(newUser,payload.Password);

            if(!result.Succeeded)
            {
                return BadRequest("User could not be created");
            }

            return Created(nameof(Register), $"User {payload.Email} created");
        }
    }
}
