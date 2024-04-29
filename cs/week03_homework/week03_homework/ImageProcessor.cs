using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week03_homework.Exceptions;
using week03_homework.Libraries;

namespace week03_homework
{
    public class ImageProcessor
    {
        private readonly IImageProcessingLibrary _imageProcessingLibrary;
        private readonly IFileStorageLibrary _fileStorageLibrary;
        public ImageProcessor(IImageProcessingLibrary imageProcessingLibrary, IFileStorageLibrary fileStorageLibrary)
        {
            _imageProcessingLibrary = imageProcessingLibrary;
            _fileStorageLibrary = fileStorageLibrary;
        }

        public async Task ProcessAndSaveImage(string inputPath, string outputPath)
        {
            ValidateExtension(inputPath);
            ValidateExtension(outputPath);

            var processedImageContent = await ProcessImage(inputPath);
            await SaveImage(outputPath, processedImageContent);

        }


        private async Task<string> ProcessImage(string inputPath)
        {
            try
            {
                return await _imageProcessingLibrary.ProcessImage(inputPath);
            }
            catch (ProcessingErrorException ex)
            {
                throw new ProcessingErrorException("Image Processing failed!", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown error happened during the image processing", ex);
            }
        }

        private async Task SaveImage(string outputPath, string processedImage)
        {
            try
            {
                await _fileStorageLibrary.SaveContentIntoFile(outputPath, processedImage);
            }
            catch (InvalidImageException ex)
            {
                throw new InvalidImageException("Image Saving failed!", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown error happened during the image saving", ex);
            }
        }

        private void ValidateExtension(string path)
        {
            if (!path.EndsWith(".jpg"))
            {
                throw new InvalidImageException("Invalid image extension");
            }
        }
    }
}
