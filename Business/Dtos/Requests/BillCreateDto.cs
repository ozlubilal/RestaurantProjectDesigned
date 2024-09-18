using Core.Entities;
using Entities.Enums;

namespace Business.Dtos.Requests
{
    public class BillCreateDto:IDto
    {
        public Guid TableId { get; set; }
        public Guid StoreBillId { get; set; }
        public decimal TotalAmount { get; set; }
        public BillStatus Status { get; set; }
    }
}

