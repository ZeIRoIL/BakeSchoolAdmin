using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_DataConnection.Contexts
{
    class MongoCategoryContext : IMongoContext
    {
        private IMongoDatabase db { get; set; }
        private IMongoClient mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoCategoryContext(IOptions<Mongosetting>
        {

        }


    }
}
