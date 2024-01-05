using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class BillDetailDto : IDto
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string BillStatusName { get; set; }
        public int BillPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
