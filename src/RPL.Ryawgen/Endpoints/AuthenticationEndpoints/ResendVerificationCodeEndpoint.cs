using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using RPL.Core.Constants.Identity;
using RPL.Core.DTOs;
using RPL.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace RPL.Ryawgen.Endpoints.AuthenticationEndpoints
{
    public class ResendVerificationCodeEndpoint : BaseAsyncEndpoint
        .WithRequest<VerificationCodeRequestDto>
        .WithResponse<RegistrationResponseDto>
    {
        private readonly IAuthenticationService _authenticationService;

        public ResendVerificationCodeEndpoint(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Authentication/ResendVerificationCode
        ///     {
        ///        "phoneNumber": "09424432870", // Phone number starts with '09'
        ///     }
        ///
        /// </remarks>
        [HttpPost("/Authentication/ResendVerificationCode")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Resend Verification Code",
            Description = "Resend Verification Code",
            OperationId = "Authentication.ResendVerificationCode",
            Tags = new[] { "AuthenticationEndpoints" })
        ]
        public async override Task<ActionResult<RegistrationResponseDto>> HandleAsync(VerificationCodeRequestDto request, CancellationToken cancellationToken = default)
        {
            return Ok(await _authenticationService.ResendVerificationCodeAsync(request.PhoneNumber));
        }
    }
}
