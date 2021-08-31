using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCommunalPayments.Api.Infrastucture.ApiContracts;
using MyCommunalPayments.Api.Infrastucture.ApiServices;
using MyCommunalPayments.BL.Interfaces;
using MyCommunalPayments.Models.Models;
using System;
using System.Threading.Tasks;

namespace MyCommunalPayments.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IApiFileService _fileService;
        private readonly IOrderService _orderService;

        public OrderController(IMapper mapper,
                               IApiFileService fileService,
                               IOrderService orderService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpPost]
        public async Task<int> UploadOrder(IFormFile file)
        {
            if (file is null)
            {
                return 0;
            }
            //var inputOrder = await _fileService.UploadFileAsync(file);

            //var order = _mapper.Map<Order>(inputOrder);

            //var neworder = await _orderService.CreateEntityAsync(order);

            return 0;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetEntityAsync(id);
            if (order is null)
                return BadRequest();
            
            var orderContract = _mapper.Map<OrderContract>(order);
            
            return Ok(orderContract);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (id <= 0)
                return BadRequest();

            await _orderService.DeleteEntityAsync(id);
            return Ok();
        }
    }
}
