using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RPL.Core.DTOs.Patients;
using RPL.Core.Entities;
using RPL.Infrastructure.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace RPL.Ryawgen.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IPatientService _patientService;

        public PatientInfo currentPatientInfo;

        public BaseController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool isCurrentPatientLoggedIn = User != null && User.Claims != null && User.Claims.Any();

            if (!isCurrentPatientLoggedIn)
            {
                currentPatientInfo = null;
            }
            else
            {
                Patient patient = await _patientService.GetPatientByUserIdAsync(User.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Subject).Value);

                currentPatientInfo = new PatientInfo
                {
                    PatientId = patient.Id,
                    UserId = patient.UserId,
                    UserName = patient.PhoneNumber,
                    FullName = patient.Name,
                    PhoneNumber = patient.PhoneNumber,
                    Status = patient.Status
                };
            }

            await next();
        }
    }
}
