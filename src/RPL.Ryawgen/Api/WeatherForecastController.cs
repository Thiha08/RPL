using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RPL.Ryawgen.Api
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : BaseApiController
    {
    }
}
