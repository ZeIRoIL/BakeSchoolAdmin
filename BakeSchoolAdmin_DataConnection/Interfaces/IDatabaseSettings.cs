using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_DataConnection
{
    public interface IDatabaseSettings
    {
        string DatabaseConnection { get; set; }
        string DatabaseName { get; set; }
        string CategoryCollectionName { get; set; }
    }
}
