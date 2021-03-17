using Core.Entities;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.FileHelper
{
    public static class FileHelper
    {
        public static string Add(IFormFile file, string str, string path)
        {
            //string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName + $"\\{str}\\");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileInfo fileInfo = new FileInfo(file.FileName);
            using (FileStream fileStream = File.Create(path + Guid.NewGuid().ToString() + fileInfo.Extension))
            {
                file.CopyTo(fileStream);
                return fileStream.Name;
            }
        }
        public static void Update(IFormFile file, string path)
        {
            try
            {
                File.Delete(path);
                using (FileStream fileStream = File.Create(path))
                {
                    file.CopyTo(fileStream);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void Delete(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}