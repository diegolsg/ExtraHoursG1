using Azure;
using ExtraHours.Core.Dto;
using ExtraHours.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExtraHours.Api.Controllers
{
    [ApiController]
    [Route("api/hour")]
    public class ExtraHourController : ControllerBase
    {
        readonly IExtraHourService _extraHourService;
        public ExtraHourController(IExtraHourService extraHourService)
        {
            _extraHourService = extraHourService;
        }
    }
}
