namespace BakeSchoolAdmin_DatabaseConnection.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using AutoMapper;
    using BakeSchoolAdmin_DatabaseConnection.Interfaces;
    using BakeSchoolAdmin_DatabaseConnection.Mapper;
    using BakeSchoolAdmin_DatabaseConnection.Models;
    using BakeSchoolAdmin_Models;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Newtonsoft.Json;
    using BakeSchoolAdmin_Models;

    /// <summary>
    /// Defines the <see cref="RecipeService" />.
    /// </summary>
    public class RecipeService : IDatabaseSettings<Recipe>
    {
        /// <summary>
        /// Defines the databaseConnection.
        /// </summary>
        private readonly string databaseConnection = "mongodb://localhost:27017";

        /// <summary>
        /// Defines the databaseName.
        /// </summary>
        private readonly string databaseName = "LearnBakeDb";

        /// <summary>
        /// Defines the recipeCollectionName.
        /// </summary>
        private readonly string recipeCollectionName = "RecipeCollection";

        /// <summary>
        /// Defines the Mongo Client..
        /// </summary>
        private MongoClient Mongoclient;

        /// <summary>
        /// Defines the mapper.
        /// </summary>
        private IMapper mapper;

        /// <summary>
        /// The initialize.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool init()
        {
            try
            {
                this.Mongoclient = new MongoClient(this.databaseConnection);
                var database = this.Mongoclient.GetDatabase(this.databaseName);
                this.recipesdata = database.GetCollection<RecipeData>(this.recipeCollectionName);
                try
                {
                    this.mapper = MappingProfil.InitializeAutoMapper().CreateMapper();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Mapper initalize exception" + ex.Message);
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets or sets the recipes data.
        /// </summary>
        public IMongoCollection<RecipeData> recipesdata { get; set; }

        /// <summary>
        /// The ReadData.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Recipe"/>.</returns>
        public Recipe ReadData(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ReadData.
        /// </summary>
        /// <returns>The <see cref="IList{Recipe}"/>.</returns>
        public IList<Recipe> ReadData()
        {
            //// Get the data from mongodb into the list
            IList<RecipeData> list = this.recipesdata.Find<RecipeData>
           (p => true).ToList<RecipeData>();

            IList<Recipe> recipes = this.mapper.Map<IList<RecipeData>, IList<Recipe>>(list);

            return recipes;
        }

        /// <summary>
        /// The WriteData.
        /// </summary>
        /// <param name="data">The data<see cref="Recipe"/>.</param>
        public void WriteData(Recipe data)
        {
            string name = data.Name;
            int number = data.Number;
            List<Ingredient> ingredients = new List<Ingredient>();
            List<Description> descriptions = new List<Description>();
            ingredients = data.Ingredients;
            descriptions = data.Descriptions;

            // Recipe to BsonObject
            BsonArray bsonArray = new BsonArray();
            foreach (var item in ingredients)
            {
                BsonDocument docu = new BsonDocument { { "data",item.Data }, { "amount", item.Amount }, { "unit", item.Unit } };

                bsonArray.Add(docu);
            }
            // Description to BsonObjet
            BsonArray bsonArrayDes = new BsonArray();
            foreach (var item in descriptions)
            {
                BsonDocument docu = new BsonDocument { { "step", item.Step }, { "text", item.Text }, { "image", item.Image } };

                bsonArrayDes.Add(docu);
            }

            try
            {
                var document = new BsonDocument
            {
                    { "name",  name },
                    { "number",  number },
                    { "ingredients",bsonArray },
                    {"description", bsonArrayDes }
              };
                var database = this.Mongoclient.GetDatabase(this.databaseName);
                var collection = database.GetCollection<BsonDocument>(this.recipeCollectionName);
                collection.InsertOne(document);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                
            }
        }

        /// <summary>
        /// The recipes observableCollection.
        /// </summary>
        /// <param name="recipeList">The recipe list<see cref="IList{Recipe}"/>.</param>
        /// <returns>The <see cref="ObservableCollection{Recipe}"/>.</returns>
        public ObservableCollection<Recipe> GetRecipesObs(IList<Recipe> recipeList)
        {
            // create an emtpy observable collection object
            ObservableCollection<Recipe> recipes = new ObservableCollection<Recipe>();

            // loop through all the records and add to observable collection object
            foreach (var item in recipeList)
                  recipes.Add(item);

            return recipes;
        }


        public void WriteData(IList<Recipe> data)
        {
            throw new NotImplementedException();
        }
    }
}
