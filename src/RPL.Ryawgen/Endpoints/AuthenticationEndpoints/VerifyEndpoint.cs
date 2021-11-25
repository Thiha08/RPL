using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPL.Core.DTOs;
using RPL.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace RPL.Ryawgen.Endpoints.AuthenticationEndpoints
{
    public class VerifyEndpoint : BaseAsyncEndpoint
        .WithRequest<VerificationRequestDto>
        .WithoutResponse
    {
        private readonly IUserService _userService;

        public VerifyEndpoint(IUserService userService)
        {
            _userService = userService;
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Authentication/Verify
        ///     {
        ///        "phoneNumber": "09424432870", // Phone number starts with '09'
        ///        "verificationCode": "123456"  // six digits number 
        ///     }
        ///
        /// </remarks>
        [AllowAnonymous]
        [HttpPost("/Authentication/Verify")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Patient Verify",
            Description = "Patient Verify",
            OperationId = "Authentication.Verify",
            Tags = new[] { "AuthenticationEndpoints" })
        ]
        public override async Task<ActionResult> HandleAsync(VerificationRequestDto request, CancellationToken cancellationToken = default)
        {
            return Ok(await _userService.VerifyUserPhoneNumberAsync(request.PhoneNumber, request.VerificationCode));
        }
    }
}
