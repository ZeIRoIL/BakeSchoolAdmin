using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_DatabaseConnection.Models
{
        public class RecipeData
        {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ObjectId { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("number")]
        public int Number { get; set; }
        [BsonElement("ingredients")]
        public Ingredient[] Ingredients { get; set; }
        [BsonElement("description")]
        public Description[] Descriptions { get; set; }
    }
    /// <summary>
    /// The class is dependency at the parent class! Contain the details of the Recipes
    /// </summary>
    public class Ingredient
    {
        [BsonElement("data")]
        public string Data { get; set; }
        [BsonElement("amount")]
        public double Amount { get; set; }
        [BsonElement("unit")]
        public string Unit { get; set; }
    }
    /// <summary>
    /// the description for the repices step by step
    /// </summary>
    public class Description
    {
        [BsonElement("step")]
        public int Step { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("hint")]
        public List<Hint> Hints { get; set; }
        [BsonElement("image")]
        public string Image { get; set; }
    }
    /// <summary>
    /// If the Step has a hint then it will show a hints
    /// </summary>
    public class Hint
    {
        [BsonElement("step")]
        public int Step { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
    }
}
