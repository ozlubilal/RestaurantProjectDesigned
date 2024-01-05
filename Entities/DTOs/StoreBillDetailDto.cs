using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class StoreBillDetailDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string StoreBillStatusName { get; set; }
        public int BillOfPayedCount { get; set; }
        public int StoreBillAmount { get; set; }
    }
}
