using Microsoft.VisualStudio.TestTools.UnitTesting;
using week03_homework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using week03_homework.Exceptions;
using week03_homework.Libraries;

namespace week03_homework.Tests
{
    [TestClass()]
    public class ImageProcessorTests
    {
        private ImageProcessor _imageProcessor;
        private Mock<IImageProcessingLibrary> _mockedImageProcessingLibrary;
        private Mock<IFileStorageLibrary> _mockedFileStorageLibrary;


        [TestInitialize]
        public void TestInitialize()
        {
            _mockedImageProcessingLibrary = new Mock<IImageProcessingLibrary>();
            _mockedFileStorageLibrary = new Mock<IFileStorageLibrary>();
            _imageProcessor= new ImageProcessor(_mockedImageProcessingLibrary.Object, _mockedFileStorageLibrary.Object);
        }

        [TestMethod()]
        public async Task ShouldProcessAndSaveImage_WhenInputIsValid()
        {
            //Arrange
            string inputpath = @"c:\Photos\test.jpg";
            string outputpath = @"c:\Photos\processed\test.jpg";
            _mockedImageProcessingLibrary.Setup(x => x.ProcessImage(It.IsAny<string>())).ReturnsAsync(()=>
            {
                return "Processed image";
            });
            _mockedFileStorageLibrary.Setup(x => x.SaveContentIntoFile(It.IsAny<string>(), It.IsAny<string>()));

            //Act
            await _imageProcessor.ProcessAndSaveImage(inputpath, outputpath);
            //Assert
            _mockedImageProcessingLibrary.Verify(x => x.ProcessImage(It.IsAny<string>()), Times.Once);
            _mockedFileStorageLibrary.Verify(x => x.SaveContentIntoFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod()]
        public async Task ShouldThrowProcessingErrorException_WhenProcessImageThrowsProcessingErrorException()
        {
            //Arrange
            string inputpath = @"c:\Photos\test.jpg";
            string outputpath = @"c:\Photos\processed\test.jpg";
            _mockedImageProcessingLibrary.Setup(x => x.ProcessImage(It.IsAny<string>())).ThrowsAsync(new ProcessingErrorException());
            _mockedFileStorageLibrary.Setup(x => x.SaveContentIntoFile(It.IsAny<string>(), It.IsAny<string>()));
            
            //Act&Assert
            await Assert.ThrowsExceptionAsync<ProcessingErrorException>(async () => await _imageProcessor.ProcessAndSaveImage(inputpath, outputpath));
            _mockedImageProcessingLibrary.Verify(x => x.ProcessImage(It.IsAny<string>()), Times.Once);
            _mockedFileStorageLibrary.Verify(x => x.SaveContentIntoFile(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod()]
        public async Task ShouldThrowException_WhenProcessImageThrowsException()
        {
            //Arrange
            string inputpath = @"c:\Photos\test.jpg";
            string outputpath = @"c:\Photos\processed\test.jpg";
            _mockedImageProcessingLibrary.Setup(x => x.ProcessImage(It.IsAny<string>())).ThrowsAsync(new Exception());
            _mockedFileStorageLibrary.Setup(x => x.SaveContentIntoFile(It.IsAny<string>(), It.IsAny<string>()));

            //Act&Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _imageProcessor.ProcessAndSaveImage(inputpath, outputpath));
            _mockedImageProcessingLibrary.Verify(x => x.ProcessImage(It.IsAny<string>()), Times.Once);
            _mockedFileStorageLibrary.Verify(x => x.SaveContentIntoFile(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }


        [TestMethod()]
        public async Task ShouldThrowInvalidImageException_WhenSaveImageThrowsInvalidImageException()
        {
            //Arrange
            string inputpath=@"c:\Photos\test.jpg";
            string outputpath=@"c:\Photos\processed\test.jpg";
            _mockedImageProcessingLibrary.Setup(x => x.ProcessImage(It.IsAny<string>())).ReturnsAsync( () =>
            {
                return "Processed image";
            });
            _mockedFileStorageLibrary.Setup(x => x.SaveContentIntoFile(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new InvalidImageException());

            //Act&Assert
            await Assert.ThrowsExceptionAsync<InvalidImageException>(async () => await _imageProcessor.ProcessAndSaveImage(inputpath, outputpath));
            _mockedImageProcessingLibrary.Verify(x => x.ProcessImage(It.IsAny<string>()), Times.Once);
            _mockedFileStorageLibrary.Verify(x => x.SaveContentIntoFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod()]
        public async Task ShouldThrowException_WhenSaveImageThrowsException()
        {
            //Arrange
            string inputpath = @"c:\Photos\test.jpg";
            string outputpath = @"c:\Photos\processed\test.jpg";
            _mockedImageProcessingLibrary.Setup(x => x.ProcessImage(It.IsAny<string>())).ReturnsAsync(() =>
            {
                return "Processed image";
            });
            _mockedFileStorageLibrary.Setup(x => x.SaveContentIntoFile(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());

            //Act&Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _imageProcessor.ProcessAndSaveImage(inputpath, outputpath));
            _mockedImageProcessingLibrary.Verify(x => x.ProcessImage(It.IsAny<string>()), Times.Once);
            _mockedFileStorageLibrary.Verify(x => x.SaveContentIntoFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        //Get unit test should add .png extensions into inputpath and outputpath thrown exception becouse jpg is expected
        [DataTestMethod()]
        [DataRow(@"c:\Photos\test.png", @"c:\Photos\processed\test.jpg")]
        [DataRow(@"c:\Photos\test.jpg", @"c:\Photos\processed\test.png")]
        [DataRow(@"c:\Photos\test.png", @"c:\Photos\processed\test.png")]
        public async Task ShouldThrowInvalidImageException_WhenSaveImageFailsWithDifferentExtension(string inputpath, string outputpath)
        {
            //Arrange
            _mockedImageProcessingLibrary.Setup(x => x.ProcessImage(It.IsAny<string>())).ReturnsAsync(() =>
            {
                return "Processed image";
            });
            _mockedFileStorageLibrary.Setup(x => x.SaveContentIntoFile(It.IsAny<string>(), It.IsAny<string>()));

            //Act&Assert
            await Assert.ThrowsExceptionAsync<InvalidImageException>(async () => await _imageProcessor.ProcessAndSaveImage(inputpath, outputpath));
            _mockedImageProcessingLibrary.Verify(x => x.ProcessImage(It.IsAny<string>()), Times.Never);
            _mockedFileStorageLibrary.Verify(x => x.SaveContentIntoFile(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }   
    }
}