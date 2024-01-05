using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreBillsController : ControllerBase
    {
        IStoreBillService _storeBillService;

        public StoreBillsController(IStoreBillService storeBillService)
        {
            _storeBillService = storeBillService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _storeBillService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _storeBillService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getbystorebillstatusid")]
        public IActionResult GetStoreBillByStatusId(int statusId)
        {
            var result = _storeBillService.GetByStatusId(statusId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("add")]
        public IActionResult Add(StoreBill storeBill)
        {
            var result = _storeBillService.Add(storeBill);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpPost("delete")]
        public IActionResult Delete(StoreBill storeBill)
        {
            var result = _storeBillService.Delete(storeBill);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
        [HttpPost("update")]
        public IActionResult Update(StoreBill storeBill)
        {
            var result = _storeBillService.Update(storeBill);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }

        [HttpGet("getlistdetails")]
        public IActionResult GetListBillDetailDto()
        {

            var result = _storeBillService.GetStoreBillDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
    }
}
