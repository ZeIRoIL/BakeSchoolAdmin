namespace BakeSchoolAdmin_DatabaseConnection.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Defines the <see cref="CategoryData" />.
    /// </summary>
    public class CategoryData
    {
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
        [BsonElement("categoryId")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Details.
        /// </summary>
        [BsonElement("details")]
        public Detail Details { get; set; }
    }

    /// <summary>
    /// The class is dependency at the parent class! Contain the details of the Category.
    /// </summary>
    public class Detail
    {
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Image.
        /// </summary>
        [BsonElement("img")]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        [BsonElement("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the Level.
        /// </summary>
        [BsonElement("difficultyLevel")]
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the DifficultyLevelRange.
        /// </summary>
        [BsonElement("difficultyLevelRange")]
        public int[] DifficultyLevelRange { get; set; }
    }
}
