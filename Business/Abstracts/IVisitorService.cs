using Core.Utilities.Results;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts;

public interface IVisitorService
{
    IDataResult<List<Visitor>> GetAll(); // Tüm ziyaretçi verilerini getir
    IDataResult<Visitor> GetVisitorByDate(DateTime date); // Ziyaretçi nesnesini ID ile getir

    IResult Add(Visitor visitor); // Yeni ziyaretçi ekle
    IResult Delete(Guid id); // Ziyaretçiyi sil
    IResult DeleteAll();

}
