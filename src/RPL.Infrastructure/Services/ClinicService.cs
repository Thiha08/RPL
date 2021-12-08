using RPL.Core.Entities;
using RPL.Infrastructure.Interfaces;
using RPL.SharedKernel.Interfaces;

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
