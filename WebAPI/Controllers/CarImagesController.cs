using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        public static IWebHostEnvironment _webHostEnviroment;
        ICarImageService _carImageService;

        public CarImagesController(IWebHostEnvironment webHostEnvironment, ICarImageService carImageService)
        {
            _webHostEnviroment = webHostEnvironment;
            _carImageService = carImageService;
        }

        [HttpPost("addimage")]
        public IActionResult Add([FromForm] IFormFile files, [FromForm] CarImage carImage)
        {
            try
            {
                if (files.Length > 0)
                {
                    string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\Resimler\\");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + Guid.NewGuid().ToString() + ".jpg"))
                    {
                        files.CopyTo(fileStream);
                        carImage.ImagePath = fileStream.Name;
                        carImage.Date = DateTime.Now;
                        var result = _carImageService.Add(carImage);
                        return Ok(result);
                    }

                }
                else
                {
                    return BadRequest(carImage);
                }
            }
            catch (Exception)
            {

                return BadRequest(carImage);
            }
        }


    }
}
