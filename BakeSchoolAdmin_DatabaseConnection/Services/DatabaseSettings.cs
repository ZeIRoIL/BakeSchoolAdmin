namespace BakeSchoolAdmin_DatabaseConnection.Services
{
    /// <summary>
    /// Defines the <see cref="DatabaseSettings" />.
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// Gets or sets the databaseConnection.
        /// </summary>
        public string DatabaseConnection { get; set; }

        /// <summary>
        /// Gets or sets the databaseName.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the categoryCollectionName.
        /// </summary>
        public string CategoryCollectionName { get; set; }
    }
}
