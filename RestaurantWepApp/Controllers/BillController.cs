using Business.Abstract;
using Business.Abstracts;
using Business.Dtos.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RestaurantWepApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BillController : BaseController
    {
        private readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        // GET: /Bill/Index
        public IActionResult Index()
        {
            LoadViewBagData(); // Load any necessary ViewBag data
            var bills = _billService.GetAll(); // Assuming GetAll returns a result containing bills
            if (bills.Success)
            {
                return View(bills.Data); // Return the list of bills to the view
            }
            SetErrorMessage(bills.Message); // Set error message
            return View("Error", bills.Message); // Handle error if fetching bills fails
        }

        // GET: /Bill/Add
        public IActionResult Add()
        {
            LoadViewBagData(); // Load necessary ViewBag data for the Add view
            return View(new BillCreateDto()); // Return a new BillCreateDto to the view
        }

        // POST: /Bill/Add
        [HttpPost]
        public IActionResult Add(BillCreateDto billCreateDto)
        {
            if (ModelState.IsValid)
            {
                var result = _billService.Add(billCreateDto);
                if (result.Success)
                {
                    SetSuccessMessage(result.Message); // Set success message
                    return Json(new { success = true }); // Return JSON success response
                }
                return Json(new { success = false, message = result.Message }); // Return JSON error response
            }

            LoadViewBagData(); // Reload ViewBag data if model state is invalid
            return Json(new { success = false, message = "Invalid model state." }); // Return JSON error response
        }

        // GET: /Bill/Edit/{id}
        public IActionResult Edit(Guid id)
        {
            var bill = _billService.GetById(id);
            if (bill == null)
            {
                SetErrorMessage("Bill not found."); // Set error message
                return View("Error"); // Return an error view if bill not found
            }

            LoadViewBagData(); // Load necessary ViewBag data for the Edit view
            var billUpdateDto = new BillUpdateDto // Convert the retrieved bill to an update DTO
            {
                Id = bill.Data.Id,
                // Map other properties as needed
            };
            return View(billUpdateDto); // Return the update DTO to the view
        }

        // POST: /Bill/Edit
        [HttpPost]
        public IActionResult Update(BillUpdateDto billUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Form is incomplete." }); // Return error if model state is invalid
            }

            var result = _billService.Update(billUpdateDto);
            if (result.Success)
            {
                SetSuccessMessage(result.Message); // Set success message
                return Json(new { success = true }); // Return JSON success response
            }
            return Json(new { success = false, message = result.Message }); // Return JSON error response
        }

        // POST: /Bill/Delete/{id}
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var bill = _billService.GetById(id);
            if (bill == null)
            {
                SetErrorMessage("Bill not found."); // Set error message
                return View("Error"); // Return an error view if bill not found
            }
            var result = _billService.Delete(id);
            if (result.Success)
            {
                SetSuccessMessage(result.Message); // Set success message
                return Json(new { success = true }); // Return JSON success response
            }
            return Json(new { success = false, message = result.Message }); // Return JSON error response
        }

        // Private method to load ViewBag data
        private void LoadViewBagData()
        {
            // Example: Load categories or any other relevant data
            // ViewBag.Categories = _categoryService.GetList().Data;
        }
    }
}
