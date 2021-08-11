using AutoMapper;
using DesignPattern.Database.Entity;
using DesignPattern.Service.IApiService;
using DesignPattern.Service.IApiServices;
using DesignPattern.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.ApiService
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public UserModel AddUser(UserModel user)
        {
            var userToAdd = _mapper.Map<User>(user);
            var userResult = _userRepository.AddUser(userToAdd);
            return _mapper.Map<UserModel>(userResult);
        }

        public UserModel UpdateUser(UserModel user)
        {

            var userToUpdate = _mapper.Map<User>(user);
            _userRepository.Update(userToUpdate);
            return user;
        }

        public UserModel DeleteUser(int id, UserModel user)
        {
            if (id != user.Id)
            {
                return null;
            }
            else
            {
                var useDel = _mapper.Map<User>(user);
                _userRepository.Delete(useDel);
                return user;
            }
        }

        public List<NewModel> GetNewByUserId(int id)
        {
            var news = _userRepository.GetNewByUserId(id);
            var newsShow = _mapper.Map<List<NewModel>>(news);
            return newsShow;
        }

        public UserModel GetUser(int id)
        {
            var user = _userRepository.FindByCondition(x => x.Id == id).FirstOrDefault();
            var userResult = _mapper.Map<UserModel>(user);
            return userResult;
        }

        public List<UserModel> GetUsers(int offset, int limit)
        {
            var users = _userRepository.All(offset, limit);
            var usersResult = _mapper.Map<List<UserModel>>(users);
            return usersResult;
        }

    }
}
