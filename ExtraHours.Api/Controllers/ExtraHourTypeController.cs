using ExtraHours.Core.dto;
using ExtraHours.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExtraHours.Api.Controllers
{
    [Route("api/hourType")]
    [ApiController]
    public class ExtraHourTypeController : ControllerBase
    {
        readonly IExtraHourTypeService _extraHourTypeService;
        public ExtraHourTypeController(IExtraHourTypeService extraHourTypeService)
        {
            _extraHourTypeService = extraHourTypeService;
        }

        [HttpPost("createExtraHourType")]
        public async Task<IActionResult> CreateExtraHourType([FromBody] ExtraHourTypeDto extraHourTypeDto)
        {
            await _extraHourTypeService.AddAsync(extraHourTypeDto);
            return Ok(extraHourTypeDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExtraHoursTypes()
        {
            var result = await _extraHourTypeService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{typeHourName}")]
        public async Task<IActionResult> GetExtraHourTypeByTypeHourName(string typeHourName)
        {
            var extraHourType = await _extraHourTypeService.GetByTypeHourNameAsync(typeHourName);
            if (extraHourType == null) return NotFound();
            return Ok(extraHourType);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetExtraHourTypeById(int id)
        {
            var extraHourType = await _extraHourTypeService.GetByIdAsync(id);
            if (extraHourType == null) return NotFound();
            return Ok(extraHourType);
        }
    }
}