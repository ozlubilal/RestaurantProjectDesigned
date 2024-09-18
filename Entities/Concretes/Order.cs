using Core.Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order : BaseEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid BillId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus Status { get; set; }

        // Navigation Property
        public Product Product { get; set; }
        public Bill Bill { get; set; }
    }
}
