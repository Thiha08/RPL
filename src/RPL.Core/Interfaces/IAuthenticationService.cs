using Ardalis.Result;
using Newtonsoft.Json.Linq;
using RPL.Core.DTOs;
using System.Threading.Tasks;

namespace RPL.Core.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Result<SignInResponseDto>> SignInAsync(SignInRequestDto model);

        Task<Result<RegistrationResponseDto>> RegisterAsync(RegistrationRequestDto model, string role);

        Task<Result<string>> ResendVerificationCodeAsync(string phoneNumber);

        Task<IResult> SignOutAsync();
    }
}
