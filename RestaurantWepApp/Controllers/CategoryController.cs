using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantWepApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private IUserService _userService;

        public CategoryController(ICategoryService categoryService, IUserService userService)
        {
            _categoryService = categoryService;
            _userService = userService;
        }

        // GET: /Category/Index
       
        public IActionResult Index()
        {
            var categories = _categoryService.GetList();
            return View(categories.Data);
        }

        // GET: /Category/Add
        public IActionResult Add()
        {
            return View("Add", new CategoryCreateDto());
        }

        // POST: /Category/Add
        [HttpPost]
        public IActionResult Add(CategoryCreateDto categoryCreateDto)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMessage(Messages.FormIncomplete);
                return View("Add", categoryCreateDto);

            }
            var result = _categoryService.Add(categoryCreateDto);
            if (result.Success)
            {
                SetSuccessMessage(result.Message);
                return RedirectToAction("Index");
            }
            SetErrorMessage(result.Message);
            return View("Add", categoryCreateDto);
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
                return View("Update", categoryUpdateDto);
            }
            return NotFound();
        }

        // POST: /Category/Edit
        [HttpPost]

        public IActionResult Update(CategoryUpdateDto categoryUpdateDto)
        {

            if (!ModelState.IsValid)
            {
                SetErrorMessage(Messages.FormIncomplete);
                return View("Update", categoryUpdateDto);
            }
            var result = _categoryService.Update(categoryUpdateDto);
            if (result.Success)
            {
                SetSuccessMessage(result.Message);
                return RedirectToAction("Index");
            }
            SetErrorMessage(result.Message);
            return View("Update", categoryUpdateDto);
        }

        // POST: /Category/Delete/5
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var result = _categoryService.Delete(id);
            if (result.Success)
            {
                SetWarningMessage(result.Message);
                return Json(new { success = true });
            }
            SetErrorMessage(result.Message);
            return Json(new { success = false});
        }

    }
}
