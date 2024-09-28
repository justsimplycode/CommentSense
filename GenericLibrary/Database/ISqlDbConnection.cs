using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericLibrary.Database
{
    public interface ISqlDbConnection
    {
        /// <summary>
        /// Connecting to database using connection string.
        /// </summary>
        SqlConnection SqlConnectionToDb { get; set; }

        /// <summary>
        /// Opens the database connection if closed. 
        /// </summary>
        void Open();
        /// <summary>
        /// Closes the database connection if opened.
        /// </summary>
        void Close();
    }
}
