namespace BakeSchoolAdmin_Models
{
    using BakeSchoolAdmin_Commands.NotfiyPropertyChanged;
    using BakeSchoolAdmin_Models.Modals.Category;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Defines the <see cref="Category" />.
    /// </summary>
    public class Category : NotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        public Category()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        /// <param name="id">the id from the category</param>
        /// <param name="details">the category details</param>
        public Category(int id, CategoryDetails details)
        {
            this.Id = id;
            this.Details = details;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [BsonElement("categoryId")]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        [BsonElement("details")]
        public CategoryDetails details { get; set; }

        /// <summary>
        /// Gets or sets the ObjectId.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ObjectId { get; set; }

        /// <summary>
        /// Gets or sets the String.
        /// </summary>
        [BsonElement("datasetId")]
        public string String { get; set; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                if (this.id != value)
                {
                    this.id = value;

                    //// Notify the GUI when Firstname and Fullname changed
                    this.OnPropertyChanged("Text");

                }
            }
        }

        /// <summary>
        /// Gets or sets the Details.
        /// </summary>
        public CategoryDetails Details
        {
            get
            {
                return this.details;
            }

            set
            {
                if (this.details != value)
                {
                    this.details = value;

                    //// Notify the GUI when Firstname and Fullname changed
                    this.OnPropertyChanged("Details");
                }
            }
        }
    }
}
