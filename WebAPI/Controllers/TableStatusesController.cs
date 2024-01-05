using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableStatusesController : ControllerBase
    {
        private ITableStatusService _tableStatusService;

        public TableStatusesController(ITableStatusService tableStatusService)
        {
            _tableStatusService = tableStatusService;
        }



        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _tableStatusService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _tableStatusService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("add")]
        public IActionResult Add(TableStatus tableStatus)
        {
            var result = _tableStatusService.Add(tableStatus);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpPost("delete")]
        public IActionResult Delete(TableStatus tableStatus)
        {
            var result = _tableStatusService.Delete(tableStatus);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpPost("update")]
        public IActionResult Update(TableStatus tableStatus)
        {
            var result = _tableStatusService.Update(tableStatus);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
    }
}
