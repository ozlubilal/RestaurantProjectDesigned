using AutoMapper;
using Business.Abstract;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Utilities.Results;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace RestaurantWepApp.Controllers;

[Authorize(Roles = "Admin")]
public class StoreBillController : BaseController
{
    private readonly IStoreBillService _storeBillService;
    private readonly IBillService _billService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public StoreBillController(IStoreBillService storeBillService, IMapper mapper, IBillService billService, ICategoryService categoryService)
    {
        _storeBillService = storeBillService;
        _mapper = mapper;
        _billService = billService;
        _categoryService = categoryService;
    }

    // GET: StoreBill/Index
    public IActionResult Index()
    {
        var result = _storeBillService.GetList();
        if (result.Success)
        {
            return View(result.Data);
        }
        SetErrorMessage(result.Message); 
        return View("Error", result.Message);
    }

    // GET: StoreBill/Add
    public IActionResult Add()
    {
        LoadViewBagData();
        return View(new StoreBillCreateDto());
    }

    // POST: StoreBill/Add
    [HttpPost]
    public IActionResult Add(StoreBillCreateDto storeBillCreateDto)
    {
        if (ModelState.IsValid)
        {
            var result = _storeBillService.Add(storeBillCreateDto);
            if (result.Success)
            {
                SetSuccessMessage(result.Message); 
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
        }
        LoadViewBagData();
        return View(storeBillCreateDto);
    }

    // GET: StoreBill/Update/{id}
    public IActionResult Update(Guid id)
    {
        var storeBillResult = _storeBillService.GetById(id);
        if (storeBillResult.Success)
        {
            var storeBillUpdateDto = _mapper.Map<StoreBillUpdateDto>(storeBillResult.Data);
            LoadViewBagData();
            return View(storeBillUpdateDto);
        }
        SetErrorMessage("Store bill not found."); 
        return RedirectToAction("Index");
    }

    // POST: StoreBill/Update
    [HttpPost]
    public IActionResult Update(StoreBillUpdateDto storeBillUpdateDto)
    {
        if (ModelState.IsValid)
        {
            var result = _storeBillService.Update(storeBillUpdateDto);
            if (result.Success)
            {
                return Json(new { success = true, message = "Store bill updated successfully!" });
            }
            return Json(new { success = false, message = result.Message });
        }
        return Json(new { success = false, message = "Invalid model state." });
    }

    // POST: StoreBill/Delete/{id}
    [HttpPost]
    public IActionResult Delete(Guid id)
    {
        var result = _storeBillService.Delete(id);
        if (result.Success)
        {
            SetWarningMessage(result.Message); 
            return Json(new { success = true });
        }
        SetErrorMessage(result.Message); 
        return Json(new { success = false, message = result.Message });
    }


    [HttpGet]
    public IActionResult GetBills(Guid id)
    {
        var result = _billService.GetByStoreBillId(id);

        if (result.Success)
        {
            // BillStatus enum değerini string'e çevirerek yanıt döndürme
            var billsWithStatusString = result.Data.Select(b => new
            {
                b.Id,
                b.CreatedDate,
                b.ClosedDate,
                b.TotalAmount,
                Status = b.Status.ToString(), // Enum'u string olarak döndür
                b.TableName
            }).ToList();

            return Json(new { success = true, bills = billsWithStatusString });
        }

        return Json(new { success = false, message = result.Message });
    }

    [HttpPost]
    public IActionResult BillUpdate(BillUpdateDto billUpdateDto)
    {
        // Model durumunu kontrol et
        if (ModelState.IsValid)
        {
            var bill = _billService.GetBillById(billUpdateDto.Id).Data;

            if (bill != null)
            {
                bill.Status = (BillStatus)billUpdateDto.Status;
            }
            billUpdateDto = _mapper.Map<BillUpdateDto>(bill);

            var result = _billService.Update(billUpdateDto);

            // Güncelleme başarılıysa
            if (result.Success)
            {
                return Json(new { success = true });
            }

            // Güncelleme başarısızsa hata mesajı döndür
            return Json(new { success = false, message = result.Message });
        }

        // Model geçersizse hata döndür
        return Json(new { success = false, message = "Geçersiz veri." });
    }

    private void LoadViewBagData()
    {
        ViewBag.StatusList = Enum.GetValues(typeof(StoreBillStatus))
                                  .Cast<StoreBillStatus>()
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
