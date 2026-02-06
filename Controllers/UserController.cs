using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using FinanceApi.DTOs;
using FinanceApi.Models;
using FinanceApi.Services;

namespace FinanceApi.Controller
{
    [ApiController]
    [Route("api/users")]
    public class UserController(IUserService userService, IAccountService accountService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IAccountService _accountService = accountService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDto userCreateDto)
        {
            var user = await _userService.CreateAsync(userCreateDto);
            return CreatedAtAction(nameof(GetAll), new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return user is null
                ? NotFound()
                : Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserCreateDto userDto)
        {
            await _userService.Update(id, userDto);
            return Ok();
        }

    }
}