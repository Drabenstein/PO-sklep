using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PO_sklep.DTO;
using PO_sklep.Helpers;
using PO_sklep.Services.Interfaces;

namespace PO_sklep.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpPost("add")]
        [ExactQueryParam("id")]
        public async Task<ActionResult> Create([FromQuery]int id, [FromBody]OrderDto order)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var orderId = await _orderService.CreateOrderAsync(id, order);

                if (orderId is null)
                {
                    return BadRequest();
                }

                return Ok(orderId);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("add")]
        [ExactQueryParam("email")]
        public async Task<ActionResult> Create([FromQuery]string email, [FromBody]OrderDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var orderId = await _orderService.CreateOrderAsync(email, order);

                if (orderId is null)
                {
                    return BadRequest();
                }

                return Ok(orderId);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
