﻿using AutoMapper;
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
            CreateMap<Category, CategoryData>();
          

            CreateMap<CategoryData, Category>();

        }
    }
}
