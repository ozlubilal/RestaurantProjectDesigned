using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Table : IEntity
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public int FloorId { get; set; }
        public int SeaterOfTableId { get; set; }
        public int TableStatusId { get; set; }
    }
}
