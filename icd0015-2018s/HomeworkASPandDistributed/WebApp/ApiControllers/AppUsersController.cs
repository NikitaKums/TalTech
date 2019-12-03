using System.Threading.Tasks;
using Contracts.BLL.App;
using ee.itcollege.nikita.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicApi.v1.DTO;
using PublicApi.v1.Mappers;

namespace WebApp.ApiControllers
{    
    /// <summary>
    /// Controller for handling User objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppUsersController : ControllerBase
    {
        
        private readonly IAppBLL _bll;

        /// <summary>
        /// AppUsers controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>
        public AppUsersController(IAppBLL bll)
        {
            _bll = bll;
        }

        
        // GET
        /// <summary>
        /// Get currently logged in AppUser object.
        /// </summary>
        /// <returns>Currently logged in AppUser object.</returns>
        [HttpGet]
        [Route("GetSingleUser")]
        public async Task<ActionResult<PublicApi.v1.DTO.AppUser>> GetSingleUser()
        {
            var user = await _bll.AppUsers.GetUserInfo(User.GetUserId());
            return AppUserMapper.MapFromBLL(user);
        }
        
        /// <summary>
        /// Get currently logged in AppUser Shop object.
        /// </summary>
        /// <returns>Currently logged in AppUser Shop object.</returns>
        /// <response code="404">User does not have a Shop associated with him.</response>
        [HttpGet]
        [Route("GetUserShop")]
        public async Task<ActionResult<PublicApi.v1.DTO.AppUserShop>> GetUserShop()
        {
            var user = await _bll.AppUsers.FindAsync(User.GetUserId());
            if (user.ShopId == null)
            {
                return NotFound();
            }
            var result = new AppUserShop()
            {
                ShopId = (int) user.ShopId,
                ShopName = user.Shop.ShopName
            };
            return result;
        }
    }
}