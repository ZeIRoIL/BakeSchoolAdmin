using AutoMapper;
using BakeSchoolAdmin_DatabaseConnection.Models;
using BakeSchoolAdmin_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_DatabaseConnection.Mapper
{
    public class CategoryMapperProfil : Profile
    {
        public CategoryMapperProfil()
        {
            CreateMap<Category, CategoryData>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.ObjectId, act => act.MapFrom(src => src.ObjectId))
                .ForMember(des => des.String, act => act.MapFrom(src => src.String))
                .ForMember(des => des.Details, act => act.MapFrom(src => src.Id));

            CreateMap<CategoryData, Category>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.ObjectId, act => act.MapFrom(src => src.ObjectId))
                .ForMember(des => des.String, act => act.MapFrom(src => src.String))
                .ForMember(des => des.Details, act => act.MapFrom(src => src.Id));
        }
    }
}
