using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SnakeTBs.Identity.Api.Authentications.Models.Constants;
using SnakeTBs.Identity.DataLayer.Models.Contexts.Entities;
using SnakeTBs.Identity.DataLayer.Models.Tables;
using SnakeTBs.Identity.DataLayer.Models.Tables.Entities;
using SnakeTBs.Identity.LogicLayer.Models.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeTBs.Identity.Api.Controllers.AdminsControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class AdminsController : ControllerBase
    {
        private readonly ILogger<AdminsController> _logger;
        private readonly DatabaseContext _context;
        public AdminsController(ILogger<AdminsController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await _context.Users.Where(v => v.Role == RoleUserTable.Admin).ToListAsync();
            return Ok(result);
        }
    }
}
