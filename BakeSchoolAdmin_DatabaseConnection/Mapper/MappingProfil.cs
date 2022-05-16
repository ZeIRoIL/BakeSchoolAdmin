namespace BakeSchoolAdmin_DatabaseConnection.Mapper
{
    using AutoMapper;

    /// <summary>
    /// Defines the <see cref="MappingProfil" />.
    /// </summary>
    public static class MappingProfil
    {
        /// <summary>
        /// The InitializeAutoMapper.
        /// </summary>
        /// <returns>The <see cref="MapperConfiguration"/>.</returns>
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
