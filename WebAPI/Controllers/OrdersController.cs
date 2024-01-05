using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }



        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _orderService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _orderService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("add")]
        public IActionResult Add(Order order)
        {
            var result = _orderService.Add(order);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpPost("delete")]
        public IActionResult Delete(Order order)
        {
            var result = _orderService.Delete(order);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpPost("update")]
        public IActionResult Update(Order order)
        {
            var result = _orderService.Update(order);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }

        [HttpGet("getbybillid")]
        public IActionResult GetByBillId(int billId)
        {
            var result = _orderService.GetByBillId(billId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpGet("getorderdetailsbybillid")]
        public IActionResult GetOrderDetailsByBillId(int billId)
        {
            var result = _orderService.GetOrderDetailsByBillId(billId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpGet("getorderdetailsbystorebillid")]
        public IActionResult GetOrderDetailsByStoreBillId(int storeBillId)
        {
            var result = _orderService.GetOrderDetailsByStoreBillId(storeBillId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpGet("getorderdetailsbyid")]
        public IActionResult GetOrderDetailsById(int id)
        {
            var result = _orderService.GetOrderDetailsByBillId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }

        [HttpGet("getorderdetailsofactive")]
        public IActionResult GetOrderDetailsOfActive(int billId)
        {
            var result = _orderService.GetOrderDetailsOfActive();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }

        [HttpPost("refreshorderdetailslist")]
        public IActionResult RefreshOrderDetailsList(List<OrderDetailDto> list)
        {
            var result = _orderService.RefreshOrderDetilsList(list);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
    

     
        [HttpGet("getbyproductname")]
        public IActionResult GetByProductName(string productName)
        {
            var result = _orderService.GetByProductName(productName);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
     

        [HttpGet("getbyorderstatusid")]
        public IActionResult GetByOrderStatusId(int orderStatusId)
        {
            var result = _orderService.GetByOrderStatusId(orderStatusId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpGet("getorderdetails")]
        public IActionResult GetOrderDetails()
        {
            var result = _orderService.GetOrderDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
   
    }
}
