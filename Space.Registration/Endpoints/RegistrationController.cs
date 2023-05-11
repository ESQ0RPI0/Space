using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Space.Forms.Registration;
using Space.Server.Services.Registration;
using Space.Shared.Api.ApiResults;

namespace Space.Registration.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        private readonly UserAuthorizationService _userAuthorizationService;

        public RegistrationController(UserAuthorizationService userAuthorizationService)
        {
            _userAuthorizationService = userAuthorizationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ServerResult<bool>> Register([FromBody] UserRegistrationForm user)
        {
            var result = await _userAuthorizationService.Registration(user);

            return result;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ServerResult<bool>> IsEmailUnique([FromBody] string email)
        {
            var result = await _userAuthorizationService.IsEmailUnique(email);
            return result;
        }
    }
}
