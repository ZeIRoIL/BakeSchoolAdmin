using BakeSchoolAdmin_DataConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_Models.Modals
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string DatabaseConnection { get; set; }
        public string DatabaseName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string RecipesCollectionName { get; set; }
    }
}
