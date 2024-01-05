using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        User GetByMail(string email);
        IDataResult<User> GetById(int id);
        IResult Delete(User user);
        IResult Update(User user);
        IDataResult<UserDetailDto> GetUserDetailByMail(string email);
        public IDataResult<List<UserDetailDto>> GetAllUserDetail();
    }
}
