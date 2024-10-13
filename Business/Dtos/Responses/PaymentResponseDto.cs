using Core.Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Responses;

public class PaymentResponseDto:IDto
{
    public Guid BillId { get; set; }
    public Guid StoreBillId { get; set; }
    public Guid TableId { get; set; }
    public string TableName { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}
