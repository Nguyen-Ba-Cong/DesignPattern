
using AutoMapper;
using DesignPattern.API.Controllers;
using DesignPattern.Service.ApiService;
using DesignPattern.Service.Common;
using DesignPattern.Service.IApiService;
using DesignPattern.Service.IApiServices;
using DesignPattern.Service.IRepositories;
using DesignPattern.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using AutoMapper.QueryableExtensions;
using System.Linq;
using DesignPattern.Database.Entity;

namespace DesignPattern.Test
{

    public class TestUserService
    {
        private Mock<IUserRepository> _mockUserRepository;
        private IUserService _userService;
        IMapper _mapper;
        private List<UserModel> _listUser;
        public TestUserService()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            _mapper = mockMapper.CreateMapper();
            _userService = new UserService(_mockUserRepository.Object, _mapper);
            _listUser = new List<UserModel>()
            {
                new UserModel()
                {
                    Id =1,
                    UserName = "Nguyen Ba Cong 1",
                    Email = "nguyenbacong1@gmail.com"
                },
                 new UserModel()
                {
                    Id =2,
                     UserName = "Nguyen Ba Cong 2",
                    Email = "nguyenbacong2@gmail.com"
                },
                 new UserModel()
                {
                    Id =3,
                     UserName = "Nguyen Ba Cong 3",
                    Email = "nguyenbacong3@gmail.com"
                },
            };
        }
        [Fact]
        public void Add_User_Service_Test()
        {
            var userModel = new UserModel();
            userModel.Id = 4;
            userModel.UserName = "Nguyen Van A";
            userModel.Email = "nguyenvana@gmail.com";
            var user = _mapper.Map<User>(userModel);
            _mockUserRepository.Setup(m => m.AddUser(It.IsAny<User>())).Returns(user);
            var result = _userService.AddUser(userModel);
            Assert.Equal(result.Id.ToString(), 4.ToString());
        }
        [Fact]
        public void Get_New_By_UserId_Test()
        {
            List<NewModel> newModels = new List<NewModel>()
            {
                new NewModel()
                {
                    Id =1,
                    Content = "Test 1 Content",
                    Title = "Test 1 Title",
                    Image ="Test 1 Image",
                    User = new UserModel()
                    {
                        Id = 1
                    }
                },
                 new NewModel()
                {
                    Id =2,
                    Content = "Test 2 Content",
                    Title = "Test 2 Title",
                    Image ="Test 2 Image",
                    User = new UserModel()
                    {
                        Id = 1
                    }
                },
                  new NewModel()
                {
                    Id =3,
                    Content = "Test 3 Content",
                    Title = "Test 3 Title",
                    Image ="Test 3 Image",
                    User = new UserModel()
                    {
                        Id = 1
                    }
                }
            };
            var listModel = _mapper.Map<List<New>>(newModels);
            _mockUserRepository.Setup(m => m.GetNewByUserId(It.IsAny<int>())).Returns(listModel);
            var result = _userService.GetNewByUserId(1);
            Assert.Equal(result.Count.ToString(), 3.ToString());
        }
        //[Fact]
        //public void Get_User_Service_Test()
        //{
        //    var user = _mapper.Map<User>(_listUser[1]);
        //    _mockUserRepository.Setup(m => m.FindByCondition(x => x.Id == It.IsAny<int>())).Returns(user.ToQueryable());
        //    var result = _userService.GetUser(2);
        //    Assert.Equal(result.Id, user.Id);
        //}
        [Fact]
        public void Update_User_Service_Test()
        {
            var user = _mapper.Map<User>(_listUser[1]);
            _mockUserRepository.Setup(m => m.Update(It.IsAny<User>())).Returns(user);
            var result = _userService.UpdateUser(_listUser[1]);
            var userExpected = _listUser[1];
            Assert.Equal(result.Id, userExpected.Id);
        }
        [Fact]
        public void Delete_User_Service_Test()
        {
            var user = _mapper.Map<User>(_listUser[1]);
            _mockUserRepository.Setup(m => m.Delete(It.IsAny<User>())).Returns(user);
            var result = _userService.DeleteUser(2, _listUser[1]);
            var userExpected = _listUser[1];
            Assert.Equal(result.Id, userExpected.Id);
        }

    }
    public static class ObjectExtensionMethods
    {
        public static IQueryable<T> ToQueryable<T>(this T instance)
        {
            return new[] { instance }.AsQueryable();
        }
    }
}
