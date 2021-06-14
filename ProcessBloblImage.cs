using System;
using System.IO;
using System.Linq;
using System.Drawing;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace BloblStorage.GenerateThumbnail
{
    public static class ProcessBlobImage
    {
        [FunctionName("ResizeImage")]
        public static void Run([BlobTrigger("rawimages/{name}", Connection = "imageuploadprocessing_STORAGE")]Stream inputBlob,
                               [Blob("processedimages/{name}", FileAccess.Write)]Stream outputImage,
                                string name,
                                ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {inputBlob.Length} Bytes");

            // Check extension
            var allowedExtensions = new[] { ".jpg", ".png", ".bmp" };
            string extension = Path.GetExtension(name);
            if(!allowedExtensions.Contains(extension))
            {
                log.LogError($"{name} blob is not a valid image.");
            }

            // Get image
            var image = Image.FromStream(inputBlob);

            // Calculate the width and the height
            int newWidth = 500;
            double ratio = Convert.ToDouble(image.Width) / Convert.ToDouble(image.Height);
            int newHeight = Convert.ToInt32(Math.Round(newWidth / ratio));

            var bitmap = new Bitmap(image);
            var thumbnailImage = bitmap.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

            thumbnailImage.Save(outputImage, image.RawFormat);
            log.LogInformation($"Thumbnail for image {name} has been created with the dimensions {newWidth} x {newHeight}");
        }
    }
}
