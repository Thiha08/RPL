using RPL.Core.Entities;
using RPL.Core.Result;
using System.Threading.Tasks;

namespace RPL.Core.Interfaces
{
    public interface IPatientService
    {
        Task<Result<Patient>> CreatePatientAsync(ApplicationUser model);
    }
}
