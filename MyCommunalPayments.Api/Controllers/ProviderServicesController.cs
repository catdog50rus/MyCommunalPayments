using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;

namespace MyCommunalPayments.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ProviderServicesController : ControllerBase
    {
        private readonly IRepository<ProvidersServices> repository;

        public ProviderServicesController(IRepository<ProvidersServices> repository) =>this.repository = repository;


        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<ProvidersServices>>> Search(string name)
        {
            try
            {
                var result = await repository.Search(name);

                if (result.Any()) return Ok(result);

                return NotFound($"Не найдено!");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка доступа {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await repository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка доступа {ex.Message}");
            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProvidersServices>> GetById(int id)
        {
            try
            {
                var result = await repository.GetByIdAsync(id);
                if (result == null) return NotFound($"Запись с ID: {id} не найдена");

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка доступа {ex.Message}");
            }


        }

        [HttpPost]
        public async Task<ActionResult<ProvidersServices>> CreateNew(ProvidersServices item)
        {
            try
            {
                if (item == null) return BadRequest($"Запрос пустой");

                var result = await repository.AddAsync(item);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка доступа {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProvidersServices>> Update(int id, ProvidersServices item)
        {
            try
            {
                if (item == null || id != item.Id) return BadRequest($"ID: {id} не соответствует запросу");
                var updateContent = await repository.GetByIdAsync(id);

                if (updateContent == null) return BadRequest($"Запись с ID: {id} не найдена");

                return await repository.EditAsync(item);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка доступа {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<ProvidersServices>> Update(ProvidersServices item)
        {
            try
            {
                if (item == null) return BadRequest();
                int id = item.Id;
                var updateContent = await repository.GetByIdAsync(id);

                if (updateContent == null) return BadRequest($"Запись с ID: {id} не найдена");

                return await repository.EditAsync(item);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка доступа {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProvidersServices>> Delete(int id)
        {
            try
            {
                var deleteContent = await repository.GetByIdAsync(id);

                if (deleteContent == null) return BadRequest($"Запись с ID: {id} не найдена");

                return await repository.RemoveAsync(id);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка доступа {ex.Message}");
            }
        }


    }
}
