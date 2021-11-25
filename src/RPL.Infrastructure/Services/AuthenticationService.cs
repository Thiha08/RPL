using Ardalis.Result;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RPL.Core.Constants.Identity;
using RPL.Core.DTOs;
using RPL.Core.Entities;
using RPL.Core.Interfaces;
using RPL.Core.Settings.Identity;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityConstants = RPL.Core.Constants.Identity.IdentityConstants;

namespace RPL.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentitySettings _identitySettings;
        private readonly IUserService _userService;
        private readonly IPatientService _patientService;
        private readonly ISmsSender _smsSender;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            IOptions<IdentitySettings> identitySettingsAccessor,
            IUserService userService,
            IPatientService patientService,
            ISmsSender smsSender)
        {
            _userManager = userManager;
            _identitySettings = identitySettingsAccessor.Value;
            _userService = userService;
            _patientService = patientService;
            _smsSender = smsSender;
        }

        public async Task<Result<SignInResponseDto>> SignInAsync(SignInRequestDto model)
        {
            var user = await _userManager.FindByNameAsync(model.PhoneNumber);

            if (user == null) return Result<SignInResponseDto>.Error(new[] { "Phone number or password is wrong." });

            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _identitySettings.IdentityServerUrl,
                Policy =
                {
                    RequireHttps = false
                }
            });

            if (disco.IsError) return Result<SignInResponseDto>.Error(new[] { disco.Error });

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = _identitySettings.ClientId,
                ClientSecret = _identitySettings.ClientSecret,
                Scope = _identitySettings.Scope + " offline_access",

                UserName = model.PhoneNumber,
                Password = model.Password
            });

            if (tokenResponse.IsError) return Result<SignInResponseDto>.Error(new[] { tokenResponse.Error });

            return new Result<SignInResponseDto>(new SignInResponseDto
            {
                AccessToken = tokenResponse.AccessToken,
                ExpiresIn = tokenResponse.ExpiresIn,
                TokenType = tokenResponse.TokenType,
                RefreshToken = tokenResponse.RefreshToken,
                Scope = tokenResponse.Scope
            });
        }

        public async Task<Result<RegistrationResponseDto>> RegisterAsync(RegistrationRequestDto model, string role)
        {
            var existingUser = await _userManager.FindByNameAsync(model.PhoneNumber);

            //if (user?.PhoneNumberConfirmed == false)
            //{
            //    user.VerificationCode = IdentityConstants.GenerateVerificationCode;
            //    user.VerificationCodeExpiryDate = DateTime.UtcNow.AddSeconds(IdentityConstants.VerificationCodeExpirySeconds);
            //    await _userManager.UpdateAsync(user);

            //    await _smsSender.SendSMSAsync(user.PhoneNumber, $"RPL: Your verification code is: { user.VerificationCode }");
            //    return Ok(new { IsSuccess = true, SuccessMessage = $"Verification code sent to { user.PhoneNumber } again." });

            //    return new Result<RegistrationResponseDto>(new RegistrationResponseDto
            //    {
            //        UserName = user.UserName,
            //        PhoneNumber = user.PhoneNumber
            //    });
            //}

            if (existingUser != null) return Result<RegistrationResponseDto>.Error(new[] { "Account already exists." });

            var newUser = new ApplicationUser
            {
                UserName = model.PhoneNumber,
                PhoneNumber = model.PhoneNumber,
                PhoneNumberConfirmed = false,
                FullName = model.FullName,
                IsResetPasswordUponLoginNeeded = false,
                VerificationCode = IdentityConstants.GenerateVerificationCode,
                VerificationCodeExpiryDate = DateTime.UtcNow.AddSeconds(180),
                Status = true,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            var userResult = await _userManager.CreateAsync(newUser, model.Password);

            if (!userResult.Succeeded) return Result<RegistrationResponseDto>.Error(userResult.Errors?.Select(e => e.Description).ToArray());

            await _userManager.AddClaimsAsync(newUser, new[]
            {
                new Claim(JwtClaimTypes.Name, newUser.UserName),
                new Claim(JwtClaimTypes.Role, role)
            });

            if (role == Roles.Patient)
            {
                var patientResult = await _patientService.CreatePatientAsync(newUser);

                await _smsSender.SendSMSAsync(newUser.PhoneNumber, $"RPL: Your verification code is: { newUser.VerificationCode }.");
            }

            return Result<RegistrationResponseDto>.Success(new RegistrationResponseDto
            {
                UserName = newUser.UserName,
                PhoneNumber = newUser.PhoneNumber
            }, $"Verification code sent to { newUser.PhoneNumber }.");
        }

        public async Task<Result<string>> ResendVerificationCodeAsync(string phoneNumber)
        {
            var user = await _userManager.FindByNameAsync(phoneNumber);

            if (user == null) return Result<string>.Error(new[] { "User account does not exist." });

            if (user.PhoneNumberConfirmed) return Result<string>.Error(new[] { "User account is alrady verified." });

            user.VerificationCode = IdentityConstants.GenerateVerificationCode;
            user.VerificationCodeExpiryDate = DateTime.UtcNow.AddSeconds(IdentityConstants.VerificationCodeExpirySeconds);

            await _userManager.UpdateAsync(user);

            await _smsSender.SendSMSAsync(user.PhoneNumber, $"RPL: Your verification code is: { user.VerificationCode }");
            
            return Result<string>.Success("", $"Verification code sent to { user.PhoneNumber }.");
        }

        public Task<IResult> SignOutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
