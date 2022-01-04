using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPL.Core.Constants.Identity;
using RPL.Core.DTOs;
using RPL.Core.Result;
using RPL.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace RPL.Larban.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Refresh token to get a new access token
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Authentication/RefreshToken
        ///     {
        ///        "refreshToken": "..."
        ///     }
        ///
        /// </remarks>
        [HttpPost("RefreshToken")]
        [ProducesResponseType(typeof(Result<RefreshTokenDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
        {
            return Ok(await _authenticationService.RefreshTokenAsync(request));
        }

        /// <summary>
        /// Register a new patient
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Authentication/Register
        ///     {
        ///        "phoneNumber": "09424432870", // Phone number starts with '09'
        ///        "password": "MyPassword123!",
        ///        "fullName": "Thiha",
        ///        "dateOfBirth": "1990-01-20T08:24:37.948Z",
        ///        "address": "No.1, 1st Street, 1st Ward, Yangon" // nullable
        ///     }
        ///
        /// </remarks>
        [HttpPost("Register")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequest request)
        {
            return Ok(await _authenticationService.RegisterAsync(request, Roles.Admin));
        }

        /// <summary>
        /// Resend verification code when the old one is expired
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Authentication/ResendVerificationCode
        ///     {
        ///        "phoneNumber": "09424432870", // Phone number starts with '09'
        ///     }
        ///
        /// </remarks>
        [HttpPost("ResendVerificationCode")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> ResendVerificationCodeAsync([FromBody] VerificationCodeRequest request)
        {
            return Ok(await _authenticationService.ResendVerificationCodeAsync(request));
        }

        /// <summary>
        /// SignIn a patient with his/her phone number and password.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Authentication/SignIn
        ///     {
        ///        "phoneNumber": "09424432870", // Phone number starts with '09'
        ///        "password": "welcome123"
        ///     }
        ///
        /// </remarks>
        [HttpPost("SignIn")]
        [ProducesResponseType(typeof(Result<SignInDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest request)
        {
            return Ok(await _authenticationService.SignInAsync(request));
        }

        /// <summary>
        /// Verify the user phone number with OTP
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Authentication/Verify
        ///     {
        ///        "phoneNumber": "09424432870", // Phone number starts with '09'
        ///        "verificationCode": "123456"  // six digits number 
        ///     }
        ///
        /// </remarks>
        [HttpPost("Verify")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> VerifyAsync([FromBody] VerificationRequest request)
        {
            return Ok(await _authenticationService.VerifyAsync(request));
        }
    }
}
