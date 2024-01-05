using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BillId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int UserId { get; set; }
        public int OrderStatusId { get; set; }
    }
}
