using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using RPL.Core.Constants.Identity;
using RPL.Core.DTOs;
using RPL.Core.Entities;
using RPL.Core.Result;
using RPL.Core.Settings.Identity;
using RPL.Infrastructure.IntegrationServices.Interfaces;
using RPL.Infrastructure.Services.Interfaces;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityConstants = RPL.Core.Constants.Identity.IdentityConstants;

namespace RPL.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IStringLocalizer<AuthenticationService> _stringLocalizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentitySettings _identitySettings;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly ISmsSender _smsSender;

        public AuthenticationService(
            IStringLocalizer<AuthenticationService> stringLocalizer,
            UserManager<ApplicationUser> userManager,
            IOptions<IdentitySettings> identitySettingsAccessor,
            IPatientService patientService,
            IDoctorService doctorService,
            ISmsSender smsSender)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _identitySettings = identitySettingsAccessor.Value;
            _patientService = patientService;
            _doctorService = doctorService;
            _smsSender = smsSender;
        }

        public async Task<Result<RefreshTokenDto>> RefreshTokenAsync(Core.DTOs.RefreshTokenRequest model)
        {
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

            if (disco.IsError)
                return Result<RefreshTokenDto>.BadRequest(disco.Error);

            var tokenResponse = await client.RequestRefreshTokenAsync(new IdentityModel.Client.RefreshTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = _identitySettings.ClientId,
                ClientSecret = _identitySettings.ClientSecret,
                Scope = _identitySettings.Scope + " offline_access",

                RefreshToken = model.RefreshToken
            });

            if (tokenResponse.IsError)
                return Result<RefreshTokenDto>.BadRequest(tokenResponse.Error);

            return Result<RefreshTokenDto>.Ok(new RefreshTokenDto
            {
                AccessToken = tokenResponse.AccessToken,
                ExpiresIn = tokenResponse.ExpiresIn,
                TokenType = tokenResponse.TokenType,
                RefreshToken = tokenResponse.RefreshToken,
                Scope = tokenResponse.Scope
            });
        }

        public async Task<IResult> RegisterAsync(RegistrationRequest model, string role)
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

            if (existingUser != null)
                return Result.BadRequest(_stringLocalizer["Account already exists."].Value);


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

            if (!userResult.Succeeded)
                return Result.BadRequest();

            await _userManager.AddClaimsAsync(newUser, new[]
            {
                new Claim(JwtClaimTypes.Name, newUser.UserName),
                new Claim(JwtClaimTypes.Role, role)
            });

            if (role == Roles.Patient)
            {
                var patientResult = await _patientService.CreatePatientAsync(newUser);
            }
            else if (role == Roles.Doctor)
            {
                var doctorResult = await _doctorService.CreateDoctorAsync(newUser);
            }

            await _smsSender.SendSMSAsync(newUser.PhoneNumber, $"{_stringLocalizer["RPL: Your verification code is"].Value} : {newUser.VerificationCode}.");

            return Result.Ok($"{_stringLocalizer["Verification code sent to"].Value} {newUser.PhoneNumber}.");
        }

        public async Task<IResult> ResendVerificationCodeAsync(VerificationCodeRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.PhoneNumber);

            if (user == null)
                return Result.BadRequest(_stringLocalizer["User account does not exist."].Value);

            if (user.PhoneNumberConfirmed)
                return Result.BadRequest(_stringLocalizer["User account is alrady verified."].Value);

            user.VerificationCode = IdentityConstants.GenerateVerificationCode;
            user.VerificationCodeExpiryDate = DateTime.UtcNow.AddSeconds(IdentityConstants.VerificationCodeExpirySeconds);

            await _userManager.UpdateAsync(user);

            await _smsSender.SendSMSAsync(user.PhoneNumber, $"{_stringLocalizer["RPL: Your verification code is"].Value} : {user.VerificationCode}");

            return Result.Ok($"{_stringLocalizer["Verification code sent to"].Value} {user.PhoneNumber}.");
        }

        public async Task<Result<SignInDto>> SignInAsync(SignInRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.PhoneNumber);

            if (user == null)
                return Result<SignInDto>.BadRequest(_stringLocalizer["Phone number or password is wrong."].Value);

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

            if (disco.IsError)
                return Result<SignInDto>.BadRequest(disco.Error);

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = _identitySettings.ClientId,
                ClientSecret = _identitySettings.ClientSecret,
                Scope = _identitySettings.Scope + " offline_access",

                UserName = model.PhoneNumber,
                Password = model.Password
            });

            if (tokenResponse.IsError)
                return Result<SignInDto>.BadRequest(tokenResponse.Error);

            return Result<SignInDto>.Ok(new SignInDto
            {
                AccessToken = tokenResponse.AccessToken,
                ExpiresIn = tokenResponse.ExpiresIn,
                TokenType = tokenResponse.TokenType,
                RefreshToken = tokenResponse.RefreshToken,
                Scope = tokenResponse.Scope
            });
        }

        public Task<IResult> SignOutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> VerifyAsync(VerificationRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.PhoneNumber);

            if (user == null)
                return Result.BadRequest(_stringLocalizer["User account does not exist."].Value);

            if (user.Status == false)
                return Result.BadRequest(_stringLocalizer["User account is deactivated."].Value);

            if (user.PhoneNumberConfirmed == true)
                return Result.BadRequest(_stringLocalizer["User account is alrady verified."].Value);

            if (user.VerificationCode != model.VerificationCode)
                return Result.BadRequest(_stringLocalizer["Invalid verification code."].Value);

            if (user.VerificationCodeExpiryDate < DateTime.UtcNow)
                return Result.BadRequest(_stringLocalizer["Expired verification code."].Value);

            user.PhoneNumberConfirmed = true;
            await _userManager.UpdateAsync(user);

            return Result.Ok(_stringLocalizer["User account is verified successfully."].Value);
        }
    }
}
