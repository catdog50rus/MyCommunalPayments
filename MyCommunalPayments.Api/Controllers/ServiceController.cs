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
    public class ServiceController : ControllerBase
    {
        private readonly IRepository<Service> repository;

        public ServiceController(IRepository<Service> repository) => this.repository = repository;

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Service>>> Search(string serviceName) 
        {
            try
            {
                var result = await repository.Search(serviceName);

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
        public async Task<ActionResult<Service>> GetById(int id)
        {
            try
            {
                var result = await repository.GetByIdAsync(id);
                if (result == null)return NotFound($"Запись с ID: {id} не найдена");

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка доступа {ex.Message}");
            }


        }

        [HttpPost]
        public async Task<ActionResult<Service>> CreateNew(Service item)
        {
            try
            {
                if (item == null) return BadRequest($"Запрос пустой");

                var result = await repository.AddAsync(item);
                return CreatedAtAction(nameof(GetById), new { id = result.IdService }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка доступа {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Service>> Update(int id, Service item)
        {
            try
            {
                if (item == null || id != item.IdService) return BadRequest($"ID: {id} не соответствует запросу");
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
        public async Task<ActionResult<Service>> Update(Service item)
        {
            try
            {
                if (item == null) return BadRequest();
                int id = item.IdService;
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
        public async Task<ActionResult<Service>> Delete(int id)
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
