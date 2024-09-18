using Core.Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes;

public class StoreBill : BaseEntity<Guid>
{
    public decimal TotalAmount { get; set; } // Günlük toplam tutar
    public StoreBillStatus Status { get; set; } // Açık/Kapalı durumu
}
