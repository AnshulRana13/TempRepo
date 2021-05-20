using AssignmentDemo.API.Controllers;
using AssignmentDemo.API.Models.Albums;
using AssignmentDemo.Entities.API.AlbumDetails;
using AssignmentDemo.Provider.AlbumRequest;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AssignmentDemoAPI.Test.ControllerTest
{
    public class AlbumsControllerTest
    {

        /// <summary>
        /// Test case 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void GetAlbum200OKResponse()
        {   //Mocking Setup
            var mockUserRequest = new Mock<IAlbumRequestHandler>();
            mockUserRequest.Setup(req => req.GetAlbums()).ReturnsAsync(GetAlbums());

            var mockIMapper = new Mock<IMapper>();
            mockIMapper.Setup(op => op.Map<List<AlbumDto>>(It.IsAny<List<Album>>())).Returns(GetAlbumsDto());

            //Act
            var controller = new AlbumsController(mockUserRequest.Object, mockIMapper.Object);
            var albums = await controller.GetAlbums();

            //Assert
            Assert.IsType<OkObjectResult>(albums.Result);

        }

        [Fact]
        public async void GetAlbumsReturnAllItems()
        {   //Mocking Setup
            var mockUserRequest = new Mock<IAlbumRequestHandler>();
            mockUserRequest.Setup(req => req.GetAlbums()).ReturnsAsync(GetAlbums());

            var mockIMapper = new Mock<IMapper>();
            mockIMapper.Setup(op => op.Map<List<AlbumDto>>(It.IsAny<List<Album>>())).Returns(GetAlbumsDto());

            //Act
            var controller = new AlbumsController(mockUserRequest.Object, mockIMapper.Object);
            var albums = await controller.GetAlbums();
            var okResult = albums.Result as OkObjectResult;
            var items = Assert.IsType<List<AlbumDto>>(okResult.Value);

            //Assert
            Assert.Equal(4, items.Count);

        }

        [Fact]
        public async void GetAlbumsReturn_NotFoundResponse()
        {
            //Mocking Setup
            var mockUserRequest = new Mock<IAlbumRequestHandler>();
            mockUserRequest.Setup(req => req.GetAlbum(1)).ReturnsAsync(GetAlbum());

            var mockIMapper = new Mock<IMapper>();
            mockIMapper.Setup(op => op.Map<AlbumDto>(It.IsAny<Album>())).Returns(GetAlbumDTo());
            //Act
            var controller = new AlbumsController(mockUserRequest.Object, mockIMapper.Object);
            var album = await controller.GetAlbumByUserId(2);
            //Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundResult>(album.Result);
        }

        private List<Album> GetAlbums()
        {
            List<Album> lst = new List<Album>()
            {
                new Album{ id =1, userId =1, title="This is title  1"},
                new Album {id= 2, userId=1, title= "This is Title 2"},
                new Album {id= 3, userId=2, title= "This is Title 2"},
                new Album {id= 4, userId=2, title= "This is Title 2"}

            };
            return lst;
        }

        private List<AlbumDto> GetAlbumsDto()
        {
            List<AlbumDto> lst = new List<AlbumDto>()
            {
                new AlbumDto { id =1, userId =1, title="This is title  1"},
                new AlbumDto {id= 2, userId=1, title= "This is Title 2"},
                new AlbumDto {id= 3, userId=2, title= "This is Title 2"},
                new AlbumDto {id= 4, userId=2, title= "This is Title 2"}

            };
            return lst;
        }

        private Album GetAlbum()
        {
            return new Album()
            {
                id = 1,
                userId = 1,
                title = "This is title  1"
            };
        }


        private AlbumDto GetAlbumDTo()
        {
            return new AlbumDto()
            {
                id = 1,
                userId = 1,
                title = "This is title  1"
            };
        }

    }
}
