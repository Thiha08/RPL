using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using RPL.Core.Constants.Identity;
using RPL.Core.DTOs;
using RPL.Core.Interfaces;
using System.Threading.Tasks;

namespace RPL.Ryawgen.Controllers
{

    [Route("api/[controller]")]
    [Produces("application/json")]
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
        ///     POST /Authentication/RefreshToken
        ///     {
        ///        "refreshToken": "..."
        ///     }
        ///
        /// </remarks>
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<RefreshTokenResultDto>> RefreshTokenAsync([FromBody] RefreshTokenRequestDto request)
        {
            return this.ToActionResult(await _authenticationService.RefreshTokenAsync(request));
        }

        /// <summary>
        /// Register a new patient
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Authentication/Register
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
        public async Task<ActionResult<string>> RegisterAsync([FromBody] RegistrationRequestDto request)
        {
            return this.ToActionResult(await _authenticationService.RegisterAsync(request, Roles.Patient));
        }

        /// <summary>
        /// Resend verification code when the old one is expired
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Authentication/ResendVerificationCode
        ///     {
        ///        "phoneNumber": "09424432870", // Phone number starts with '09'
        ///     }
        ///
        /// </remarks>
        [HttpPost("ResendVerificationCode")]
        public async Task<ActionResult<string>> ResendVerificationCodeAsync([FromBody] VerificationCodeRequestDto request)
        {
            return this.ToActionResult(await _authenticationService.ResendVerificationCodeAsync(request));
        }

        /// <summary>
        /// SignIn a patient with his/her phone number and password.
        /// </summary>
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
        [HttpPost("SignIn")]
        public async Task<ActionResult<SignInResultDto>> SignInAsync([FromBody] SignInRequestDto request)
        {
            return this.ToActionResult(await _authenticationService.SignInAsync(request));
        }

        /// <summary>
        /// Verify the user phone number with OTP
        /// </summary>
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
        [HttpPost("Verify")]
        public async Task<ActionResult<string>> VerifyAsync([FromBody] VerificationRequestDto request)
        {
            return this.ToActionResult(await _authenticationService.VerifyAsync(request));
        }
    }
}
