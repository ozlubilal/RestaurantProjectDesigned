using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class TableDetailDto : IDto
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string FloorName { get; set; }
        public string SeaterOfTableName { get; set; }
        public string TableStatusName { get; set; }
    }
}
