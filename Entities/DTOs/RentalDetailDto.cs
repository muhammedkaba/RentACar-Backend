using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class RentalDetailDto : IDto
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public string CarName { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
    }
}
