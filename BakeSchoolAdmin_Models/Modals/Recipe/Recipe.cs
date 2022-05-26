namespace BakeSchoolAdmin_Models
{
    using BakeSchoolAdmin_Models.Modals.Recipe;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Recipe" />.
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// </summary>
        public Recipe()
        {
        }

        /// <summary>
        /// Gets or sets the ObjectId.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ObjectId { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Number.
        /// </summary>
        [BsonElement("number")]
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the Ingredients.
        /// </summary>
        [BsonElement("ingredients")]
        public List<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// Gets or sets the Descriptions.
        /// </summary>
        [BsonElement("description")]
        public List<Description> Descriptions { get; set; }
    }
}
