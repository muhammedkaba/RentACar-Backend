using Business.Abstract;
using Core.Utilities.FileHelper;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
                    carImage.ImagePath = FileHelper.Add(files, "Resimler");
                    var result = _carImageService.Add(carImage);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Dosya Girilmedi.");
                }
            }
            catch (Exception)
            {
                return BadRequest("Dosya Girilmedi.");
            }
        }
        [HttpPost("updateimage")]
        public IActionResult Update([FromForm] IFormFile files, [FromForm] int imageId)
        {
            try
            {
                if (files.Length > 0)
                {
                    CarImage carImageToUpdate = _carImageService.GetById(imageId).Data;
                    var result = _carImageService.Update(files, carImageToUpdate);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Dosya Girilmedi.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("deleteimage")]
        public IActionResult Delete([FromForm] int imageId)
        {
            try
            {
                CarImage carImageToDelete = _carImageService.GetById(imageId).Data;
                var result = _carImageService.Delete(carImageToDelete);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("getbycarid")]
        public IActionResult GetByCarId([FromForm] int carId)
        {
            try
            {
                var result = _carImageService.GetByCarId(carId);
                return Ok(result.Data);
            }
            catch (Exception)
            {
                return BadRequest("Bir hata meydana geldi");
            }
        }
    }
}
