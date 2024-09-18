using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes;

public class EfUserDal : EfEntityRepositoryBase<User, Context>, IUserDal
{
    private readonly Context _context;

    public EfUserDal(DbContextOptions<Context> options) : base(new Context(options))
    {
        _context = new Context(options);
    }

    public List<OperationClaim> GetClaims(User user)
    {
        var result = from operationClaim in _context.OperationClaims
                     join userOperationClaim in _context.UserOperationClaims
                         on operationClaim.Id equals userOperationClaim.OperationClaimId
                     where userOperationClaim.UserId == user.Id
                     select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
        return result.ToList();
    }
}
