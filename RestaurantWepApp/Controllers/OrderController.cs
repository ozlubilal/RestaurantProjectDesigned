using AutoMapper;
using Business.Abstract;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Entities.Concrete;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantWepApp.Controllers;

[Authorize(Roles = "Admin")]
public class OrderController : BaseController
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly IBillService _billService;
    private readonly IMapper _mapper;

    
    public OrderController(IOrderService orderService, IMapper mapper, IProductService productService, IBillService billService)
    {
        _orderService = orderService;
        _mapper = mapper;
        _productService = productService;
        _billService = billService;
    }

    // GET: /Order/Index
    public IActionResult Index()
    {
        var result = _orderService.GetList();
        if (result.Success)
        {
            return View(result.Data); 
        }
        return View("Error", result.Message);
    }

    public IActionResult ListByBilId(Guid id)
    {
        var result = _orderService.GetByBillId(id);
        ViewBag.bill = _billService.GetById(id).Data;
        if (result.Success)
        {
            return View(result.Data); // Başarılı ise detayları gösterir
        }
        return NotFound(); // Sipariş bulunamazsa 404 döndürür
    }

    public IActionResult Add()
    {
        LoadViewBagData(); 
        return View(new OrderCreateDto());
    }

   
    [HttpPost]
    public IActionResult Add(OrderCreateDto orderCreateDto)
    {
        if (ModelState.IsValid)
        {
            var result = _orderService.Add(orderCreateDto);
            if (result.Success)
            {
                SetSuccessMessage(result.Message); 
                return RedirectToAction("Index"); 
            }
            SetErrorMessage(result.Message); 
        }

        LoadViewBagData(); 
        return View(orderCreateDto); 
    }

    // POST: /Order/Edit/5
    [HttpPost]
    public IActionResult Update(OrderUpdateDto orderUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            SetErrorMessage(Messages.FormIncomplete);
            return Json(new { success = false, message = Messages.FormIncomplete });
        }

        var result = _orderService.Update(orderUpdateDto);
        if (result.Success)
        {
            SetSuccessMessage(result.Message); 
            return Json(new { success = true });
        }
        SetErrorMessage(result.Message); 
        return Json(new { success = false, message = result.Message }); 
    }

    // POST: /Order/Delete/5
    
    [HttpPost]
    public IActionResult Delete(Guid id)
    {
        var result = _orderService.Delete(id);
        if (result.Success)
        {
            SetWarningMessage(result.Message);
            return Json(new { success = true }); 
        }
        SetErrorMessage(result.Message);
        return Json(new { success = false, message = result.Message }); 
    }

   
    private void LoadViewBagData()
    {
        ViewBag.StatusList = Enum.GetValues(typeof(OrderStatus))
                                 .Cast<OrderStatus>()
                                 .Select(e => new { Value = (int)e, Text = e.ToString() })
                                 .ToList();

        var products = _productService.GetList();
        if (products.Success)
        {
            ViewBag.Products = products.Data; 
        }
        else
        {
            ViewBag.Products = new List<ProductResponseDto>(); 
        }

        var bills = _billService.GetAll();
        if (bills.Success)
        {
            ViewBag.Bills = bills.Data; 
        }
        else
        {
            ViewBag.Bills = new List<BillResponseDto>(); 
        }
    }
}
