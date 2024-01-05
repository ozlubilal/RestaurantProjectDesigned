using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillStatusesController : ControllerBase
    {
        IBillStatusService _billStatusService;

        public BillStatusesController(IBillStatusService billStatusService)
        {
            _billStatusService = billStatusService;
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _billStatusService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _billStatusService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("add")]
        public IActionResult Add(BillStatus billSatus)
        {
            var result = _billStatusService.Add(billSatus);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpPost("delete")]
        public IActionResult Delete(BillStatus billSatus)
        {
            var result = _billStatusService.Delete(billSatus);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpPost("update")]
        public IActionResult Update(BillStatus billSatus)
        {
            var result = _billStatusService.Update(billSatus);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
    }
}
