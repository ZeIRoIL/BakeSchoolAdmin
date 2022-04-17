using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_Models
{   
    public class Category 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ObjectId { get; set; }
        [BsonElement("datasetId")]
        public string String { get; set; }
        [BsonElement("categoryId")]
        public int Id { get; set; }
        [BsonElement("details")]
        public Detail Details { get; set; }
    }
    /// <summary>
    /// The class is dependency at the parent class! Contain the details of the Category
    /// </summary>
    public class Detail
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("img")]
        public string Image { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
        [BsonElement("difficultyLevel")]
        public int Level { get; set; }
        [BsonElement("difficultyLevelRange")]
        public int[] DifficultyLevelRange { get; set; }
    }
}
