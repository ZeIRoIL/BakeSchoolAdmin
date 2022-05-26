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
    using BakeSchoolAdmin_Models.Modals.Recipe;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Newtonsoft.Json;

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
        /// Defines the mongo Client..
        /// </summary>
        private MongoClient mongoclient;

        /// <summary>
        /// Defines the mapper.
        /// </summary>
        private IMapper mapper;

        /// <summary>
        /// Gets or sets the recipes data.
        /// </summary>
        public IMongoCollection<RecipeData> recipesdata { get; set; }

        /// <summary>
        /// The initialize.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool init()
        {
            try
            {
                this.mongoclient = new MongoClient(this.databaseConnection);
                var database = this.mongoclient.GetDatabase(this.databaseName);
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
        /// Check whether database is connected.
        /// </summary>
        /// <returns>value of connection state</returns>
        public bool IsConnection()
        {
            this.mongoclient = new MongoClient(this.databaseConnection);
            var database = this.mongoclient.GetDatabase(this.databaseName);
            bool isMongoLive = database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);

            if (isMongoLive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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
        /// The WriteDataRecipe.
        /// </summary>
        /// <param name="data">The data<see cref="Recipe"/>.</param>
        public void WriteDataRecipe(Recipe data)
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
                var database = this.mongoclient.GetDatabase(this.databaseName);
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
            {
                recipes.Add(item);
            }

            return recipes;
        }

        /// <summary>
        /// Write data into a list
        /// </summary>
        /// <param name="data">recipe list</param>
        public void WriteDataRecipe(IList<Recipe> data)
        {
            throw new NotImplementedException();
        }
    }
}
