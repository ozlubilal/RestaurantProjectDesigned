using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes;

public class Visitor:BaseEntity<Guid> 
{
    public string Url { get; set; } 
    public DateTime VisitTime { get; set; } 
}
