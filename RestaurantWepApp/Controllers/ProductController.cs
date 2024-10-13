using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantWepApp.Controllers;

[Authorize(Roles = "Admin")]
public class ProductController : BaseController
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
        LoadViewBagData();
        return View(new ProductCreateDto());
    }

    // POST: /Product/Add
    [HttpPost]
    public IActionResult Add(ProductCreateDto productCreateDto)
    {
        if (!ModelState.IsValid)
        {
            LoadViewBagData(); 
            SetErrorMessage(Messages.FormIncomplete);
            return View(productCreateDto);
        }

        var result = _productService.Add(productCreateDto);
        if (result.Success)
        {
            SetSuccessMessage(result.Message);
            return RedirectToAction("Index");
        }

        LoadViewBagData();
        SetErrorMessage(result.Message);
        return View(productCreateDto);
    }

    // GET: /Product/Edit/5
    public IActionResult Update(Guid id)
    {
        var result = _productService.GetById(id);
        if (result.Success)
        {
            var productUpdateDto = _mapper.Map<ProductUpdateDto>(result.Data);
            LoadViewBagData();
            return View(productUpdateDto);
        }
        return NotFound();
    }

    // POST: /Product/Edit
    [HttpPost]
    public IActionResult Update(ProductUpdateDto productUpdateDto)
    {
        if (!ModelState.IsValid) 
        {
            LoadViewBagData(); 
            SetErrorMessage(Messages.FormIncomplete);
            return View(productUpdateDto);
        }

        var result = _productService.Update(productUpdateDto);
        if (result.Success)
        {
            SetSuccessMessage(result.Message);
            return RedirectToAction("Index");
        }

        LoadViewBagData();
        SetErrorMessage(result.Message);
        return View(productUpdateDto);
    }

    // POST: /Product/Delete/5
    [HttpPost]
    public IActionResult Delete(Guid id)
    {
        var result = _productService.Delete(id);
        if (result.Success)
        {
            SetWarningMessage(result.Message);
            return Json(new { success = true });
        }
        SetErrorMessage(result.Message);
        return Json(new { success = false });
    }

    // Helper method to load data for dropdowns
    private void LoadViewBagData()
    {
        ViewBag.StatusList = Enum.GetValues(typeof(ProductStatus))
                                  .Cast<ProductStatus>()
                                  .Select(e => new { Value = (int)e, Text = e.ToString() })
                                  .ToList();
        var categories = _categoryService.GetList();

        if (categories.Success)
        {
            ViewBag.Categories = _categoryService.GetList().Data;
        }
        else
        {
            ViewBag.Categories = new List<CategoryResponseDto>();
        }
       
    }
}
