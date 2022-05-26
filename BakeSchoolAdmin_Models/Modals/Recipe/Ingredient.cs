using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_Models.Modals.Recipe
{
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
}
