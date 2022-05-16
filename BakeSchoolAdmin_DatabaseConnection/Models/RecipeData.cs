namespace BakeSchoolAdmin_DatabaseConnection.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="RecipeData" />.
    /// </summary>
    public class RecipeData
    {
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
        public Ingredient[] Ingredients { get; set; }

        /// <summary>
        /// Gets or sets the Descriptions.
        /// </summary>
        [BsonElement("description")]
        public Description[] Descriptions { get; set; }
    }

    /// <summary>
    /// The class is dependency at the parent class! Contain the details of the Recipes.
    /// </summary>
    public class Ingredient
    {
        /// <summary>
        /// Gets or sets the Data.
        /// </summary>
        [BsonElement("data")]
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        [BsonElement("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Gets or sets the Unit.
        /// </summary>
        [BsonElement("unit")]
        public string Unit { get; set; }
    }

    /// <summary>
    /// the description for the repices step by step.
    /// </summary>
    public class Description
    {
        /// <summary>
        /// Gets or sets the Step.
        /// </summary>
        [BsonElement("step")]
        public int Step { get; set; }

        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        [BsonElement("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the Hints.
        /// </summary>
        [BsonElement("hint")]
        public List<Hint> Hints { get; set; }

        /// <summary>
        /// Gets or sets the Image.
        /// </summary>
        [BsonElement("image")]
        public string Image { get; set; }
    }

    /// <summary>
    /// If the Step has a hint then it will show a hints.
    /// </summary>
    public class Hint
    {
        /// <summary>
        /// Gets or sets the Step.
        /// </summary>
        [BsonElement("step")]
        public int Step { get; set; }

        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        [BsonElement("text")]
        public string Text { get; set; }
    }
}
