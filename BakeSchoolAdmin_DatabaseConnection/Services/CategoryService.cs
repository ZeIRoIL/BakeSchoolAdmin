namespace BakeSchoolAdmin_DatabaseConnection.Services
{
    using AutoMapper;
    using BakeSchoolAdmin_DatabaseConnection.Interfaces;
    using BakeSchoolAdmin_DatabaseConnection.Mapper;
    using BakeSchoolAdmin_DatabaseConnection.Models;
    using BakeSchoolAdmin_Models;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Defines the <see cref="CategoryService" />.
    /// </summary>
    public class CategoryService : IDatabaseSettings<Category>
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
        /// Defines the categoryCollectionName.
        /// </summary>
        private readonly string categoryCollectionName = "CategoryCollection";

        /// <summary>
        /// Defines the mapper.
        /// </summary>
        private IMapper mapper;

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
        /// Gets or sets the categories data.
        /// </summary>
        public IMongoCollection<CategoryData> categoriesdata { get; set; }

        /// <summary>
        /// The initialize .
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool init()
        {
            try
            {
                this.client = new MongoClient(this.databaseConnection);
                var database = this.client.GetDatabase(this.databaseName);
                this.categoriesdata = database.GetCollection<CategoryData>(this.categoryCollectionName);
                
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
        /// <returns>the value of right to connection</returns>
        public bool IsConnection()
        {
            this.client = new MongoClient(this.databaseConnection);
            var database = this.client.GetDatabase(this.databaseName);
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
            IList<CategoryData> list = this.categoriesdata.Find<CategoryData>(p => true).ToList<CategoryData>();

            IList<Category> categories = this.mapper.Map<IList<CategoryData>, IList<Category>>(list);

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

            this.categoriesdata.DeleteOne(deleteFilter);
            return true;
        }

        /// <summary>
        /// The WriteDataRecipe.
        /// </summary>
        /// <param name="data">The data<see cref="Category"/>.</param>
        /// <returns>The <see cref="bool"/>write data query</returns>
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
                    { "datasetId",  "category" },
                    { "categoryId",  id },
                    { "details",
                        new BsonDocument
                        {
                             { "name", name },
                             { "img", "ffgs" },
                            { "text",  text },
                            { "difficultyLevel",  level }
                        }
                     }
              };
                
                if (data.WantFileDb)
                {
                    string targetPathFolder = @".\database\category";

                    if (!Directory.Exists(targetPathFolder))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(targetPathFolder);
                        Console.WriteLine("The directory was created successfully at {0}.{1}", Directory.GetCreationTime(targetPathFolder), targetPathFolder);
                    }

                    if (this.PrettyWrite(document, targetPathFolder + @"\json.txt"))
                    {
                        MessageBox.Show("Save into the file!", targetPathFolder);
                    }
                }
                else
                {
                    var database = this.client.GetDatabase(this.databaseName);
                    var collection = database.GetCollection<BsonDocument>(this.categoryCollectionName);
                    collection.InsertOne(document);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Write the object into a file pretty
        /// </summary>
        /// <param name="obj">JSON object</param>
        /// <param name="fileName">name of the file</param>
        /// <returns>the value of update state</returns>
        public bool PrettyWrite(object obj, string fileName)
        {
            try
            {
                var jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
                File.WriteAllText(fileName, jsonString);
                return true;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Update the data in the database
        /// </summary>
        /// <param name="data">category data</param>
        /// <returns>the value of update state</returns>
        public bool UpdateData(Category data)
        {
            try
            {
                var document = new BsonDocument
            {
                    { "datasetId", "category" },
                    { "categoryId",  data.Id + 1 },
                    { "details",
                        new BsonDocument
                        {
                             { "name", data.Details.Name },
                             { "img", "ffgs" },
                            { "text", data.Details.Text },
                            { "difficultyLevel",  data.Details.Level }
                        }
                     }
              };

                var database = this.client.GetDatabase(this.databaseName);
                var collection = database.GetCollection<BsonDocument>(this.categoryCollectionName);

                var filter = Builders<BsonDocument>.Filter.Eq("categoryId", data.Id + 1);
                collection.ReplaceOne(filter, document);

                return true;
            }
            catch (Exception)
            {
                return false;   
            }
        }

        /// <summary>
        /// The WriteDataRecipe.
        /// </summary>
        /// <param name="data">The data<see cref="IList{Category}"/>.</param>
        public void WriteDataRecipe(IList<Category> data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Category observableCollection.
        /// </summary>
        /// <param name="categoriesList">The categoriesList<see cref="IList{Category}"/>.</param>
        /// <returns>The <see cref="ObservableCollection{Category}"/>.</returns>
        public ObservableCollection<Category> GetCategoryObserv(IList<Category> categoriesList)
        {
            // create an emtpy observable collection object
            ObservableCollection<Category> categories = new ObservableCollection<Category>();

            // loop through all the records and add to observable collection object
            foreach (var item in categoriesList)
            {
                categories.Add(item);
            }

            return categories;
        }

        /// <summary>
        /// The WriteDataRecipe.
        /// </summary>
        /// <param name="data">The data<see cref="Category"/>.</param>
        void IDatabaseSettings<Category>.WriteDataRecipe(Category data)
        {
            throw new NotImplementedException();
        }
    }
}
