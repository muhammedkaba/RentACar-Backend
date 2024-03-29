﻿using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICreditCardService
    {
        IResult Check(string cardNo);
        IResult Add(CreditCard creditCard);
        IDataResult<List<CreditCard>> GetByCustomerId(int customerId);
        IDataResult<CreditCard> GetById(int id);
    }
}