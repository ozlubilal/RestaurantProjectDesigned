using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("getuserdetailbymail")]
        public IActionResult GetUserDetailByMail(string email)
        {
            var result=_userService.GetUserDetailByMail(email);
            if (result.Success) 
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("getalluserdetail")]
        public IActionResult GetAllUserDetail()
        {
            var result = _userService.GetAllUserDetail();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result=_userService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("delete")]
        public IActionResult Delete(User user)
        {
            var result=_userService.Delete(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
