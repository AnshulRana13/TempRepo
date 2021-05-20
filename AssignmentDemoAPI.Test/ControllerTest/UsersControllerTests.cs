using AssignmentDemo.API.Controllers;
using AssignmentDemo.Provider.UserRequest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Moq;
using AssignmentDemo.Entities.API.UserDetails;
using AutoMapper;
using AssignmentDemo.API.Models.Users;
using System.Collections;
using Microsoft.AspNetCore.Mvc;


namespace AssignmentDemoAPI.Test.ControllerTest
{
   
    public class UsersControllerTests
    {
        /// <summary>
        /// Test case 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void GetUsers200OKResponse()
        {   //Mocking Setup
            var mockUserRequest = new Mock<IUserRequestHandler>();
            mockUserRequest.Setup(req => req.GetUsers()).ReturnsAsync(GetUsersList());

            var mockIMapper = new Mock<IMapper>();
            mockIMapper.Setup(op => op.Map<List<UserDto>>(It.IsAny<List<User>>())).Returns(GetUsersDtoList());

            //Act
            var controller = new UsersController(mockUserRequest.Object, mockIMapper.Object);
            var users = await controller.GetUsers();

            //Assert
            Assert.IsType<OkObjectResult>(users.Result);

        }

        [Fact]
        public async void GetUsersReturnAllItems()
        {   //Mocking Setup
            var mockUserRequest = new Mock<IUserRequestHandler>();
            mockUserRequest.Setup(req => req.GetUsers()).ReturnsAsync(GetUsersList());

            var mockIMapper = new Mock<IMapper>();
            mockIMapper.Setup(op => op.Map<List<UserDto>>(It.IsAny<List<User>>())).Returns(GetUsersDtoList());

            //Act
            var controller = new UsersController(mockUserRequest.Object, mockIMapper.Object);
            var users = await controller.GetUsers();
            var okResult = users.Result as OkObjectResult;
            var items = Assert.IsType<List<UserDto>>(okResult.Value);

            //Assert
            Assert.Equal(2, items.Count);

        }
        [Fact]
        public async void GetUsersReturn_NotFoundResponse()
        {
            //Mocking Setup
            var mockUserRequest = new Mock<IUserRequestHandler>();
            mockUserRequest.Setup(req => req.GetUser(1)).ReturnsAsync(GetUser());

            var mockIMapper = new Mock<IMapper>();
            mockIMapper.Setup(op => op.Map<UserDto>(It.IsAny<User>())).Returns(GetUserDto());
            //Act
            var controller = new UsersController(mockUserRequest.Object, mockIMapper.Object);
            var user = await controller.GetUser(2);
            //Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundResult>(user.Result);
        }



        private List<User> GetUsersList()
        {
            List<User> userdetails = new List<User>();
            userdetails.Add(new User()
            {
                id = '1',
                name = "Rivan",
                username = "RivanRana13",
                email = "Rivan.abc@gmanil.com",
                phone = "8884135222",
                website = "http://google.com",
                address = new Address()
                {
                    street = "Gubalala",
                    suite = "Rohan",
                    city = "Bangalore",
                    zipcode = "560061",
                    geo = new Geo()
                    {
                        lat = "11'2",
                        lng = "12'3"
                    }
                },
                company = new Company()
                {
                    name = "Adobe",
                    bs = "AVDDD",
                    catchPhrase = "This is my world"
                }
            });         
            userdetails.Add(new User()
            {
                id = '2',
                name = "sfsdf",
                username = "sfsdffdf",
                email = "Rivan.abc@gmanil.com",
                phone = "8884135222",
                website = "http://google.com",
                address = new Address()
                {
                    street = "Gubalala",
                    suite = "Rohan",
                    city = "Bangalore",
                    zipcode = "560061",
                    geo = new Geo()
                    {
                        lat = "11'2",
                        lng = "12'3"
                    }
                },
                company = new Company()
                {
                    name = "Adobe",
                    bs = "AVDDD",
                    catchPhrase = "This is my world"
                }
            });


            return userdetails;
        }

        private List<UserDto> GetUsersDtoList()
        {
            List<UserDto> userdetails = new List<UserDto>();
            userdetails.Add(new UserDto()
            {
                id = '1',
                name = "Rivan",
                username = "RivanRana13",
                email = "Rivan.abc@gmanil.com",
                phone = "8884135222",
                website = "http://google.com",
                address = new AddressDto()
                {
                    street = "Gubalala",
                    suite = "Rohan",
                    city = "Bangalore",
                    zipcode = "560061",
                    geo = new GeoDto()
                    {
                        lat = "11'2",
                        lng = "12'3"
                    }
                },
                company = new CompanyDto()
                {
                    name = "Adobe",
                    bs = "AVDDD",
                    catchPhrase = "This is my world"
                }
            });
            userdetails.Add(new UserDto()
            {
                id = '2',
                name = "sfsdf",
                username = "sfsdffdf",
                email = "Rivan.abc@gmanil.com",
                phone = "8884135222",
                website = "http://google.com",
                address = new AddressDto()
                {
                    street = "Gubalala",
                    suite = "Rohan",
                    city = "Bangalore",
                    zipcode = "560061",
                    geo = new GeoDto()
                    {
                        lat = "11'2",
                        lng = "12'3"
                    }
                },
                company = new CompanyDto()
                {
                    name = "Adobe",
                    bs = "AVDDD",
                    catchPhrase = "This is my world"
                }
            });


            return userdetails;
        }

        private User GetUser()
        {
            User user = new User()
            {
                id = '1',
                name = "Rivan",
                username = "RivanRana13",
                email = "Rivan.abc@gmanil.com",
                phone = "8884135222",
                website = "http://google.com",
                address = new Address()
                {
                    street = "Gubalala",
                    suite = "Rohan",
                    city = "Bangalore",
                    zipcode = "560061",
                    geo = new Geo()
                    {
                        lat = "11'2",
                        lng = "12'3"
                    }
                },
                company = new Company()
                {
                    name = "Adobe",
                    bs = "AVDDD",
                    catchPhrase = "This is my world"
                }
            };

            return user;
        }

        private UserDto GetUserDto()
        {
            UserDto userDto = new UserDto()
            {
                id = '1',
                name = "Rivan",
                username = "RivanRana13",
                email = "Rivan.abc@gmanil.com",
                phone = "8884135222",
                website = "http://google.com",
                address = new AddressDto()
                {
                    street = "Gubalala",
                    suite = "Rohan",
                    city = "Bangalore",
                    zipcode = "560061",
                    geo = new GeoDto()
                    {
                        lat = "11'2",
                        lng = "12'3"
                    }
                },
                company = new CompanyDto()
                {
                    name = "Adobe",
                    bs = "AVDDD",
                    catchPhrase = "This is my world"
                }
            };
            return userDto;
        }
    }
}
