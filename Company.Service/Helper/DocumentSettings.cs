using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //var folderPath = @"D:\\Route\\Route Assignments\\C sharp\\MVC03\\WebApplication1\\wwwroot\\imgs\\";

            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

            var fileName = $"{Guid.NewGuid()}-{file.FileName}";

            var FilePath = Path.Combine(FolderPath, fileName);

            using var FileStream = new FileStream(FilePath, FileMode.Create);

            file.CopyTo(FileStream);

            return fileName;
        }
    }
}
