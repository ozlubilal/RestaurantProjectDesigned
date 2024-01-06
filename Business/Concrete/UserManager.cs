using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constans;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
   
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }
        [SecuredOperation("Admin")]
        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }
        public IDataResult<UserDetailDto> GetUserDetailByMail(string email)
        {
            return new SuccessDataResult<UserDetailDto>(_userDal.GetUserDetails().Where(u => u.Email == email).FirstOrDefault());
        }
        public IDataResult<List<UserDetailDto>> GetAllUserDetail()
        {
            return new SuccessDataResult<List<UserDetailDto>>(_userDal.GetUserDetails());
        }
        [SecuredOperation("Admin")]
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u=>u.Id == id));
        }
        [SecuredOperation("Admin")]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UpdatedSuccess);
        }
    }
}
