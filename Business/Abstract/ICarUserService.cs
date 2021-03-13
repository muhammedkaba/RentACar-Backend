using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarUserService
    {
        IResult Add(CarUser user);
        IResult Delete(CarUser user);
        IResult Update(CarUser user);
        IDataResult<List<CarUser>> GetAll();
    }
}
