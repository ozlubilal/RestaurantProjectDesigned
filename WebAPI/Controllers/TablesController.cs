using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private ITableService _tableService;

        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }



        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _tableService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _tableService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getbystatuid")]
        public IActionResult GetByStatuId(int statuId)
        {
            var result = _tableService.GetByStatuId(statuId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("add")]
        public IActionResult Add(Table table)
        {
            var result = _tableService.Add(table);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpPost("delete")]
        public IActionResult Delete(Table table)
        {
            var result = _tableService.Delete(table);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpPost("update")]
        public IActionResult Update(Table table)
        {
            var result = _tableService.Update(table);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpGet("getlisttabledetaildto")]
        public IActionResult GetListTableDetailDto()
        {
            var result = _tableService.GetListTableDetailDto();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
    }
}
