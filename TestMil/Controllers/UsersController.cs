using System;
using System.Threading.Tasks;
using BLL.Interfaces.Services;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into UsersController");
        }

        [HttpGet()]
        public async Task<ActionResult<UserDto>> GetAllUsersAsync()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserByIdAsync([FromRoute] int id)
        {
            return Ok(await _userService.GetAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateAsync([FromBody] UserDto userDto)
        {
            try
            {
                return Ok(await _userService.AddAsync(userDto));
            }
            catch (Exception ex)
            {
                return BadRequest();
                _logger.LogError(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<UserDto>> UpdateAsync([FromBody] UserDto userDto)
        {
            try
            {
                var user = await _userService.GetAsync(userDto.Id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(await _userService.UpdateAsync(userDto));
            }
            catch (Exception ex)
            {
                return BadRequest();
                _logger.LogError(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync([FromRoute] int id)
        {
            try
            {
                var user = await _userService.GetAsync(id);

                if(user == null)
                {
                    return NotFound();
                }
                await _userService.RemoveAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest();
                _logger.LogError(ex.Message);
            }

            return Ok();
        }
    }
}
