using Ardalis.Result;
using RPL.Core.DTOs;
using RPL.Core.Entities;
using RPL.Core.Filters;
using RPL.Core.Specifications;
using RPL.Infrastructure.Interfaces;
using RPL.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IRepository<Clinic> _clinicRepository;

        public ClinicService(IRepository<Clinic> clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        
    }
}
