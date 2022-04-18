using BakeSchoolAdmin_Commands.NotfiyPropertyChanged;
using BakeSchoolAdmin_Models.Modals.Category;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_Models
{

    public class Category : NotifyPropertyChanged
    {
        #region --------------------------------------------------------------------------Constructor---------------------------------------------------
        public Category()
        {

        }
            
        public Category(int id, CategoryDetails details)
        {
            this.Id = id;
            this.Details = details;
        }
        #endregion

        #region --------------------------------------------------------------------------Fields---------------------------------------------------
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ObjectId { get; set; }
        [BsonElement("datasetId")]
        public string String { get; set; }
        [BsonElement("categoryId")]
        private int id { get; set; }
        [BsonElement("details")]
        private CategoryDetails details { get; set; }
        #endregion
        #region --------------------------------------------------------------------------Properties---------------------------------------------------
        /// <summary>
        /// Gets or sets the Id
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
        /// Gets or sets the Detail
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
        #endregion

    }
}
