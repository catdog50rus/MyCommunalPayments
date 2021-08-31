using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCommunalPayments.BL.Interfaces;
using MyCommunalPayments.Models.Models;
using System;
using System.Threading.Tasks;

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

        private readonly IInvoiceService  _invoiceService;

        /// <summary>
        /// Invoice Controller
        /// </summary>
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService ?? throw new ArgumentNullException(nameof(invoiceService));
        }


        //[HttpGet("{search}")]
        //public async Task<ActionResult<IEnumerable<Invoice>>> Search(string name)
        //{
        //    try
        //    {
        //        var result = await _repository.Search(name);

        //        if (result.Any()) return Ok(result);

        //        return NotFound($"Не найдено!");

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Ошибка базы данных {ex.Message}");
        //    }
        //}

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
                return Ok(await _invoiceService.GetEntitiesAsync());
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
                var result = await _invoiceService.GetEntityAsync(id);
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
        /// <param name="newInvoice">Invoice</param>
        /// <returns>Invoice</returns>
        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateNew(Invoice newInvoice)
        {
            if (newInvoice is null)
            {
                return BadRequest($"Запрос пустой");
            }

            try
            {
                var result = await _invoiceService.CreateEntityAsync(newInvoice);
                return Ok(result);
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
        public async Task<ActionResult> Update(int id, Invoice item)
        {
            try
            {
                if (item == null || id != item.IdInvoice) 
                    return BadRequest($"ID: {id} не соответствует запросу");

                await _invoiceService.UpdateEntityAsync(item);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка базы данных {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(Invoice item)
        {
            if (item is null)
            {
                return BadRequest($"Запись  не найдена");
            }

            try
            {

                await _invoiceService.UpdateEntityAsync(item);
                return Ok();

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
        public async Task<ActionResult> Delete(int id)
        {
            if(id <= 0)
                return BadRequest($"Запись с ID: {id} не найдена");

            try
            {

                await _invoiceService.DeleteEntityAsync(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка базы данных {ex.Message}");
            }
        }
    }
}
