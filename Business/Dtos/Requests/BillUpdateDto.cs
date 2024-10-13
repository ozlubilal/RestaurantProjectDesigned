using Core.Entities;
using Entities.Enums;

namespace Business.Dtos.Requests;

public class BillUpdateDto:IDto
{
    public Guid Id { get; set; }
    public Guid TableId { get; set; }
    public Guid StoreBillId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime? ClosedDate { get; set; }
    public BillStatus Status { get; set; } // Enum int olarak tutulabilir veya string
}
