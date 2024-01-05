using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class SeaterOfTable : IEntity
    {
        public int Id { get; set; }
        public string SeaterOfTableName { get; set; }
        public int SeaterCount { get; set; }
    }
}
