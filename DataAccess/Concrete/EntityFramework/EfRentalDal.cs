using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from c in context.Cars
                             join r in context.Rentals
                             on c.Id equals r.CarId
                             join cu in context.Customers
                             on r.CustomerId equals cu.CustomerId
                             select new RentalDetailDto
                             {
                                 RentalId = r.RentalId,
                                 CarId = c.Id,
                                 CarName = c.Description,
                                 UserId = cu.CustomerId,
                                 CompanyName = cu.CompanyName,
                             };
                return result.ToList();

            }
        }
    }
}
