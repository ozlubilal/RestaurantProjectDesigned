using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Core.Utilities.Results;
using Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantWepApp.Controllers // WebAPI yerine WebApp ya da UI ile ilgili bir namespace kullanın.
{
    public class StoreBillController : Controller // ControllerBase değil, Controller'dan türetmelisiniz.
    {
        private readonly IStoreBillService _storeBillService;
        private readonly IMapper _mapper;

        public StoreBillController(IStoreBillService storeBillService,IMapper mapper)
        {
            _storeBillService = storeBillService;
            _mapper = mapper;        }

        // GET: StoreBill/Index
        public IActionResult Index()
        {
            var result = _storeBillService.GetList();
            if (result.Success)
            {
                return View(result.Data); // View döndürüyoruz.
            }
            return View("Error", result.Message);
        }

        // GET: StoreBill/Add
        public IActionResult Add()
        {
            ViewBag.StatusList = Enum.GetValues(typeof(StoreBillStatus))
                                     .Cast<StoreBillStatus>()
                                     .Select(e => new { Value = (int)e, Text = e.ToString() })
                                     .ToList();
            return View(new StoreBillCreateDto()); // Bu kısımda bir form sayfası dönebiliriz.
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
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(storeBillCreateDto);
        }

        // GET: StoreBill/Update/{id}
        // GET: StoreBill/Update/{id}
        public IActionResult Update(Guid id)
        {
            var storeBill = _storeBillService.GetById(id);
            if (storeBill.Success)
            {
                var storeBillUpdateDto = _mapper.Map<StoreBillUpdateDto>(storeBill.Data);
                
                ViewBag.StatusList = Enum.GetValues(typeof(StoreBillStatus))
                                        .Cast<StoreBillStatus>()
                                        .Select(e => new { Value = (int)e, Text = e.ToString() })
                                        .ToList();

                return View(storeBillUpdateDto); // Model türü StoreBillUpdateDto
            }
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
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(storeBillUpdateDto);
        }

        // GET: StoreBill/Delete/{id}
        public IActionResult Delete(Guid id)
        {
            var result = _storeBillService.Delete(id);
            if (result.Success)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false, message = result.Message });
        }
    }
}
