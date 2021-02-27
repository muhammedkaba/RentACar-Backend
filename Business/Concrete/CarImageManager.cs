using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(CarImage carImage)
        {
            IResult result = BusinessRules.Run(
                CheckIfHasMoreThanFiveImages(carImage.CarId)
                );
            if (result != null)
            {
                return result;
            }
            CarImage carImageToAdd = new CarImage
            {
                CarId = carImage.CarId,
                ImagePath = carImage.ImagePath
            };
            carImageToAdd.Date = DateTime.Now;
            _carImageDal.Add(carImageToAdd);
            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            FileHelper.Delete(carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            var result = _carImageDal.GetAll(ci => ci.CarId == carId);
            if (result.Any())
            {
                return new SuccessDataResult<List<CarImage>>(result);
            }
            result = _carImageDal.GetAll(ci => ci.CarId == 999);
            return new SuccessDataResult<List<CarImage>>(result);
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(ci => ci.Id == id));
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            FileHelper.Update(file, carImage.ImagePath);
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

        private IResult CheckIfHasMoreThanFiveImages(int carId)
        {
            var result = GetByCarId(carId);
            if (result.Data.Count >= 5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
}
