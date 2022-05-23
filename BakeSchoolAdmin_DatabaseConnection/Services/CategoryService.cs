namespace BakeSchoolAdmin_DatabaseConnection.Services
{
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
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CategoryService" />.
    /// </summary>
    public class CategoryService : IDatabaseSettings<Category>
    {
        /// <summary>
        /// Defines the mapper.
        /// </summary>
        private IMapper mapper;

        /// <summary>
        /// Gets or sets the categoriesdata.
        /// </summary>
        public IMongoCollection<CategoryData> categoriesdata { get; set; }

        /// <summary>
        /// Defines the DatabaseConnection.
        /// </summary>
        private readonly string DatabaseConnection = "mongodb://localhost:27017";

        /// <summary>
        /// Defines the DatabaseName.
        /// </summary>
        private readonly string DatabaseName = "LearnBakeDb";

        /// <summary>
        /// Defines the CategoryCollectionName.
        /// </summary>
        private readonly string CategoryCollectionName = "CategoryCollection";

        /// <summary>
        /// Defines the client.
        /// </summary>
        private MongoClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        public CategoryService()
        {
        }

        /// <summary>
        /// The initialize .
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
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

        /// <summary>
        /// The GetCategory.
        /// </summary>
        /// <returns>The <see cref="Category"/>.</returns>
        public Category GetCategory()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ReadData.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Category"/>.</returns>
        public Category ReadData(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ReadData.
        /// </summary>
        /// <returns>The <see cref="IList{Category}"/>.</returns>
        public IList<Category> ReadData()
        {
            // Get the data from mongodb into the list
            IList<CategoryData> list = categoriesdata.Find<CategoryData>
            (p => true).ToList<CategoryData>();

            IList<Category> categories = mapper.Map<IList<CategoryData>, IList<Category>>(list);

            return categories;
        }

        /// <summary>
        /// The DeleteData.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool DeleteData(int id)
        {
            var deleteFilter = Builders<CategoryData>.Filter.Eq("categoryId", id);

            categoriesdata.DeleteOne(deleteFilter);
            return true;
        }

        /// <summary>
        /// The WriteData.
        /// </summary>
        /// <param name="data">The data<see cref="Category"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool WriteData(Category data)
        {
            

            string name = data.Details.Name;
            int id = data.Id;
            string text = data.Details.Text;
            int level = data.Details.Level;

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
                            {"difficultyLevel" , level }
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

        /// <summary>
        /// Update the data in the database
        /// </summary>
        /// <param name="data">category data</param>
        /// <returns></returns>
        public bool UpdateData(Category data)
        {
            try
            {
                var document = new BsonDocument
            {
                    { "datasetId" , "category"},
                    { "categoryId" , data.Id + 1},
                    { "details",
                        new BsonDocument
                        {
                             { "name",data.Details.Name},
                             { "img","ffgs"},
                            {"text" ,data.Details.Text},
                            {"difficultyLevel" , data.Details.Level }
                        }
                     }
              };
                
                
                var database = client.GetDatabase(DatabaseName);
                var collection = database.GetCollection<BsonDocument>(CategoryCollectionName);

                var filter = Builders<BsonDocument>.Filter.Eq("categoryId",data.Id + 1);
                collection.ReplaceOne(filter, document);

                return true;
            }
            catch (Exception)
            {
                return false;
                throw new Exception("Catagory cannot update");
            }
        }

        /// <summary>
        /// The WriteData.
        /// </summary>
        /// <param name="data">The data<see cref="IList{Category}"/>.</param>
        public void WriteData(IList<Category> data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The GetCategoryObserv.
        /// </summary>
        /// <param name="categoriesList">The categoriesList<see cref="IList{Category}"/>.</param>
        /// <returns>The <see cref="ObservableCollection{Category}"/>.</returns>
        public ObservableCollection<Category> GetCategoryObserv(IList<Category> categoriesList)
        {
            //create an emtpy observable collection object
            ObservableCollection<Category> categories = new ObservableCollection<Category>();

            //loop through all the records and add to observable collection object
            foreach (var item in categoriesList)
                categories.Add(item);

            return categories;
        }

        /// <summary>
        /// The WriteData.
        /// </summary>
        /// <param name="data">The data<see cref="Category"/>.</param>
        void IDatabaseSettings<Category>.WriteData(Category data)
        {
            throw new NotImplementedException();
        }
    }
}
