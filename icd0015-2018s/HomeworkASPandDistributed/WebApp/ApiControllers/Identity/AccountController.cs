using System.Threading.Tasks;
using ee.itcollege.nikita.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PublicApi.v1.DTO;
using WebApp.Areas.Identity.Pages.Account;
using AppUser = Domain.Identity.AppUser;

namespace WebApp.ApiControllers.Identity
{
    /// <summary>
    /// Controller for handling accounts.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Account controller constructor.
        /// </summary>
        /// <param name="signInManager">Identity SignInManager.</param>
        /// <param name="emailSender">Identity Email Sender interface.</param>
        /// <param name="configuration">Key/value application configuration interface.</param>
        /// <param name="logger">Logging interface.</param>
        /// <param name="userManager">Identity UserManager.</param>
        public AccountController(SignInManager<AppUser> signInManager, IEmailSender emailSender, IConfiguration configuration, ILogger<RegisterModel> logger, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _emailSender = emailSender;
            _configuration = configuration;
            _logger = logger;
            _userManager = userManager;
        }

        /// <summary>
        /// Handle identity user login.
        /// </summary>
        /// <param name="model">Data transfer object with login information.</param>
        /// <response code="200">User successfully logged in.</response>
        /// <response code="403">User is not found or password is check was not successful.</response>
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] Login model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            
            if (appUser == null)
            {
                // user is not found, return 403
                _logger.LogInformation("User not found.");
                return StatusCode(403);
            }
            
            // do not log user in, just check that the password is ok
            var result = await _signInManager.CheckPasswordSignInAsync(appUser, model.Password, false);

            if (result.Succeeded)
            {
                // create claims based user 
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
          
                // get the Json Web Token
                var jwt = JwtHelper.GenerateJwt(
                    claimsPrincipal.Claims, 
                    _configuration["JWT:Key"], 
                    _configuration["JWT:Issuer"], 
                    int.Parse(_configuration["JWT:ExpireDays"]));
                _logger.LogInformation("Token generated for user");
                return Ok(new {token = jwt});
            }

            return StatusCode(403);

        }
        
        /// <summary>
        /// Handle identity user registration.
        /// </summary>
        /// <param name="model">Data transfer object with registration information.</param>
        /// <response code="200">User successfully registered/created.</response>
        /// <response code="406">Not acceptable.</response>
        /// <response code="400">Data provided via data transfer object was not valid.</response>
        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] Register model)
        {
            
            if (ModelState.IsValid)
            {
                var appUser = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = await _userManager.CreateAsync(appUser, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("New user created.");
                    
                    // create claims based user 
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
          
                    // get the Json Web Token
                    var jwt = JwtHelper.GenerateJwt(
                        claimsPrincipal.Claims, 
                        _configuration["JWT:Key"], 
                        _configuration["JWT:Issuer"], 
                        int.Parse(_configuration["JWT:ExpireDays"]));
                    _logger.LogInformation("Token generated for user");
                    return Ok(new {token = jwt});
                    
                }
                return StatusCode(406); //406 Not Acceptable
            }
            
            return StatusCode(400); //400 Bad Request

        }
    }
}