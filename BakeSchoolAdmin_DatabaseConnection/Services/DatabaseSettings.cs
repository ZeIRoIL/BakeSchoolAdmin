namespace BakeSchoolAdmin_DatabaseConnection.Services
{
    /// <summary>
    /// Defines the <see cref="DatabaseSettings" />.
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// Gets or sets the DatabaseConnection.
        /// </summary>
        public string DatabaseConnection { get; set; }

        /// <summary>
        /// Gets or sets the DatabaseName.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the CategoryCollectionName.
        /// </summary>
        public string CategoryCollectionName { get; set; }
    }
}
