using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<User> GetIdByEmail(string email);
        IResult Add(User user);
        IResult Update(UserForRegisterDto user);
        IDataResult<User> GetByMail(string email);
    }
}
