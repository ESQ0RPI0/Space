using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Space.Forms.Registration;
using Space.Registration.DataBase.Context;
using Space.Server.Datamodel.DatabaseModels.Registration;
using Space.Shared.Api.ApiResults;
using static Space.Shared.Common.Server.ServerTypes;

namespace Space.Server.Services.Registration
{
    public class UserAuthorizationService
    {
        private readonly UsersContext _context;
        private readonly IPasswordHasher<UserRegistrationForm> _passwordHasher;

        public UserAuthorizationService(UsersContext context, IPasswordHasher<UserRegistrationForm> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<ServerResult<bool>> Registration(UserRegistrationForm user)
        {
            var isEmailUnique = await IsEmailUnique(user.Email);

            if (!isEmailUnique)
            {
                return ServerErrorCodes.EmailAlreadyInUse.WithExtras<bool>("Email is already in use."); 
            }
            var hashedPassword = _passwordHasher.HashPassword(user, user.Password);

            var newUser = new User
            {
                Email = user.Email,
                PasswordHash = hashedPassword,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return true.ToServerResult("super");
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            return await _context.Users.AllAsync(u => u.Email != email);
        }
    }
}
