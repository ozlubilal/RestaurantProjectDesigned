using AutoMapper;
using Business.Abstract;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using DataAccess.Migrations;
using Entities.Concretes;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantWepApp.Controllers;

[Authorize(Roles = "Cashier")]
public class CashierController : BaseController
{
    private readonly ITableService _tableService;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;
    private readonly ICategoryService _categoryService;
    private readonly IBillService _billService;
    private readonly IPaymentService _paymentService;
    private readonly IStoreBillService _storeBillService;
    private readonly IVisitorService _visitorService;

    public CashierController(ITableService tableService, IMapper mapper, IProductService productService, IOrderService orderService, 
        ICategoryService categoryService, IBillService billService, IPaymentService paymentService, IStoreBillService storeBillService, IVisitorService visitorService)
    {
        _tableService = tableService;
        _mapper = mapper;
        _productService = productService;
        _orderService = orderService;
        _categoryService = categoryService;
        _billService = billService;
        _paymentService = paymentService;
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
            // Açık olan store bill'i bul
            var storeBillId = _storeBillService.GetByStatus(StoreBillStatus.Açık).Data.FirstOrDefault()?.Id;
            if (storeBillId == null)
            {
                return View("Error", "Açık bir StoreBill bulunamadı.");
            }

            decimal cashTotal = 0;
            decimal creditCardTotal = 0;

            // Bu store bill ile ilişkili ödemeleri al           
            var allPayments = _paymentService.GetList().Data;
            if (allPayments!=null)
            {
                var payments = allPayments.FindAll(p => p.StoreBillId == storeBillId);

                if (payments != null && payments.Count > 0)
                {
                    // Ödemeler üzerinden nakit ve kredi kartı toplamlarını hesapla
                    foreach (var item in payments)
                    {
                        if (item.PaymentMethod == PaymentMethod.Nakit)
                        {
                            cashTotal += item.Amount; // Nakit ödeme toplama
                        }
                        else if (item.PaymentMethod == PaymentMethod.Kredi_Kartı)
                        {
                            creditCardTotal += item.Amount; // Kredi kartı ödeme toplama
                        }
                    }
                }
            }
            else
            {
                ViewBag.PaymentMessage = "Bu StoreBill'e ait ödeme bulunamadı.";
            
            }

            // Toplam tahsilat hesapla
            decimal totalCollection = cashTotal + creditCardTotal;

            // Dinamik değerleri ViewBag'e aktar
            ViewBag.TotalCollection = totalCollection;
            ViewBag.CashTotal = cashTotal;
            ViewBag.CreditCardTotal = creditCardTotal;

            // Tablo verilerini DTO'ya mapleyip gönder
            var tableDtos = _mapper.Map<List<TableResponseDto>>(result.Data);
            return View(tableDtos);
        }
        return View("Error", result.Message);
    }



    [HttpGet]
    public IActionResult ProcessPayment(Guid tableId)
    {
        // Ödeme alma işlemleri
        var result = _tableService.GetById(tableId);
        if (result.Success)
        {
            // İlgili masa bilgileriyle ödeme formunu göster
            return View(result.Data);
        }
        return View("Error", result.Message);
    }

    [HttpGet]
    public IActionResult TableOrderDetails(Guid tableId)
    {
        var table = _tableService.GetById(tableId).Data;
        var bill = _billService.GetByTableId(tableId).Data.Where(b => b.Status == BillStatus.Aktif).FirstOrDefault();
        var productResponseDto = _productService.GetList().Data;
        ViewBag.bill = bill;    
        ViewBag.table = table;
        if (bill != null)
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
    public IActionResult DeleteOrder(Guid orderId, Guid tableId)
    {
        var result = _orderService.Delete(orderId);

        if (result.Success)
        {
            return RedirectToAction("TableOrderDetails", new { tableId = tableId });
        }

        SetErrorMessage(result.Message);
        return RedirectToAction("TableOrderDetails", new { tableId = tableId });
    }
    [HttpPost]
    public IActionResult MarkAsServed(Guid orderId)
    {
        var order = _orderService.GetById(orderId).Data;
        if (order != null)
        {
            var orderUpdateDto = new OrderUpdateDto
            {
                Id = order.Id,
                Quantity = order.Quantity,
                Status = OrderStatus.Servis_Edildi
            };
            var updateResult = _orderService.Update(orderUpdateDto);

            if (updateResult.Success)
            {
                return RedirectToAction("TableOrderDetails", new { tableId = order.TableId });
            }
        }

        // Hata durumu
        return RedirectToAction("TableOrderDetails", new { tableId = ViewBag.table.Id });
    }
    [HttpPost]
    public IActionResult CompletePayment(Guid billId, decimal totalAmount, PaymentMethod paymentMethod,Guid tableId)
    {
        // DTO oluşturma
        PaymentCreateDto paymentCreateDto = new PaymentCreateDto
        {
            BillId = billId,
            Amount = totalAmount,
            PaymentMethod = paymentMethod, // Enum değerini doğrudan kullanabilirsiniz.
        };

        // Payment ekleme
        var result = _paymentService.Add(paymentCreateDto);

        // Sonuç kontrolü
        if (result.Success)
        {
            SetSuccessMessage(result.Message);
            return RedirectToAction("Index", "Cashier"); // Başarı durumunda yönlendirme
        }

        // Hata durumunda geri dönme veya hata mesajı ayarlama
        SetErrorMessage(result.Message);
        return RedirectToAction("TableOrderDetails", new {tableId=tableId});
    }


}
