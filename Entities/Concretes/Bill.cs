using Core.Entities;
using Entities.Concretes;
using Entities.Enums;

namespace Entities.Concrete;

public class Bill : BaseEntity<Guid>
{
    public Guid TableId { get; set; }
    public BillStatus Status { get; set; }
    public Guid StoreBillId { get; set; }
    public decimal TotalAmount { get; set; }

    //NavigationProperty
    public Table? Table { get; set; }
    public StoreBill StoreBill { get; set; }
    public List<Order> Orders { get; set; } = new List<Order>();
}
