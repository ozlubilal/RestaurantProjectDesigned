using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantWepApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private IUserService _userService;

        public CategoryController(ICategoryService categoryService,IUserService userService)
        {
            _categoryService = categoryService;
            _userService = userService;
        }

        // GET: /Category/Index
        public IActionResult Index()
        {
            // Token'ı cookie'den alma
           

            var categories = _categoryService.GetList();
            return View(categories.Data);
        }
    


    // GET: /Category/Add
    public IActionResult Add()
        {
            return View("CategoryAdd", new CategoryCreateDto());
        }

        // POST: /Category/Add
        [HttpPost]
        public IActionResult Add(CategoryCreateDto categoryCreateDto)
        {
            var result = _categoryService.Add(categoryCreateDto);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View("CategoryAdd", categoryCreateDto);
        }

        // GET: /Category/Edit/5
        public IActionResult Update(Guid id)
        {
            var result = _categoryService.GetById(id);
            if (result.Success)
            {
                var categoryUpdateDto = new CategoryUpdateDto
                {
                    Id = result.Data.Id,
                    Name = result.Data.Name
                };
                return View("CategoryUpdate", categoryUpdateDto);
            }
            return NotFound();
        }

        // POST: /Category/Edit
        [HttpPost]

        public IActionResult Update(CategoryUpdateDto categoryUpdateDto)
        {
            var result = _categoryService.Update(categoryUpdateDto);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View("CategoryUpdate", categoryUpdateDto);
        }

        // POST: /Category/Delete/5
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var result = _categoryService.Delete(id);
            if (result.Success)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false, message = result.Message });
        }

    }
}
