using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes;

public class Payment:BaseEntity<Guid>
{
    public Guid BillId { get; set; }
    public decimal Amount { get; set; } 
    public string PaymentMethod { get; set; }

    public Bill? Bill { get; set; }
    // public bool IsCompleted { get; set; }
}
