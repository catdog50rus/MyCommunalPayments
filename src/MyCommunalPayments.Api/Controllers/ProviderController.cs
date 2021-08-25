﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCommunalPayments.BL.Interfaces;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;

namespace MyCommunalPayments.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService  _service;

        public ProviderController(IProviderService service)
        {
             _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        //[HttpGet("{search}")]
        //public async Task<ActionResult<IEnumerable<Provider>>> Search(string name)
        //{
        //    try
        //    {
        //        var result = await repository.Search(name);

        //        if (result.Any()) return Ok(result);

        //        return NotFound($"Не найдено!");

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Ошибка базы данных {ex.Message}");
        //    }
        //}

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _service.GetEntitiesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка базы данных {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Provider>> GetById(int id)
        {
            try
            {
                var result = await _service.GetEntityAsync(id);
                if (result == null) 
                    return NotFound($"Запись с ID: {id} не найдена");

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка базы данных {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Provider>> CreateNew(Provider item)
        {
            try
            {
                if (item == null) 
                    return BadRequest($"Запрос пустой");

                var result = await _service.CreateEntityAsync(item);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка базы данных {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, Provider item)
        {
            try
            {
                if (item == null || id != item.IdProvider) 
                    return BadRequest($"ID: {id} не соответствует запросу");
                await _service.UpdateEntityAsync(item);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка базы данных {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Provider>> Update(Provider item)
        {
            try
            {
                if (item == null) 
                    return BadRequest();
                await _service.UpdateEntityAsync(item);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка базы данных {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Provider>> Delete(int id)
        {
            try
            {
                await _service.DeleteEntityAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка базы данных {ex.Message}");
            }
        }
    }
}
