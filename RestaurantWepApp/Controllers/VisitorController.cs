using Business.Abstracts;
using Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantWepApp.Controllers
{
    public class VisitorController : BaseController
    {
        private readonly IVisitorService _visitorService;

        public VisitorController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        public IActionResult Index()
        {
            var result = _visitorService.GetAll().Data;
            return View(result);
        }
        public IActionResult DeleteAll() 
        {
            _visitorService.DeleteAll();    
            return View("Index");
        }

    }
}
