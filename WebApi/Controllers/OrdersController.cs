using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public IActionResult GetList()
    {
        var result = _orderService.GetList();
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var result = _orderService.GetById(id);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpGet("ByBillId/{billId}")]
    public IActionResult GetByBillId(Guid billId)
    {
        var result = _orderService.GetByBillId(billId);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpGet("ByProductId/{productId}")]
    public IActionResult GetByProductId(Guid productId)
    {
        var result = _orderService.GetByProductId(productId);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpPost]
    public IActionResult Add(OrderCreateDto orderCreateDto)
    {
        var result = _orderService.Add(orderCreateDto);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpPut]
    public IActionResult Update(OrderUpdateDto orderUpdateDto)
    {
        var result = _orderService.Update(orderUpdateDto);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var result = _orderService.Delete(id);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }
}
