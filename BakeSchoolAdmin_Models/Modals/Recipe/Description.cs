using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_Models.Modals.Recipe
{
    /// <summary>
    /// the description for the recipes step by step.
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
        /// Gets or sets the Image.
        /// </summary>
        [BsonElement("image")]
        public string Image { get; set; }
    }
}
