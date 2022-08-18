using AutoMapper;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaksManagement.Backend.Entities.MongoEntities;
using TaksManagement.Backend.Models;

namespace TaksManagement.Backend.Maps
{
    public class TaskMapping : Profile
    {
        public TaskMapping()
        {
            CreateMap<TaskModel, TaskEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => ObjectId.GenerateNewId()))
                .ForMember(dest => dest.SequenceNumber, opt => opt.MapFrom(src => src.SequenceNumber))
                .ForMember(dest => dest.ResponsibleId, opt => opt.MapFrom(src => ObjectId.Parse(src.ResponsibleId)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));

            CreateMap<TaskEntity, TaskModel>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.SequenceNumber, opt => opt.MapFrom(src => src.SequenceNumber))
                .ForMember(dest => dest.ResponsibleId, opt => opt.MapFrom(src => src.ResponsibleId))
                .ForMember(dest => dest.ResponsibleName, opt => opt.MapFrom(src => src.ResponsibleName))
                .ForMember(dest => dest.GeneratorId, opt => opt.MapFrom(src => src.GeneratorId))
                .ForMember(dest => dest.GeneratorName, opt => opt.MapFrom(src => src.GeneratorName))
                .ForMember(dest => dest.OpeningDate, opt => opt.MapFrom(src => src.OpeningDate))
                .ForMember(dest => dest.ActivitiesIds, opt => opt.MapFrom(src => src.ActivitiesIds))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
        }
    }
}
