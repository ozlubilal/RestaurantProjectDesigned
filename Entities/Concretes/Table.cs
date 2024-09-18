using Core.Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Table : BaseEntity<Guid>
    {
        public string TableName { get; set; }
        public TableStatus Status { get; set; }
    }
}
