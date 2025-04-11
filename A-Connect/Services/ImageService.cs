using System;
using System.IO;
using System.Threading.Tasks;

namespace A_Connect.Services
{
    public class ImageService
    {
        public static async Task<string> PickAndSaveImageAsync()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Select an image"
                });

                if (result == null)
                    return null;

                // Create a unique filename for the image
                string fileName = Path.GetRandomFileName() + Path.GetExtension(result.FileName);
                string targetPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                // Copy the file to app storage
                using (var sourceStream = await result.OpenReadAsync())
                using (var targetStream = File.Create(targetPath))
                {
                    await sourceStream.CopyToAsync(targetStream);
                }

                return targetPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Image picking error: {ex.Message}");
                return null;
            }
        }

        public static async Task<FileResult> TakePhotoAsync()
        {
            if (!MediaPicker.IsCaptureSupported)
                return null;

            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();

                if (photo == null)
                    return null;

                // Create a unique filename for the image
                string fileName = Path.GetRandomFileName() + Path.GetExtension(photo.FileName);
                string targetPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                // Copy the file to app storage
                using (var sourceStream = await photo.OpenReadAsync())
                using (var targetStream = File.Create(targetPath))
                {
                    await sourceStream.CopyToAsync(targetStream);
                }

                return photo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Camera error: {ex.Message}");
                return null;
            }
        }

        public static string GetImageFullPath(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return null;

            return imagePath;
        }
    }
}