using BakeSchoolAdmin_DatabaseConnection.Models;
using BakeSchoolAdmin_Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_DatabaseConnection.Services
{
    public class CategoryService
    {
        public  IMongoCollection<CategoryData> categories { get; set; }
        public CategoryService(DatabaseSettings settings)
        {
            var client = new MongoClient(settings.DatabaseConnection);
            var database = client.GetDatabase(settings.DatabaseName);
            categories = database.GetCollection<CategoryData>(settings.CategoryCollectionName);

        }
        public List<CategoryData> GetCategoryData()
        {
            List<CategoryData> list = categories.Find<CategoryData>
               (p => true).ToList<CategoryData>();

            return list;
        }
    }
}
