using AssignmentDemo.API.Controllers;
using AssignmentDemo.API.Models.Photos;
using AssignmentDemo.Entities.API.PhotoDetails;
using AssignmentDemo.Provider.PhotoRequest;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AssignmentDemoAPI.Test.ControllerTest
{
    public class PhotosControllerTest
    {

        /// <summary>
        /// Test case 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void GetPhoto200OKResponse()
        {   //Mocking Setup
            var mockUserRequest = new Mock<IPhotoRequestHandler>();
            mockUserRequest.Setup(req => req.GetPhotos()).ReturnsAsync(GetPhotos());

            var mockIMapper = new Mock<IMapper>();
            mockIMapper.Setup(op => op.Map<List<PhotoDto>>(It.IsAny<List<Photo>>())).Returns(GetPhotosDto());

            //Act
            var controller = new PhotosController(mockUserRequest.Object, mockIMapper.Object);
            var photos = await controller.GetPhotos();

            //Assert
            Assert.IsType<OkObjectResult>(photos.Result);

        }

        [Fact]
        public async void GetPhotosReturnAllItems()
        {   //Mocking Setup
            var mockUserRequest = new Mock<IPhotoRequestHandler>();
            mockUserRequest.Setup(req => req.GetPhotos()).ReturnsAsync(GetPhotos());

            var mockIMapper = new Mock<IMapper>();
            mockIMapper.Setup(op => op.Map<List<PhotoDto>>(It.IsAny<List<Photo>>())).Returns(GetPhotosDto());

            //Act
            var controller = new PhotosController(mockUserRequest.Object, mockIMapper.Object);
            var photos = await controller.GetPhotos();
            var okResult = photos.Result as OkObjectResult;
            var items = Assert.IsType<List<PhotoDto>>(okResult.Value);

            //Assert
            Assert.Equal(2, items.Count);

        }

        [Fact]
        public async void GetAlbumsReturn_NotFoundResponse()
        {
            //Mocking Setup
            var mockUserRequest = new Mock<IPhotoRequestHandler>();
            mockUserRequest.Setup(req => req.GetPhoto(1)).ReturnsAsync(GetPhoto());

            var mockIMapper = new Mock<IMapper>();
            mockIMapper.Setup(op => op.Map<PhotoDto>(It.IsAny<Photo>())).Returns(GetPhotoDto());
            //Act
            var controller = new PhotosController(mockUserRequest.Object, mockIMapper.Object);
            var album = await controller.GetPhoto(3);
            //Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundResult>(album.Result);
        }

        private List<Photo> GetPhotos()
        {

            List<Photo> lst = new List<Photo>()
            {
                new Photo()
                {
                    id = 1,
                    url = "http://google.com",
                    thumbnailUrl= "http://abb.com/acfd",
                    title = "This is Title 1"
                },
                new Photo()
                {
                    id = 2,
                    url = "http://google.com",
                    thumbnailUrl= "http://abb.com/acfd",
                    title = "This is Title 2"
                }
            };
            return lst;

        }

        private List<PhotoDto> GetPhotosDto()
        {

            List<PhotoDto> lst = new List<PhotoDto>()
            {
                new PhotoDto()
                {
                    id = 1,
                    url = "http://google.com",
                    thumbnailUrl= "http://abb.com/acfd",
                    title = "This is Title 1"
                },
                new PhotoDto()
                {
                    id = 2,
                    url = "http://google.com",
                    thumbnailUrl= "http://abb.com/acfd",
                    title = "This is Title 2"
                }
            };
            return lst;

        }

        private Photo GetPhoto()
        {
            return new Photo()
            {
                id = 1,
                url = "http://google.com",
                thumbnailUrl = "http://abb.com/acfd",
                title = "This is Title 1"
            };
        }

        private PhotoDto GetPhotoDto()
        {
            return new PhotoDto()
            {
                id = 1,
                url = "http://google.com",
                thumbnailUrl = "http://abb.com/acfd",
                title = "This is Title 1"
            };
        }
    }
}
