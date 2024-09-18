using AutoMapper;
using Business.Abstract;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantWepApp.Controllers
{
    public class TableController : Controller
    {
        private readonly ITableService _tableService;
        private readonly IMapper _mapper;

        public TableController(ITableService tableService, IMapper mapper)
        {
            _tableService = tableService;
            _mapper = mapper;
        }

        // GET: /Table/Index
        public IActionResult Index()
        {
            var result = _tableService.GetList();
            if (result.Success)
            {
                var tableDtos = _mapper.Map<List<TableResponseDto>>(result.Data);
                return View(tableDtos);
            }
            return View("Error", result.Message);
        }

        // GET: /Table/Details/5
        public IActionResult Details(Guid id)
        {
            var result = _tableService.GetById(id);
            if (result.Success)
            {
                var tableDto = _mapper.Map<TableResponseDto>(result.Data);
                return View(tableDto);
            }
            return NotFound();
        }

        // GET: /Table/Create
        public IActionResult Add()
        {
            return View();
        }

        // POST: /Table/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(TableCreateDto tableDto)
        {
            if (ModelState.IsValid)
            {
                var result = _tableService.Add(tableDto);
                if (result.Success)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(tableDto);
        }

        // GET: /Table/Edit/5
        public IActionResult Update(Guid id)
        {
            var result = _tableService.GetById(id);
            if (result.Success)
            {
                var tableDto = _mapper.Map<TableUpdateDto>(result.Data);
                return View(tableDto);
            }
            return NotFound();
        }

        // POST: /Table/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(TableUpdateDto tableDto)
        {
            if (ModelState.IsValid)
            {
                var result = _tableService.Update(tableDto);
                if (result.Success)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(tableDto);
        }

        // POST: /Table/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var result = _tableService.Delete(id);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            return View("Error", result.Message);
        }
    }
}
