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
    public class RegisterEndpoint : BaseAsyncEndpoint
        .WithRequest<RegistrationRequestDto>
        .WithResponse<RegistrationResponseDto>
    {
        private readonly IAuthenticationService _authenticationService;

        public RegisterEndpoint(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Authentication/Register
        ///     {
        ///        "phoneNumber": "09424432870", // Phone number starts with '09'
        ///        "password": "MyPassword123!"
        ///        "fullName": "Thiha",
        ///        "dateOfBirth": "1990-01-20T08:24:37.948Z",
        ///        "address": "No.1, 1st Street, 1st Ward, Yangon", // nullable
        ///     }
        ///
        /// </remarks>
        [HttpPost("/Authentication/Register")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Patient Registration",
            Description = "Patient Registration",
            OperationId = "Authentication.Register",
            Tags = new[] { "AuthenticationEndpoints" })
        ]
        public async override Task<ActionResult<RegistrationResponseDto>> HandleAsync(RegistrationRequestDto request, CancellationToken cancellationToken = default)
        {
            return Ok(await _authenticationService.RegisterAsync(request, Roles.Patient));
        }
    }
}
