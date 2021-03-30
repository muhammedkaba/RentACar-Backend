using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICarService _carService;
        ICustomerService _customerService;

        public RentalManager(IRentalDal rentalDal, ICarService carService, ICustomerService customerService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
            _customerService = customerService;
        }

        public IResult Add(Rental rental)
        {
            IResult result = BusinessRules.Run(
                CheckIfDateIsAvailable(rental),
                CheckIfCustomerFindeksIsEnough(rental)
                );
            {
                if (result != null)
                {
                    return result;
                }
                _rentalDal.Add(rental);
                return new SuccessResult();
            }
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetLastRentalOfCar(int carId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.GetAll(r=> r.CarId == carId).LastOrDefault());
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }

        private IResult CheckIfDateIsAvailable(Rental rental)
        {
            var result = GetLastRentalOfCar(rental.CarId);
            if (result.Data == null)
            {
                return new SuccessResult();
            }
            else if (DateTime.Compare(rental.RentDate,result.Data.ReturnDate) < 0 || DateTime.Compare(rental.RentDate, rental.ReturnDate) >= 0)
            {
                return new ErrorResult("Bu tarihler arasında kiralama işlemi yapamazsınız");
            }
            return new SuccessResult();
        }

        private IResult CheckIfCustomerFindeksIsEnough(Rental rental)
        {
            var car = _carService.GetCarById(rental.CarId);
            var customer = _customerService.GetById(rental.CustomerId);
            if (customer.Data.Findeks < car.Data[0].MinFindeks)
            {
                return new ErrorResult("Findeks puanınız yeterli değil.");
            }
            return new SuccessResult();
        }
    }
}
