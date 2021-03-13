using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarUserManager : ICarUserService
    {
        ICarUserDal _userDal;
        public CarUserManager(ICarUserDal userDal)
        {
            _userDal = userDal;
        }
        public IResult Add(CarUser user)
        {
            _userDal.Add(user);
            return new SuccessResult();
        }

        public IResult Delete(CarUser user)
        {
            _userDal.Delete(user);
            return new SuccessResult();
        }

        public IDataResult<List<CarUser>> GetAll()
        {
            return new SuccessDataResult<List<CarUser>>(_userDal.GetAll());
        }

        public IResult Update(CarUser user)
        {
            _userDal.Update(user);
            return new SuccessResult();
        }
    }
}
