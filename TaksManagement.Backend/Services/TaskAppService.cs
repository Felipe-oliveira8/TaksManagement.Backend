using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaksManagement.Backend.Entities.MongoEntities;
using TaksManagement.Backend.Models;

namespace TaksManagement.Backend.Services
{
    public class TaskAppService
    {
        public IMapper Mapper { get; set; }

        private readonly IMongoCollection<TaskEntity> tasks;
        public UserAppService UserAppService { get; set; }

        public TaskAppService(IConfiguration configuration, IMapper mapper, UserAppService userAppService)
        {
            var client = new MongoClient(configuration.GetConnectionString("TaskManagement"));
            var database = client.GetDatabase("TaskManagement");

            tasks = database.GetCollection<TaskEntity>("tasks");
            Mapper = mapper;
            UserAppService = userAppService;
        }

        public void Create(TaskModel task, string username)
        {
            var taskEntity = Mapper.Map<TaskModel, TaskEntity>(task);
            var user = UserAppService.GetUserByUsername(username);
            taskEntity.GeneratorId = user.Id;
            taskEntity.GeneratorName = user.Name;
            taskEntity.OpeningDate = DateTime.Now;
            taskEntity.SequenceNumber = GetSequenceTask() + 1;

            try
            {
                tasks.InsertOne(taskEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TaskModel> GetTasks()
        {
            var listTasks = tasks.Find(task => true).ToList();
            var ListTaskModel = new List<TaskModel>();

            foreach (var item in listTasks)
            {
                var task = Mapper.Map<TaskEntity, TaskModel>(item);
                ListTaskModel.Add(task);
            }

            return ListTaskModel;
        }

        public TaskModel GetTask(string id)
        {
            return Mapper.Map<TaskEntity, TaskModel>(tasks.Find(task => task.Id == MongoDB.Bson.ObjectId.Parse(id)).FirstOrDefault());
        }

        public TaskEntity GetTaskActivity(string id)
        {
            return tasks.Find(task => task.Id == MongoDB.Bson.ObjectId.Parse(id)).FirstOrDefault();
        }

        public void Update(TaskModel taskUpdate, string username)
        {
            var task = Mapper.Map<TaskModel, TaskEntity>(taskUpdate);
            var user = UserAppService.GetUserByUsername(username);
            try
            {
                if ((task.ResponsibleId != null) && (task.ResponsibleId != user.Id && task.GeneratorId != user.Id))
                {
                    throw new Exception("You do not have permission to change this task.");
                }
                task.Status = taskUpdate.Status;
                tasks.ReplaceOne(x => x.Id == task.Id, task);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateActivity(TaskEntity taskUpdate)
        {
            try
            {
                tasks.ReplaceOne(x => x.Id == taskUpdate.Id, taskUpdate);
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
                tasks.DeleteOne(x => x.Id == MongoDB.Bson.ObjectId.Parse(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetSequenceTask()
        {
            return (int)tasks.CountDocuments(task => true);
        }

    }
}
