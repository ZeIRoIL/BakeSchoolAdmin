using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_DataConnection
{
    public class ConnectionMongoDB
    {
        public ConnectionMongoDB()
        {
            MongoClient client = new MongoClient();
            IMongoDatabase db = client.GetDatabase("");
            
        }
    }
}
