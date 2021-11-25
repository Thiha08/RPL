using Ardalis.Result;
using RPL.Core.Entities;
using System.Threading.Tasks;

namespace RPL.Core.Interfaces
{
    public interface IPatientService
    {
        Task<Result<Patient>> CreatePatientAsync(ApplicationUser model);
    }
}
