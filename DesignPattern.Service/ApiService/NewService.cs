using AutoMapper;
using DesignPattern.Database.Entity;
using DesignPattern.Service.IApiServices;
using DesignPattern.Service.IRepositories;
using DesignPattern.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.ApiService
{
    public class NewService : INewService
    {
        private readonly INewRepository _newRepository;
        private readonly IMapper _mapper;
        public NewService(INewRepository newRepository, IMapper mapper)
        {
            _newRepository = newRepository;
            _mapper = mapper;
        }
        public NewModel AddNew(string email, NewModel newModel)
        {
            var newToAdd = _mapper.Map<New>(newModel);
            var neww = _newRepository.AddNew(email, newToAdd);
            if (neww != null)
            {
                return newModel;
            }
            return null;
        }

        public NewModel DeleteNew(string email, NewModel newModel)
        {
            try
            {
                var newToDel = _mapper.Map<New>(newModel);
                var newResult = _newRepository.DeleteNew(email, newToDel);
                if (newResult != null)
                {
                    return _mapper.Map<NewModel>(newResult);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));

            }
            return null;
        }

        public NewModel GetNew(int id)
        {
            var newGet = _newRepository.FindByCondition(e => e.Id == id).FirstOrDefault();
            if (newGet != null)
            {
                var newResult = _mapper.Map<NewModel>(newGet);
                return newResult;
            }
            return null;
        }

        public List<NewModel> GetNews(int offset, int limit)
        {
            var news = _newRepository.All(offset, limit);
            if (news != null)
            {
                var newsResult = _mapper.Map<List<NewModel>>(news);
                return newsResult;
            }
            return null;
        }

        public NewModel UpdateNew(string email, NewModel newModel)
        {
            try
            {
                var newToUpdate = _mapper.Map<New>(newModel);
                var newResult = _newRepository.UpdateNew(email, newToUpdate);
                if (newResult != null)
                {
                    return _mapper.Map<NewModel>(newResult);
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
                return null;
            }
        }
    }
}
