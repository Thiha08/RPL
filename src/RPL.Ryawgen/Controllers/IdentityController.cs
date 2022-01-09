using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPL.Infrastructure.Services.Interfaces;
using System.Linq;

namespace RPL.Ryawgen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class IdentityController : BaseController
    {
        public IdentityController(IPatientService patientService)
            : base(patientService)
        {

        }

        /// <summary>
        /// Test identity server 4 auth
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/identity
        ///
        /// </remarks>
        [HttpGet]
        public ActionResult Get()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return new JsonResult(claims);
        }
    }
}
