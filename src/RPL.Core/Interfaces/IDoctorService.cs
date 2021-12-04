using RPL.Core.Entities;
using RPL.Core.Result;
using System.Threading.Tasks;

namespace RPL.Core.Interfaces
{
    public interface IDoctorService
    {
        Task<Result<Doctor>> CreateDoctorAsync(ApplicationUser model);
    }
}
