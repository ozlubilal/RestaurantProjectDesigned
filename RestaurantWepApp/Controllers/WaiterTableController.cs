    using AutoMapper;
    using Business.Abstract;
    using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Requests;
    using Business.Dtos.Responses;
using Core.Extensions;
using Entities.Concretes;
    using Entities.Enums;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Drawing.Printing;
    using System.Security.Claims;

    namespace RestaurantWepApp.Controllers;

    [Authorize(Roles = "Waiter")]
    public class WaiterTableController : BaseController
    {
        private readonly ITableService _tableService;
        private readonly IOrderService _orderService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IBillService _billService;
        private readonly IStoreBillService _storeBillService;
        private readonly IMapper _mapper;
        private readonly IVisitorService _visitorService;

        public WaiterTableController(ITableService tableService, IMapper mapper, IOrderService orderService,
            IProductService productService, ICategoryService category,IBillService billService, IStoreBillService storeBillService, IVisitorService visitorService)
        {
            _tableService = tableService;
            _orderService = orderService;
            _productService = productService;
            _categoryService = category;
            _billService = billService;
            _mapper = mapper;
            _storeBillService = storeBillService;
            _visitorService = visitorService;
        }

        public IActionResult Index()
        {
            var controllerName = ControllerContext.RouteData.Values["controller"].ToString(); // Controller adı
            var actionName = ControllerContext.RouteData.Values["action"].ToString(); // Action adı

            var visitor = new Visitor
            {
                Url = $"{controllerName}/{actionName}", // Controller ve action isimlerini birleştiriyoruz
                                                        // VisitTime burada otomatik olarak Add metodunda ayarlanacak
            };

            _visitorService.Add(visitor);

            var result = _tableService.GetList();
            if (result.Success)
            {
                var tableDtos = _mapper.Map<List<TableResponseDto>>(result.Data);
                return View(tableDtos);
            }
            return View("Error", result.Message);
        }
        public IActionResult TableOrderDetails(Guid tableId)
        {
            var table = _tableService.GetById(tableId).Data;
            var bill=_billService.GetByTableId(tableId).Data.Where(b=>b.Status==BillStatus.Aktif).FirstOrDefault();
            ViewBag.categories = _categoryService.GetList().Data;
            var productResponseDto = _productService.GetList().Data;
            ViewBag.products = _mapper.Map<List<Product>>(productResponseDto);
            ViewBag.table = table;
            if (bill!=null)
            {
                ViewBag.billId = bill.Id;

            }
            var result = _orderService.GetByTableId(tableId);
            if (result.Success)
            {
                return View(result.Data);
            }
            return View("Error", result.Message);
        }
        [HttpGet]
        public IActionResult AddOrder(Guid productId, Guid billId, Guid tableId)
        {
            // Sistemde giriş yapan kullanıcının Id'sini alalım
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                // Eğer kullanıcı giriş yapmamışsa hata verelim
                SetErrorMessage("Kullanıcı oturumu bulunamadı. Lütfen giriş yapın.");
                return RedirectToAction("Login", "Account"); // Örneğin, kullanıcıyı giriş sayfasına yönlendirebilirsiniz
            }

            var userId = Guid.Parse(userIdClaim.Value);

            // Aynı billId ve productId ile aynı statüde bir sipariş olup olmadığını kontrol edelim
            var existingOrder = _orderService.GetByBillId(billId).Data
                .FirstOrDefault(o => o.ProductId == productId && o.Status == OrderStatus.Hazırlanıyor);

            if (existingOrder != null)
            {
                // Eğer sipariş zaten varsa, adedi artır ve güncelle
                existingOrder.Quantity += 1;
                var orderUpdateDto = new OrderUpdateDto
                {
                    Id = existingOrder.Id,
                    Quantity = existingOrder.Quantity,
                    Status = existingOrder.Status
                };
                var updateResult = _orderService.Update(orderUpdateDto);

                if (updateResult.Success)
                {
                    return RedirectToAction("TableOrderDetails", new { tableId = tableId });
                }

                SetErrorMessage(updateResult.Message);
                return RedirectToAction("TableOrderDetails", new { tableId = tableId });
            }

            // Ürün bilgisini almak için ProductService'den ürünü çek
            var productResult = _productService.GetById(productId);
            if (!productResult.Success || productResult.Data == null)
            {
                SetErrorMessage(Messages.ProductNotFound);
                return RedirectToAction("TableOrderDetails", new { tableId = tableId });
            }

            // Eğer aynı ürün için sipariş yoksa, yeni bir sipariş oluştur
            var product = productResult.Data;
            var orderCreateDto = new OrderCreateDto
            {
                ProductId = productId,
                Status = OrderStatus.Hazırlanıyor,
                Price = product.Price,  // Ürünün fiyatını Order'a ekle
                Quantity = 1,
                BillId = billId,
                UserId = userId  // Dinamik olarak giriş yapan kullanıcının Id'sini kullanıyoruz
            };

            var result = _orderService.Add(orderCreateDto);

            // Sipariş başarıyla eklendiyse TableOrderDetails sayfasına yönlendirin
            if (result.Success)
            {
                return RedirectToAction("TableOrderDetails", new { tableId = tableId });
            }

            // Eğer ekleme başarısız olursa, hata mesajı ile geri yönlendirin
            SetErrorMessage(result.Message);
            return RedirectToAction("TableOrderDetails", new { tableId = tableId });
        }


        [HttpPost]
        public IActionResult UpdateOrderQuantity(Guid orderId, int quantity)
        {
            // Logic to update the order quantity in your database
            // For example:
            var order = _orderService.GetById(orderId).Data; 
            if (order != null && quantity>0)
            {
                order.Quantity = quantity; // Update the quantity
                var orderUpdateDto = _mapper.Map<OrderUpdateDto>(order);

                // Update the order in the database
                _orderService.Update(orderUpdateDto); 
            }
            else if(order != null && quantity==0) 
            {
                _orderService.Delete(order.Id);
            }

            return Json(new { success = true }); // Return a success response
        }

        [HttpGet]
        public IActionResult OpenBill(Guid tableId) 
        {
            var storeBillId = _storeBillService.GetByStatus(StoreBillStatus.Açık).Data.FirstOrDefault().Id;
            BillCreateDto billCreateDto = new BillCreateDto();
            billCreateDto.TableId = tableId;
            billCreateDto.StoreBillId = storeBillId;
            billCreateDto.Status = BillStatus.Aktif;
            var result=_billService.Add(billCreateDto);
            if(result.Success)
            {
                SetSuccessMessage(result.Message);
                return RedirectToAction("TableOrderDetails", new { tableId = tableId });
            }
            SetErrorMessage(result.Message);
            return RedirectToAction("TableOrderDetails", new { tableId = tableId });
        }

        [HttpGet]
        public IActionResult Delete(Guid billId)
        {
           var result= _billService.Delete(billId);
            if (result.Success)
            {
                SetWarningMessage(result.Message);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DeleteBill(Guid billId)
        {
            var result = _billService.Delete(billId);
            if (result.Success)
            {
                SetWarningMessage(result.Message);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    [HttpGet]
    public IActionResult DeleteOrder(Guid orderId, Guid tableId)
    {
        var order = _orderService.GetById(orderId).Data;

        // Eğer sipariş 'Hazırlanıyor' ise herkes silebilir
        if (order.Status == OrderStatus.Hazırlanıyor)
        {
            var result = _orderService.Delete(orderId);
            if (result.Success)
            {
                return RedirectToAction("TableOrderDetails", new { tableId = tableId });
            }
            SetErrorMessage(result.Message);
            return RedirectToAction("TableOrderDetails", new { tableId = tableId });
        }

        // Sipariş 'Hazırlanıyor' değilse sadece chef rolü silebilir
        var roleClaims = HttpContext.User.ClaimRoles();
        if (!roleClaims.Contains("Chef"))
        {
            SetErrorMessage(Messages.JustChefDelete);
            return RedirectToAction("TableOrderDetails", new { tableId = tableId });
        }

        var deleteResult = _orderService.Delete(orderId);
        if (deleteResult.Success)
        {
            return RedirectToAction("TableOrderDetails", new { tableId = tableId });
        }

        SetErrorMessage(deleteResult.Message);
        return RedirectToAction("TableOrderDetails", new { tableId = tableId });
    }


}
