using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using FinanceApi.DTOs.Create;
using FinanceApi.Models;
using FinanceApi.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApi.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/categories")]
    public class CategoryController(ICategoryService service) : ControllerBase
    {
        protected Guid UserId => Guid.Parse(User.FindFirst("id")?.Value!);
        private readonly ICategoryService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _service.GetAllAsync(UserId);

            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            await _service.CreateAsync(categoryCreateDto, UserId);

            return NoContent();
        }
    }
}