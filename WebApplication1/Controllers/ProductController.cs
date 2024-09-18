using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace RestaurantWepApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, ICategoryService categoryService, IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // GET: /Product/Index
        public IActionResult Index()
        {
            var products = _productService.GetList();
            return View(products.Data);
        }

        // GET: /Product/Add
        public IActionResult Add()
        {
            ViewBag.Categories = _categoryService.GetList().Data; // Load categories for dropdown
            ViewBag.StatusList = Enum.GetValues(typeof(ProductStatus))
                                       .Cast<ProductStatus>()
                                       .Select(e => new { Value = (int)e, Text = e.ToString() })
                                       .ToList();
            return View(new ProductCreateDto());
        }

        // POST: /Product/Add
        [HttpPost]
        public IActionResult Add(ProductCreateDto productCreateDto)
        {
            var result = _productService.Add(productCreateDto);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            ViewBag.Categories = _categoryService.GetList().Data; // Reload categories on error
            return View(productCreateDto);
        }

        // GET: /Product/Edit/5
        public IActionResult Update(Guid id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                var productUpdateDto = _mapper.Map<ProductUpdateDto>(result.Data);

                // Enum değerlerini ViewBag ile View'e gönder
                ViewBag.StatusList = Enum.GetValues(typeof(ProductStatus))
                                        .Cast<ProductStatus>()
                                        .Select(e => new { Value = (int)e, Text = e.ToString() })
                                        .ToList();

                ViewBag.Categories = _categoryService.GetList().Data; // Kategorileri yükle
                return View(productUpdateDto);
            }
            return NotFound();
        }



        // POST: /Product/Edit
        [HttpPost]
        public IActionResult Update(ProductUpdateDto productUpdateDto)
        {
            var result = _productService.Update(productUpdateDto);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            ViewBag.Categories = _categoryService.GetList().Data; // Reload categories on error
            return View(productUpdateDto);
        }

        // POST: /Product/Delete/5
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var result = _productService.Delete(id);
            if (result.Success)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false, message = result.Message });
        }
    }
}
