using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaksManagement.Backend.Entities;
using TaksManagement.Backend.Entities.MongoEntities;
using TaksManagement.Backend.Models;

namespace TaksManagement.Backend.Services
{
    public class ActivityAppService
    {
        public IMapper Mapper { get; set; }
        private readonly IMongoCollection<ActivityEntity> activities;
        public UserAppService UserAppService { get; set; }
        public TaskAppService TaskAppService { get; set; }

        public ActivityAppService(IConfiguration configuration, IMapper mapper, UserAppService userAppService, TaskAppService taskAppService)
        {
            var client = new MongoClient(configuration.GetConnectionString("TaskManagement"));
            var database = client.GetDatabase("TaskManagement");

            activities = database.GetCollection<ActivityEntity>("activities");
            Mapper = mapper;
            UserAppService = userAppService;
            TaskAppService = taskAppService;
        }

        public List<ActivityModel> GetActivities(string taskId)
        {
            var listActivities = activities.Find(activity => activity.TaskId == MongoDB.Bson.ObjectId.Parse(taskId)).ToList();
            var ListActivityModel = new List<ActivityModel>();

            foreach (var item in listActivities)
            {
                var activity = Mapper.Map<ActivityEntity, ActivityModel>(item);
                ListActivityModel.Add(activity);
            }

            return ListActivityModel;
        }

        public ActivityModel Create(ActivityModel activity, TaskEntity taskEntity, string username)
        {
            var activityEntity = Mapper.Map<ActivityModel, ActivityEntity>(activity);
            var user = UserAppService.GetUserByUsername(username);
            if (user.Id == taskEntity.GeneratorId || user.Id == taskEntity.ResponsibleId)
            {
                activityEntity.GeneratorId = user.Id;
                activityEntity.GeneratorName = user.Name;
                activityEntity.Date = DateTime.Now;
                activityEntity.Sequence = (int)activities.CountDocuments(activity => activity.TaskId == activityEntity.TaskId) + 1;
                activities.InsertOne(activityEntity);

                var activitiesIds = taskEntity.ActivitiesIds?.ToList() ?? new List<ObjectId>();
                activitiesIds.Add(activityEntity.Id);
                taskEntity.ActivitiesIds = activitiesIds;
                taskEntity.Status = activity.Status;
                TaskAppService.UpdateActivity(taskEntity);

                return activity;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
