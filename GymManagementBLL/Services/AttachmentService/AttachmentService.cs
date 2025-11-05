
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
     
        private readonly List<string> allowedExtensions = [".jpg", ".jpeg", ".png"];
        private readonly long maxFileSize = 5 * 1024 * 1024; //5MB

        public string? Upload(string folderName, IFormFile file)
        {
            try
            {
                if (folderName is null || file is null || file.Length == 0) return null;

                //1. Check Extension.
                string extension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                    return null;

                //2. Check Size
                if (file.Length > maxFileSize) return null;

                //3. Get Located Folder Path
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folderName);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                //4. Make Attachment Name Unique (GUID)
                string fileName = Guid.NewGuid().ToString() + extension;

                //5. Get File Path
                var filePath = Path.Combine(folderPath, fileName);

                //6. Create File Stream To Copy File
                using var fileStream = new FileStream(filePath, FileMode.Create);

                //7. Use Stream To Copy File
                file.CopyTo(fileStream);

                //8. Return File Name To Store in Database
                return fileName;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Failed to upload file to folder = {folderName} : {ex}");
                return null;
            }

        }

        public bool Delete(string fileName, string folderName)
        {
            try
            {
                if(string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folderName)) 
                    return false;

                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName, fileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete {fileName} : {ex}");
                return false;
            }
        }
    }
}
