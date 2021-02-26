using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarImageManager carImageManager = new CarImageManager(new EfCarImageDal());

            carImageManager.Add(new CarImage
            {
                CarId = 1,
                Date = DateTime.Now,
                ImagePath = "eq321312321"
            });

            //CarManager carManager = new CarManager(new EfCarDal());

            //var result = carManager.GetCarDetails();
            //foreach (var car in result.Data)
            //{
            //    Console.WriteLine(car.CarName+"//"+car.ColorName+"//"+car.BrandName);
            //}
            //Console.WriteLine(result.Message);
        }
    }
}
