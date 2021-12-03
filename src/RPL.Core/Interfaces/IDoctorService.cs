using Ardalis.Result;
using RPL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPL.Core.Interfaces
{
    public interface IDoctorService
    {
        Task<Result<Doctor>> CreateDoctorAsync(ApplicationUser model);
    }
}
