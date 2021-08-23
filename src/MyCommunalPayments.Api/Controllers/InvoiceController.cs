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
    /// <summary>
    /// Invoice controller
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public class InvoiceController : ControllerBase
    {
        private readonly IRepository<Invoice> repository;
        /// <summary>
        /// Invoice Controller
        /// </summary>
        /// <param name="repository"></param>
        public InvoiceController(IRepository<Invoice> repository) => this.repository = repository;


        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> Search(string name)
        {
            try
            {
                var result = await repository.Search(name);

                if (result.Any()) return Ok(result);

                return NotFound($"Не найдено!");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка базы данных {ex.Message}");
            }
        }

        /// <summary>
        /// Get All Invoices
        /// </summary>
        /// <returns>All Invoces</returns>
        /// <response code="500">Ошибка базы данных</response> 
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await repository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка базы данных {ex.Message}");
            }

        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id">Invoice Id</param>
        /// <returns>Invoice</returns>
        /// <response code="200">Find Invoice</response> 
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Invoice>> GetById(int id)
        {
            try
            {
                var result = await repository.GetByIdAsync(id);
                if (result == null) return NotFound($"Запись с ID: {id} не найдена");

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка базы данных {ex.Message}");
            }


        }

        /// <summary>
        /// Create New Invoice
        /// </summary>
        /// <param name="item">Invoice</param>
        /// <returns>Invoice</returns>
        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateNew(Invoice item)
        {
            try
            {
                if (item == null) return BadRequest($"Запрос пустой");

                var result = await repository.AddAsync(item);
                return CreatedAtAction(nameof(GetById), new { id = result.IdInvoice }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка базы данных {ex.Message}");
            }
        }

        /// <summary>
        /// Update Invoice
        /// </summary>
        /// <param name="id">Invoice Id</param>
        /// <param name="item">Invoice</param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Invoice>> Update(int id, Invoice item)
        {
            try
            {
                if (item == null || id != item.IdInvoice) return BadRequest($"ID: {id} не соответствует запросу");
                var updateContent = await repository.GetByIdAsync(id);

                if (updateContent == null) return BadRequest($"Запись с ID: {id} не найдена");

                return await repository.EditAsync(item);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка базы данных {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Invoice>> Update(Invoice item)
        {
            try
            {
                if (item == null) return BadRequest();
                int id = item.IdInvoice;
                var updateContent = await repository.GetByIdAsync(id);

                if (updateContent == null) return BadRequest($"Запись с ID: {id} не найдена");

                return await repository.EditAsync(item);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка базы данных {ex.Message}");
            }
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="id">Invoice Id</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Invoice>> Delete(int id)
        {
            try
            {
                var deleteContent = await repository.GetByIdAsync(id);

                if (deleteContent == null) return BadRequest($"Запись с ID: {id} не найдена");

                return await repository.RemoveAsync(id);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка базы данных {ex.Message}");
            }
        }
    }
}
