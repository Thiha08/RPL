using RPL.Core.DTOs;
using RPL.Core.Result;
using System.Threading.Tasks;

namespace RPL.Core.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Result<RefreshTokenResultDto>> RefreshTokenAsync(RefreshTokenRequestDto model);

        Task<IResult> RegisterAsync(RegistrationRequestDto model, string role);

        Task<IResult> ResendVerificationCodeAsync(VerificationCodeRequestDto model);

        Task<Result<SignInResultDto>> SignInAsync(SignInRequestDto model);

        Task<IResult> SignOutAsync();

        Task<IResult> VerifyAsync(VerificationRequestDto model);
    }
}
