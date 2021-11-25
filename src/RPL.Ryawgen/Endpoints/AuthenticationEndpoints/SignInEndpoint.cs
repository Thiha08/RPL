using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPL.Core.DTOs;
using RPL.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RPL.Ryawgen.Endpoints.AuthenticationEndpoints
{
    public class SignInEndpoint : BaseAsyncEndpoint
        .WithRequest<SignInRequestDto>
        .WithResponse<SignInResponseDto>
    {
        private readonly IAuthenticationService _authenticationService;

        public SignInEndpoint(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Authentication/SignIn
        ///     {
        ///        "phoneNumber": "09424432870", // Phone number starts with '09'
        ///        "password": "welcome123"
        ///     }
        ///
        /// </remarks>
        [AllowAnonymous]
        [HttpPost("/Authentication/SignIn")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Patient SignIn",
            Description = "Patient SignIn",
            OperationId = "Authentication.SignIn",
            Tags = new[] { "AuthenticationEndpoints" })
        ]
        public override async Task<ActionResult<SignInResponseDto>> HandleAsync([FromBody] SignInRequestDto request, CancellationToken cancellationToken = default)
        {
            return Ok(await _authenticationService.SignInAsync(request));
        }
    }
}
