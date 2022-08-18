using AutoMapper;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaksManagement.Backend.Entities;
using TaksManagement.Backend.Models;

namespace TaksManagement.Backend.Maps
{
    public class ActivityMapping : Profile
    {
        public ActivityMapping()
        {
            CreateMap<ActivityModel, ActivityEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => ObjectId.GenerateNewId()))
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => ObjectId.Parse(src.TaskId)));

            CreateMap<ActivityEntity, ActivityModel>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId.ToString()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.Sequence, opt => opt.MapFrom(src => src.Sequence))
                .ForMember(dest => dest.GeneratorName, opt => opt.MapFrom(src => src.GeneratorName));

        }
    }
}
