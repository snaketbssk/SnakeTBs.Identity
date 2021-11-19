using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SnakeTBs.Identity.Api.Authentications.Models.Constants;
using SnakeTBs.Identity.DataLayer.Models.Contexts.Entities;
using SnakeTBs.Identity.DataLayer.Models.Tables.Entities;
using SnakeTBs.Identity.LogicLayer.Models.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnakeTBs.Identity.Api.Controllers.PermissionsControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class PermissionsController : ControllerBase
    {
        private readonly ILogger<PermissionsController> _logger;
        private readonly DatabaseContext _context;
        public PermissionsController(ILogger<PermissionsController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PermissionTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _context.Permissions.ToListAsync();
            return Ok(result);
        }
    }
}
