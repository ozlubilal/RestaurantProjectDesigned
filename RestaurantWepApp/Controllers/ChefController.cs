using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Entities.Concretes;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantWepApp.Controllers;

[Authorize(Roles = "Chef")]
public class ChefController : BaseController
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
    private readonly ITableService _tableService;
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly IVisitorService _visitorService;

    public ChefController(IOrderService orderService, IMapper mapper, ITableService tableService, ICategoryService categoryService,
        IProductService productService,IVisitorService visitorService)
    {
        _orderService = orderService;
        _mapper = mapper;
        _tableService = tableService;
        _categoryService = categoryService;
        _productService = productService;
        _visitorService = visitorService;
    }


    [HttpGet]
    public IActionResult Index(Guid? tableId, Guid? categoryId, Guid? productId, int? currentStatus)
    {

        var controllerName = ControllerContext.RouteData.Values["controller"].ToString(); // Controller adı
        var actionName = ControllerContext.RouteData.Values["action"].ToString(); // Action adı

        var visitor = new Visitor
        {
            Url = $"{controllerName}/{actionName}", // Controller ve action isimlerini birleştiriyoruz
                                                    // VisitTime burada otomatik olarak Add metodunda ayarlanacak
        };

        _visitorService.Add(visitor);


        var orders = _orderService.GetList().Data;

        if (currentStatus.HasValue)
        {
            var status = (OrderStatus)currentStatus.Value;
            orders = orders.FindAll(o => o.Status == status);
        }

        if (tableId.HasValue)
        {
            orders = orders.FindAll(o => o.TableId == tableId.Value);
        }
        if (categoryId.HasValue)
        {
            orders = orders.FindAll(o => o.CategoryId == categoryId.Value);
        }
        if (productId.HasValue)
        {
            orders = orders.FindAll(o => o.ProductId == productId.Value);
        }

        var filteredOrders = orders.ToList();

        ViewBag.Tables = _tableService.GetList().Data;
        ViewBag.Categories = _categoryService.GetList().Data;
        ViewBag.Products = _productService.GetList().Data;

        return View(filteredOrders); // Direkt Index görünümüne geçiş
    }


    [HttpGet]
    public IActionResult UpdateOrderStatus(Guid id, OrderStatus newStatus, OrderStatus? currentStatus = null)
    {
        var order = _orderService.GetById(id).Data;
        if (order == null)
        {
            return Json(new { success = false, message = "Order not found." });
        }

        order.Status = newStatus;
        var orderUpdateDto = _mapper.Map<OrderUpdateDto>(order);
        var result = _orderService.Update(orderUpdateDto);

        // Eğer currentStatus boş değilse, aynı filtreyi uygula
        if (currentStatus.HasValue)
        {
            return RedirectToAction("FilterByStatus", new { status = currentStatus.Value });
        }

        // Filtre yoksa tüm siparişlere dön
        return RedirectToAction("Index");
    }

    public IActionResult FilterByStatus(OrderStatus status)
    {
        ViewBag.Tables = _tableService.GetList().Data;
        ViewBag.Categories = _categoryService.GetList().Data;
        ViewBag.Products = _productService.GetList().Data;
        ViewBag.CurrentStatus=(int)status;

        var orders = _orderService.GetList().Data.Where(o => o.Status == status).ToList();
        return View("Index", orders);
    }

 

}
