using Microsoft.AspNetCore.Mvc;

namespace RestaurantWepApp.Controllers
{
    public class BaseController : Controller
    {
        protected void SetSuccessMessage(string message)
        {
            TempData["SuccessMessage"] = message;
        }

        protected void SetErrorMessage(string message)
        {
            TempData["ErrorMessage"] = message;
        }
        protected void SetWarningMessage(string message) 
        {
            TempData["WarningMessage"]= message;
        }
    }
}
