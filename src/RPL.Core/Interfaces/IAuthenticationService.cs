using RPL.Core.DTOs;
using RPL.Core.Result;
using System.Threading.Tasks;

namespace RPL.Core.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Result<RefreshTokenDto>> RefreshTokenAsync(RefreshTokenRequest model);

        Task<IResult> RegisterAsync(RegistrationRequest model, string role);

        Task<IResult> ResendVerificationCodeAsync(VerificationCodeRequest model);

        Task<Result<SignInDto>> SignInAsync(SignInRequest model);

        Task<IResult> SignOutAsync();

        Task<IResult> VerifyAsync(VerificationRequest model);
    }
}
