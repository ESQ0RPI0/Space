using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Space.Forms.Registration;
using Space.Registration.DataBase.Context;
using Space.Server.Datamodel.DatabaseModels.Registration;

namespace Space.Registration.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
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
        public IActionResult Register([FromBody]UserRegistrationForm user)
        {
            var hashedPassword = _passwordHasher.HashPassword(user, user.Password);

            var newUser = new User
            {
                Email = user.Email,
                PasswordHash = hashedPassword,
            };
            
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok(newUser);
        }

    }
}
