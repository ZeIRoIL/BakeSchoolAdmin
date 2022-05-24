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
    using MongoDB.Driver;

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
#warning need the implemtation

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// The WriteData.
        /// </summary>
        /// <param name="data">The data<see cref="IList{Recipe}"/>.</param>
        public void WriteData(IList<Recipe> data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The GetCategoryObservableCollection.
        /// </summary>
        /// <param name="recipesList">The recipesList<see cref="IList{Recipe}"/>.</param>
        /// <returns>The <see cref="ObservableCollection{Recipe}"/>.</returns>
        public ObservableCollection<Recipe> GetCategoryObserv(IList<Recipe> recipesList)
        {
            // create an emtpy observable collection object
            ObservableCollection<Recipe> recipes = new ObservableCollection<Recipe>();

            // loop through all the records and add to observable collection object
            foreach (var item in recipesList)
                recipes.Add(item);
            return recipes;
        }
    }
}
