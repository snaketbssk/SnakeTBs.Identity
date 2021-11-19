using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SnakeTBs.Identity.Api.Authentications.Models.Constants;
using SnakeTBs.Identity.Api.Controllers.UserControllers.Requests.Entities;
using SnakeTBs.Identity.DataLayer.Models.Contexts.Entities;
using SnakeTBs.Identity.DataLayer.Models.Tables;
using SnakeTBs.Identity.DataLayer.Models.Tables.Entities;
using SnakeTBs.Identity.LogicLayer.Managers.Entities;
using SnakeTBs.Identity.LogicLayer.Models.Constants;
using System.Threading.Tasks;

namespace SnakeTBs.Identity.Api.Controllers.AdminControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly DatabaseContext _context;
        public AdminController(ILogger<AdminController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(UserTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> PutSignUpAsync([FromBody] SignUpRequest request)
        {
            var userManager = new UserManager(_context);
            var userTableCreater = request.ToUserTableCreater(RoleUserTable.Admin);
            var result = await userManager.CreateAsync(userTableCreater, request.Referral, true, true);
            return Ok(result);
        }
    }
}
