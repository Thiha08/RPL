using Ardalis.Result;
using RPL.Core.DTOs;
using System.Threading.Tasks;

namespace RPL.Core.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Result<RefreshTokenResultDto>> RefreshTokenAsync(RefreshTokenRequestDto model);

        Task<Result<string>> RegisterAsync(RegistrationRequestDto model, string role);

        Task<Result<string>> ResendVerificationCodeAsync(VerificationCodeRequestDto model);

        Task<Result<SignInResultDto>> SignInAsync(SignInRequestDto model);

        Task<IResult> SignOutAsync();

        Task<Result<string>> VerifyAsync(VerificationRequestDto model);
    }
}
