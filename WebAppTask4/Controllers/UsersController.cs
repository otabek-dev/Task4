﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppTask4.Areas.Identity.Data;
using WebAppTask4.Models;
using WebAppTask4.Service;

namespace Task4App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        [Route("Block")]
        public async Task<IActionResult> Block([FromBody] string[] guids)
        {
            await _userService.BlockUser(guids);
            return Ok();
        }

        [HttpPost]
        [Route("UnBlock")]
        public async Task<IActionResult> UnBlock([FromBody] string[] guids)
        {
            await _userService.UnBlockUser(guids);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] string[] guids)
        {
            await _userService.DeleteUser(guids);
            return Ok();
        }
    }
}
