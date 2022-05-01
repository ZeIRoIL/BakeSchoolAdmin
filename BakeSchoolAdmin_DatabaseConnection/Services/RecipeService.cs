using AutoMapper;
using BakeSchoolAdmin_DatabaseConnection.Interfaces;
using BakeSchoolAdmin_DatabaseConnection.Mapper;
using BakeSchoolAdmin_DatabaseConnection.Models;
using BakeSchoolAdmin_Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_DatabaseConnection.Services
{
    public class RecipeService : IDatabaseSettings<Recipe>
    {
        private IMapper mapper;

        public IMongoCollection<RecipeData> recipesdata { get; set; }

        private readonly string DatabaseConnection = "mongodb://localhost:27017";
        private readonly string DatabaseName = "test";
        private readonly string RecipeCollectionName = "RecipesCollection";

        private MongoClient client;
        public bool init()
        {
            try
            {
                this.client = new MongoClient(DatabaseConnection);
                var database = client.GetDatabase(DatabaseName);
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

        public Recipe ReadData(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Recipe> ReadData()
        {
#warning need the implemtation
           
            //// Get the data from mongodb into the list
            IList<RecipeData> list = recipesdata.Find<RecipeData>
           (p => true).ToList<RecipeData>();

            IList<Recipe> recipes = mapper.Map<IList<RecipeData>, IList<Recipe>>(list);


            return recipes;
        }

        public void WriteData(Recipe data)
        {
            throw new NotImplementedException();
        }

        public void WriteData(IList<Recipe> data)
        {
            throw new NotImplementedException();
        }
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
