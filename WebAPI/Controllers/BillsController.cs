using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        IBillService _billService;

        public BillsController(IBillService billService)
        {
            _billService = billService;
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _billService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _billService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("calculatebillprice")]
        public IActionResult CalculateBillPrice(int billId)
        {
            var result = _billService.CalculateBillPrice(billId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpPost("add")]
        public IActionResult Add(Bill bill)
        {
            var result = _billService.Add(bill);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpPost("delete")]
        public IActionResult Delete(Bill bill)
        {
            var result = _billService.Delete(bill);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpPost("update")]
        public IActionResult Update(Bill Bill)
        {
            var result = _billService.Update(Bill);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }

        [HttpGet("getlistbilldetaildto")]
        public IActionResult GetListBillDetailDto()
        {
            var result = _billService.GetListBillDetailDto();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }

        [HttpGet("getbybillstatusid")]
        public IActionResult GetByBillStatusId(int billStatusId)
        {
            var result = _billService.GetByBillStatusId(billStatusId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }

        [HttpGet("getbystorebillid")]
        public IActionResult GetByStoreBillId(int billStatusId)
        {
            var result = _billService.GetByStoreBillId(billStatusId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpGet("getbytableidandstatusid")]
        public IActionResult GetByTableIdAndStatusId(int tableId, int statusId)
        {
            var result = _billService.GetByTableIdAndStatusId(tableId,statusId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpGet("getbydate")]
        public IActionResult GetByDate(DateTime date)
        {
            var result = _billService.GetByDate(date);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }




    }
}
