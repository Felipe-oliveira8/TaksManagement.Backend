using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using TaksManagement.Backend.Entities.MongoEntities;
using TaksManagement.Backend.Models;

namespace TaksManagement.Backend.Services
{
    public class UserAppService
    {
        public IMongoCollection<UserEntity> users;
        public IMapper Mapper { get; set; }

        public UserAppService(IConfiguration configuration, IMapper mapper)
        {
            var client = new MongoClient(configuration.GetConnectionString("TaskManagement"));
            var database = client.GetDatabase("TaskManagement");

            users = database.GetCollection<UserEntity>("Users");
            Mapper = mapper;
        }

        public List<UserModel> GetUsers() 
        {
            var listUsers = users.Find(user => true).ToList();
            var ListUserModel = new List<UserModel>();

            foreach(var item in listUsers)
            {
                var user = Mapper.Map<UserEntity, UserModel>(item);
                ListUserModel.Add(user);
            }

            return ListUserModel;
        }

        public UserEntity GetUserByUsername(string username)
        {
            return users.Find(user => user.User == username).FirstOrDefault();
        }

        public UserModel GetUser(string id) 
        {
            var usuario = users.Find(user => user.Id == MongoDB.Bson.ObjectId.Parse(id)).FirstOrDefault();
            return Mapper.Map<UserEntity, UserModel>(usuario);
        }

        public UserModel Create(UserModel user)
        {
            var userExist = GetUserByUsername(user.User);
            if(userExist is null)
            {
                var userEntity = Mapper.Map<UserModel, UserEntity>(user);
                user.Id = userEntity.Id.ToString();
                try
                {
                    users.InsertOne(userEntity);
                    return user;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("Usuario ja existente no sistema!");
            }
        }

        public void Update(UserModel userUpdate)
        {
            var user = Mapper.Map<UserModel, UserEntity>(userUpdate);

            try
            {
                users.ReplaceOne(x => x.Id == MongoDB.Bson.ObjectId.Parse(userUpdate.Id), user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(string id)
        {
            try
            {
                users.DeleteOne(x => x.Id == MongoDB.Bson.ObjectId.Parse(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
