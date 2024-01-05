using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Bill : IEntity
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public int StoreBillId { get; set; }
        public int BillStatusId { get; set; }
        public DateTime Date { get; set; }
    }
}
