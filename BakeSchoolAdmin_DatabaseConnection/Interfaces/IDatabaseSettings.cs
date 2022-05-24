namespace BakeSchoolAdmin_DatabaseConnection.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="IDatabaseSettings{T}" />.
    /// </summary>
    /// <typeparam name="T">Only the database interface</typeparam>
    public interface IDatabaseSettings<T> where T : class
    {
        /// <summary>
        /// check the initialize for the database.
        /// </summary>
        /// <returns>the value true/false</returns>
        bool init();

        /// <summary>
        /// Read single dataset from service.
        /// </summary>
        /// <param name="id">the data id from the database</param>
        /// <returns>the id data</returns>
        T ReadData(int id);

        /// <summary>
        /// Read all datasets form service.
        /// </summary>
        /// <returns>The IList of a generic class</returns>
        IList<T> ReadData();

        /// <summary>
        /// Write single dataset to service.
        /// </summary>
        /// <param name="data">Context from the database</param>
        void WriteData(T data);

        /// <summary>
        /// Write all datasets to service.
        /// </summary>
        /// <param name="data">Context from the database</param>
        void WriteData(IList<T> data);
    }
}
