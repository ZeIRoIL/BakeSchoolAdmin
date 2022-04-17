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
    public class RecipeServices
    {
        private readonly IMongoCollection<Recipe> _RecipeCollection;
        /// <summary>
        /// added the options from DatabaseSettings
        /// </summary>
        /// <param name="categoryOptions"></param>
        public RecipeServices(
            IOptions<DatabaseSettings> options)
        {
            var mongoClient = new MongoClient(
                options.Value.DatabaseConnection);
            var mongoDatabase = mongoClient.GetDatabase(
                options.Value.DatabaseName);
            _RecipeCollection = mongoDatabase.GetCollection<Recipe>(
                options.Value.RecipesCollectionName);
        }

        public async Task<List<Recipe>> GetAsync() =>
        await _RecipeCollection.Find(_ => true).ToListAsync();

        public async Task<Recipe> GetAsync(int id) =>
            await _RecipeCollection.Find(x => x.Number == id).FirstOrDefaultAsync();
    }
}
