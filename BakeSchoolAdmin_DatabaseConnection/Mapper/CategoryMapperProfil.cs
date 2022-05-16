namespace BakeSchoolAdmin_DatabaseConnection.Mapper
{
    using AutoMapper;
    using BakeSchoolAdmin_DatabaseConnection.Models;
    using BakeSchoolAdmin_Models;

    /// <summary>
    /// Defines the <see cref="CategoryMapperProfil" />.
    /// </summary>
    public class CategoryMapperProfil : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryMapperProfil"/> class.
        /// </summary>
        public CategoryMapperProfil()
        {
            CreateMap<Category, CategoryData>();


            CreateMap<CategoryData, Category>();
        }
    }
}
