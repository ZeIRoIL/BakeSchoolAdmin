using AutoMapper;
using BakeSchoolAdmin_DatabaseConnection.Interfaces;
using BakeSchoolAdmin_DatabaseConnection.Mapper;
using BakeSchoolAdmin_DatabaseConnection.Models;
using BakeSchoolAdmin_Models;
using MongoDB.Bson;
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
    public class CategoryService : IDatabaseSettings<Category>
    {

        private IMapper mapper;

        public  IMongoCollection<CategoryData> categoriesdata { get; set; }
       
        private readonly string DatabaseConnection = "mongodb://localhost:27017";
        private readonly string DatabaseName = "LearnBakeDb";
        private readonly string CategoryCollectionName = "CategoryCollection";

        private MongoClient client;
        

        public CategoryService()
        {
            
        }
        public bool init()
        {
            try
            {
            this.client = new MongoClient(DatabaseConnection);
            var database = client.GetDatabase(DatabaseName);
            this.categoriesdata = database.GetCollection<CategoryData>(CategoryCollectionName);
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

        public Category GetCategory()
        {
            throw new NotImplementedException();
        }

        public Category ReadData(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Category> ReadData()
        {
            // Get the data from mongodb into the list
            IList<CategoryData> list = categoriesdata.Find<CategoryData>
            (p => true).ToList<CategoryData>();

            IList<Category> categories = mapper.Map<IList<CategoryData>, IList<Category>>(list);
            
            return categories;
        }

        public bool WriteData(Category data)
        {
            int[] levelRange = data.Details.difficultyLevelRange;

            string name = data.Details.Name;
            int id = data.Id;
            string text = data.Details.Text;
            int level = data.Details.Level;

            //BsonArray bArray = new BsonArray();
            //foreach (var term in level)
            //{
            //    bArray.Add(term.ToBson());
            //}l
            try
            {
                var document = new BsonDocument
            {
                    { "datasetId" , "category"},
                    { "categoryId" , id},
                    { "details",
                        new BsonDocument
                        {
                             { "name",name},
                             { "img","ffgs"},
                            {"text" ,text},
                            {"difficultyLevel" , level },
                            {"difficultyLevelRange",new BsonArray
                            { 1,2 } 
                            }
                        }
                     }
              };
                var database = client.GetDatabase(DatabaseName);
                var collection = database.GetCollection<BsonDocument>(CategoryCollectionName);
                collection.InsertOne(document);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public void WriteData(IList<Category> data)
        {
            throw new NotImplementedException();
        }
        public ObservableCollection<Category> GetCategoryObserv(IList<Category> categoriesList)
        {
            //create an emtpy observable collection object
            ObservableCollection<Category> categories = new ObservableCollection<Category>();
            
            //loop through all the records and add to observable collection object
            foreach (var item in categoriesList)
                categories.Add(item);



            return categories;
        }

        void IDatabaseSettings<Category>.WriteData(Category data)
        {
            throw new NotImplementedException();
        }
    }
}
