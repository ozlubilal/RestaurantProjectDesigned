using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeaterOfTablesController : ControllerBase
    {
        private ISeaterOfTableService _seaterOfTableService;

        public SeaterOfTablesController(ISeaterOfTableService seaterOfTableService)
        {
            _seaterOfTableService = seaterOfTableService;
        }



        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _seaterOfTableService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _seaterOfTableService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("add")]
        public IActionResult Add(SeaterOfTable seaterOfTable)
        {
            var result = _seaterOfTableService.Add(seaterOfTable);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpPost("delete")]
        public IActionResult Delete(SeaterOfTable seaterOfTable)
        {
            var result = _seaterOfTableService.Delete(seaterOfTable);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpPost("update")]
        public IActionResult Update(SeaterOfTable seaterOfTable)
        {
            var result = _seaterOfTableService.Update(seaterOfTable);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
    }
}
