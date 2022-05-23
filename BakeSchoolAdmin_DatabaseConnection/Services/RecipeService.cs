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
        /// Defines the mapper.
        /// </summary>
        private IMapper mapper;

        /// <summary>
        /// Gets or sets the recipesdata.
        /// </summary>
        public IMongoCollection<RecipeData> recipesdata { get; set; }

        /// <summary>
        /// Defines the DatabaseConnection.
        /// </summary>
        private readonly string DatabaseConnection = "mongodb://localhost:27017";

        /// <summary>
        /// Defines the DatabaseName.
        /// </summary>
        private readonly string DatabaseName = "LearnBakeDb";

        /// <summary>
        /// Defines the RecipeCollectionName.
        /// </summary>
        private readonly string RecipeCollectionName = "RecipeCollection";

        /// <summary>
        /// Defines the Mongo Client..
        /// </summary>
        private MongoClient Mongoclient;

        /// <summary>
        /// The init.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool init()
        {
            try
            {
                this.Mongoclient = new MongoClient(DatabaseConnection);
                var database = Mongoclient.GetDatabase(DatabaseName);
                this.recipesdata = database.GetCollection<RecipeData>(RecipeCollectionName);
                try
                {
                    mapper = MappingProfil.InitializeAutoMapper().CreateMapper();
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
            IList<RecipeData> list = recipesdata.Find<RecipeData>
           (p => true).ToList<RecipeData>();

            IList<Recipe> recipes = mapper.Map<IList<RecipeData>, IList<Recipe>>(list);


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
        /// The GetCategoryObserv.
        /// </summary>
        /// <param name="recipesList">The recipesList<see cref="IList{Recipe}"/>.</param>
        /// <returns>The <see cref="ObservableCollection{Recipe}"/>.</returns>
        public ObservableCollection<Recipe> GetCategoryObserv(IList<Recipe> recipesList)
        {
            //create an emtpy observable collection object
            ObservableCollection<Recipe> recipes = new ObservableCollection<Recipe>();

            //loop through all the records and add to observable collection object
            foreach (var item in recipesList)
                recipes.Add(item);



            return recipes;
        }
    }
}
