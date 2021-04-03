using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult();
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        public IResult Update(UserForUpdateDto user)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(user.Password , out passwordHash, out passwordSalt);
            var userToUpdate = new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            userToUpdate.Id = GetIdByEmail(user.OldEmail).Data.Id;
            _userDal.Update(userToUpdate);
            return new SuccessResult();
        }

        public IDataResult<User> GetIdByEmail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u=>u.Email == email));
        }
    }
}
