using BakeSchoolAdmin_Models;
using BakeSchoolAdmin_Models.Modals;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_DataConnection
{
    public class CategoryService
    {
        private readonly IMongoCollection<Category> _CategoryCollection;
        /// <summary>
        /// added the options from DatabaseSettings
        /// </summary>
        /// <param name="categoryOptions"></param>
        public CategoryService(
            IOptions<DatabaseSettings> categoryOptions)
        {
            var mongoClient = new MongoClient(
                categoryOptions.Value.DatabaseConnection);
            var mongoDatabase = mongoClient.GetDatabase(
                categoryOptions.Value.DatabaseName);
            _CategoryCollection = mongoDatabase.GetCollection<Category>(
                categoryOptions.Value.CategoryCollectionName);
        }

        public async Task<List<Category>> GetAsync() =>
            await _CategoryCollection.Find(_ => true).ToListAsync();


    }
}
