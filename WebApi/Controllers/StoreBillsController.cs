using Business.Abstracts;
using Business.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoreBillsController : ControllerBase
{
    private readonly IStoreBillService _storeBillService;

    public StoreBillsController(IStoreBillService storeBillService)
    {
        _storeBillService = storeBillService;
    }

    [HttpGet]
    public IActionResult GetList()
    {
        var result = _storeBillService.GetList();
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var result = _storeBillService.GetById(id);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpPost]
    public IActionResult Add(StoreBillCreateDto storeBillCreateDto)
    {
        var result = _storeBillService.Add(storeBillCreateDto);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpPut]
    public IActionResult Update(StoreBillUpdateDto storeBillUpdateDto)
    {
        var result = _storeBillService.Update(storeBillUpdateDto);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var result = _storeBillService.Delete(id);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }
}
