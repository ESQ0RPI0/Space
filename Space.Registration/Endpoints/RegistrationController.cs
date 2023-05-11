using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Space.Forms.Registration;
using Space.Registration.DataBase.Context;
using Space.Server.Datamodel.DatabaseModels.Registration;

namespace Space.Registration.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        private readonly UsersContext _context;
        private readonly IPasswordHasher<UserRegistrationForm> _passwordHasher;

        public RegistrationController(UsersContext context,
            IPasswordHasher<UserRegistrationForm> passwordHasher
            )
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]UserRegistrationForm user)
        {
            if (!await IsEmailUnique(user.Email))
            {
                return BadRequest("Email is already in use.");
            }

            var hashedPassword = _passwordHasher.HashPassword(user, user.Password);

            var newUser = new User
            {
                Email = user.Email,
                PasswordHash = hashedPassword,
            };
            
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("super");
        }

        private async Task<bool> IsEmailUnique(string email)
        {
            return await _context.Users.AllAsync(u => u.Email != email);
        }

    }
}
