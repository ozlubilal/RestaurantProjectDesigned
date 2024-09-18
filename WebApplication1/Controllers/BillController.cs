using AutoMapper;
using Business.Abstract;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Utilities.Results;
using Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace RestaurantWepApp.Controllers
{
    public class BillController : Controller
    {
        private readonly IBillService _billService;
        private readonly ITableService _tableService;
        private readonly IMapper _mapper;

        public BillController(IBillService billService, ITableService tableService, IMapper mapper)
        {
            _billService = billService;
            _tableService = tableService;
            _mapper = mapper;
        }

        // GET: /Bill/Index
        public IActionResult Index()
        {
            var result = _billService.GetAll();
            if (result.Success)
            {
                return View(result.Data);
            }
            return View("Error", result.Message); // Başarısız durumda bir hata ekranı gösterilebilir
        }

        // GET: /Bill/Details/5
        public IActionResult Details(Guid id)
        {
            var result = _billService.GetById(id);
            if (result.Success)
            {
                return View(result.Data);
            }
            return NotFound();
        }

        // GET: /Bill/Create
        public IActionResult Add()
        {
            ViewBag.Tables = _tableService.GetList().Data;

            ViewBag.StatusList = Enum.GetValues(typeof(BillStatus))
                                     .Cast<BillStatus>()
                                     .Select(e => new { Value = (int)e, Text = e.ToString() })
                                     .ToList();
            return View(new BillCreateDto());
        }

        // POST: /Bill/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(BillCreateDto billCreateDto)
        {
            if (ModelState.IsValid)
            {
                var result = _billService.Add(billCreateDto);
                if (result.Success)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result.Message);
            }
            ViewBag.Tables = _tableService.GetList().Data;

            ViewBag.StatusList = Enum.GetValues(typeof(BillStatus))
                                     .Cast<BillStatus>()
                                     .Select(e => new { Value = (int)e, Text = e.ToString() })
                                     .ToList();
            return View(billCreateDto);
        }

        // GET: /Bill/Edit/5
        public IActionResult Update(Guid id)
        {
            var result = _billService.GetById(id);
            if (result.Success)
            {
                var bill = result.Data;
                var billUpdateDto = _mapper.Map<BillUpdateDto>(bill);
                return View(billUpdateDto);
            }
            return NotFound();
        }

        // POST: /Bill/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid id, BillUpdateDto billUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = _billService.Update(billUpdateDto);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(billUpdateDto);
        }

        // POST: /Bill/Delete/5
        [HttpPost]
        // [ValidateAntiForgeryToken]

        public IActionResult Delete(Guid id)
        {
            var result = _billService.Delete(id);
            if (result.Success)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false, message = result.Message });
        }

    }
}
