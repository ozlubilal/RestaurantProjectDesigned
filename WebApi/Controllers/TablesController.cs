using Business.Abstracts;
using Business.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TablesController : ControllerBase
{
    private readonly ITableService _tableService;

    public TablesController(ITableService tableService)
    {
        _tableService = tableService;
    }

    [HttpGet]
    public IActionResult GetList()
    {
        var result = _tableService.GetList();
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var result = _tableService.GetById(id);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpPost]
    public IActionResult Add(TableCreateDto tableCreateDto)
    {
        var result = _tableService.Add(tableCreateDto);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpPut]
    public IActionResult Update(TableUpdateDto tableUpdateDto)
    {
        var result = _tableService.Update(tableUpdateDto);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var result = _tableService.Delete(id);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }
}
