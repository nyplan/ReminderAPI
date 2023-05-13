using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ReminderAPI.DTOs.ReminderDTOs;
using ReminderAPI.Services.Abstract;
using ReminderAPI.Tokens;

namespace ReminderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Fixed")]
    public class RemindersController : ControllerBase
    {
        private readonly IReminderMappingService _reminderService;
        public RemindersController(IReminderMappingService reminderService)
        {
            _reminderService = reminderService;
        }

        [HttpGet("token")]
        public IActionResult Get()
        {
            return Created("", new BuildJwtToken().CreateToken());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] ReminderToAddDto dto)
        {
            await _reminderService.CreateAsync(dto);
            return Created("", dto);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Read() 
        {
            return Ok(_reminderService.Read());
        }

        [HttpPut]
        [Authorize]
        [DisableRateLimiting]
        public IActionResult Update([FromBody] ReminderToUpdateDto dto)
        {
            _reminderService.Update(dto);
            return NoContent();
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteRange([FromBody] IEnumerable<int> ids)
        {
            _reminderService.DeleteRange(ids);
            return NoContent();
        }
       
    }
}
