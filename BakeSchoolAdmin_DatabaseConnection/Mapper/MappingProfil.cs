using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_DatabaseConnection.Mapper
{
    public class MappingProfil
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CategoryMapperProfil());
            });
            return configuration;
        }
    }
}
