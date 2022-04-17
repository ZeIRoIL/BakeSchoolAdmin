using BakeSchoolAdmin_Commands.NotfiyPropertyChanged;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_Models.Modals.Category
{
    public class CategoryDetails : NotifyPropertyChanged
    {
        #region --------------------------------------------------------------------------Fields---------------------------------------------------
        [BsonElement("name")]
        private string name { get; set; }
        [BsonElement("img")]
        private string image { get; set; }
        [BsonElement("text")]
        private string text { get; set; }
        [BsonElement("difficultyLevel")]
        private int level { get; set; }
        [BsonElement("difficultyLevelRange")]
        public int[] difficultyLevelRange { get; set; }
        #endregion

        #region -------------------------------------------------------------------------- Construcor ---------------------------------------------------

        #endregion

        #region --------------------------------------------------------------------------Properties---------------------------------------------------
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
               if(value != null)
                {
                    this.name = value;
                }
            }
        }

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

        public int[] DifficultyLevelRange
        {
            get
            {
                return this.difficultyLevelRange;
            }
            set
            {
                if (value != null)
                {
                    this.difficultyLevelRange = value;
                }
            }
        }

        #endregion
    }
}
