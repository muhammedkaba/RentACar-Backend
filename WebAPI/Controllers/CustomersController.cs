using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarUsersController : ControllerBase
    {
        ICarUserService _carUserService;

        public CarUsersController(ICarUserService carUserService)
        {
            _carUserService = carUserService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carUserService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
