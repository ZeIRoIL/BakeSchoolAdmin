namespace BakeSchoolAdmin_Models.Modals.Category
{
    using BakeSchoolAdmin_Commands.NotfiyPropertyChanged;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Defines the <see cref="CategoryDetails" />.
    /// </summary>
    public class CategoryDetails : NotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (value != null)
                {
                    this.name = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Image.
        /// </summary>
        public string Image
        {
            get
            {
                return this.image;
            }

            set
            {
                if (value != null)
                {
                    this.image = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (value != null)
                {
                    this.text = value;
                    this.OnPropertyChanged("Text");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Level.
        /// </summary>
        public int Level
        {
            get
            {
                return this.level;
            }

            set
            {
                if (value != 0)
                {
                    this.level = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [BsonElement("name")]
        private string name { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        [BsonElement("img")]
        private string image { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        [BsonElement("text")]
        private string text { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        [BsonElement("difficultyLevel")]
        private int level { get; set; }
    }
}
