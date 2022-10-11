using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FakeStoreApi.Helper
{
    public class FileUploader
    {
        public static async Task<string> FileUpload(IFormFile file, IWebHostEnvironment webHostEnvironment)
        {
            if (file == null)
                return null;

            var folder = "Images/ProductImage/";
            folder += Guid.NewGuid().ToString() + "_" + file.FileName;
            var serverPath = Path.Combine(webHostEnvironment.WebRootPath, folder);
            await file.CopyToAsync(new FileStream(serverPath, FileMode.Create));
            return folder;
        }
    }
}
