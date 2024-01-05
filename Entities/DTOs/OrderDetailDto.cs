using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class OrderDetailDto : IDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public int StoreBillId { get; set; }
        public string TableName { get; set; }
        public int TableStatuId { get; set; }
        public int BillId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string OrderStatusName { get; set; }
        public int BillStatuId { get; set; }
    }
}
