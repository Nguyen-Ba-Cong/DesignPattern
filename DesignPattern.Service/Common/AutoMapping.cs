using AutoMapper;
using DesignPattern.Database.Entity;
using DesignPattern.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.Common
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();
            CreateMap<New, NewModel>();
            CreateMap<NewModel, New>();
            CreateMap<CategoryModel, Category>();
            CreateMap<Category, CategoryModel>();
        }

    }
}
