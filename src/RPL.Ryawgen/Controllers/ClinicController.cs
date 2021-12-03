using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPL.Core.DTOs;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace RPL.Ryawgen.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class ClinicController : BaseController
    {
        /// <summary>
        /// Get a list of clinic nearby with scheduled doctors.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Clinic/jobs
        ///
        /// </remarks>
        /// <returns>A JSON array containing the jobs assigned to the driver.</returns>
        //[HttpGet]
        //[Route("jobs")]
        //[Produces("application/json")]
        //[SwaggerResponse(200, Type = typeof(ClinicNearbyDto))]
        //public async Task<IActionResult> Jobs()
        //{
        //    //try
        //    //{
        //    //    if (string.IsNullOrWhiteSpace(currentDriverInfo.PrimeMoverNo))
        //    //    {
        //    //        return Json(new { success = false, message = ConstantDriverMessages.NoPrimeMoverAssigned });
        //    //    }

        //    //    var result = await _driverJobService.GetMobileDriverJobsByDriverIdAsync(currentDriverInfo.DriverId, currentDriverInfo.CompanyId);

        //    //    return Json(new { success = result.IsActionSuccessful, message = result.Message, jobs = result.Data });
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return BadRequest(new { message = ex.Message });
        //    //}
        //}
    }
}
