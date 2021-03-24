using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CreditCard : IEntity
    {
        public int Id { get; set; }
        public string CardNo { get; set; }
        public string Name { get; set; }
        public string ExpiringDate { get; set; }
        public string CVV { get; set; }
    }
}