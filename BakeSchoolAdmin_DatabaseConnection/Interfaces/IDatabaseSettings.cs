using BakeSchoolAdmin_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_DatabaseConnection.Interfaces
{
    public interface IDatabaseSettings<T> where T : class
    {
        
        /// <summary>
        /// check the initialize for the database
        /// </summary>
        /// <returns></returns>
        bool init();
        /// <summary>
        /// Read single dataset from service 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T ReadData(int id);

        /// <summary>
        /// Read all datasets form service
        /// </summary>
        /// <returns></returns>
        IList<T> ReadData();

        /// <summary>
        /// Write single dataset to service
        /// </summary>
        /// <param name="data"></param>
        void WriteData(T data);

        /// <summary>
        /// Write all datasets to service
        /// </summary>
        /// <param name="data"></param>
        void WriteData(IList<T> data);

    }
}
