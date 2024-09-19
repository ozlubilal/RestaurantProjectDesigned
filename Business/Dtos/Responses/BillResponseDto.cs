using Core.Entities;
using Entities.Enums;

namespace Business.Dtos.Responses
{
    public class BillResponseDto : IDto
    {
        public Guid Id { get; set; }
        public string TableName { get; set; } // Masa adı
        public Guid StoreBillId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public BillStatus Status { get; set; } // Enum int ya da enum olarak tutulabilir.
        public List<OrderResponseDto> Orders { get; set; } // Bağlı siparişlerin listesi
    }
}
