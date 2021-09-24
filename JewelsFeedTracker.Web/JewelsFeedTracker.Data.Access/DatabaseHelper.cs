using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;
using System.IO;
using Serilog;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace JewelsFeedTracker.Data.Access
{
    public class DataBaseHelper
    {
        private static ILogger<DataBaseHelper> _logger;
        private static SqlConnection con;
        public DataBaseHelper()
        {

        }
        public DataBaseHelper(ILogger<DataBaseHelper> logger)
        {
            _logger = logger;
        }
        public static string ConnectionString
        {
            //IConfigurationBuilder builder = new ConfigurationBuilder();
            //builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            //var root = builder.Build();
            //var sampleConnectionString = root.GetSection("ConnectionStrings").GetSection("SqlServerConnection").Value;

            get
            {

                string basePath = System.AppContext.BaseDirectory;
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json")
                    .Build();

                return configuration.GetSection("ConnectionStrings").GetSection("SqlServerConnection").Value;
            }

        }

        public static void OpenConection()
        {
            con = new SqlConnection(ConnectionString);
            if (con.State == ConnectionState.Closed)
                con.Open();
        }
        public static void CloseConnection()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
        public static void ExecuteQueries(string Query_)
        {
            SqlCommand cmd = new SqlCommand(Query_, con);
            cmd.ExecuteNonQuery();
        }
        public static DataRow[] DataReaderList(string Query_)
        {
            SqlCommand cmd = new SqlCommand(Query_, con);
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(Query_, con))
            {
                da.Fill(dt);
            }
            var dtEnumerable = dt.AsEnumerable();
            DataRow[] resultArray = dtEnumerable.ToArray();
            return resultArray;
        }
        public static string DataReader(string Query_)
        {
            string result = string.Empty;
            SqlCommand cmd = new SqlCommand(Query_, con);
            {
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                    result = dr.GetString(0);
            }
            return result;
        }
        public static ArrayList GetArrayList(string Query_)
        {
            SqlCommand cmd = new SqlCommand(Query_, con);
            ArrayList rowList = new ArrayList();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                rowList.Add(values);
            }
            return rowList;
        }


        public static DataSet ExecuteQueryByQuery(string query)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(query, con))
            {
                da.SelectCommand.CommandTimeout = 120;
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
        public static DataTable ExecuteProcedure(string PROC_NAME, params object[] parameters)
        {
            try
            {
                if (parameters.Length % 2 != 0)
                    throw new ArgumentException("Wrong number of parameters sent to procedure. Expected an even number.");
                DataTable a = new DataTable();
                List<SqlParameter> filters = new List<SqlParameter>();

                string query = "EXEC " + PROC_NAME;

                bool first = true;
                for (int i = 0; i < parameters.Length; i += 2)
                {
                    filters.Add(new SqlParameter(parameters[i] as string, parameters[i + 1]));
                    query += (first ? " " : ", ") + ((string)parameters[i]);
                    first = false;
                }

                a = Query(query, filters);
                return a;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable ExecuteQuery(string query, params object[] parameters)
        {
            try
            {
                if (parameters.Length % 2 != 0)
                    throw new ArgumentException("Wrong number of parameters sent to procedure. Expected an even number.");
                DataTable a = new DataTable();
                List<SqlParameter> filters = new List<SqlParameter>();

                for (int i = 0; i < parameters.Length; i += 2)
                    filters.Add(new SqlParameter(parameters[i] as string, parameters[i + 1]));

                a = Query(query, filters);
                return a;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int ExecuteNonQuery(string query, params object[] parameters)
        {
            try
            {
                if (parameters.Length % 2 != 0)
                    throw new ArgumentException("Wrong number of parameters sent to procedure. Expected an even number.");
                List<SqlParameter> filters = new List<SqlParameter>();

                for (int i = 0; i < parameters.Length; i += 2)
                    filters.Add(new SqlParameter(parameters[i] as string, parameters[i + 1]));
                return NonQuery(query, filters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object ExecuteScalar(string query, params object[] parameters)
        {
            try
            {
                if (parameters.Length % 2 != 0)
                    throw new ArgumentException("Wrong number of parameters sent to query. Expected an even number.");
                List<SqlParameter> filters = new List<SqlParameter>();

                for (int i = 0; i < parameters.Length; i += 2)
                    filters.Add(new SqlParameter(parameters[i] as string, parameters[i + 1]));
                return Scalar(query, filters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Private Methods

        private static DataTable Query(String consulta, IList<SqlParameter> parametros)
        {
            try
            {

                DataTable dt = new DataTable();               
                SqlConnection connection = new SqlConnection(ConnectionString);
                SqlCommand command = new SqlCommand();
                SqlDataAdapter da;
                try
                {
                    command.Connection = connection;
                    command.CommandText = consulta;
                    if (parametros != null)
                    {
                        command.Parameters.AddRange(parametros.ToArray());
                    }
                    da = new SqlDataAdapter(command);
                    da.Fill(dt);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
                return dt;
            }
            catch (Exception)
            {
                throw;
            }

        }

        private static int NonQuery(string query, IList<SqlParameter> parametros)
        {
            try
            {
                DataSet dt = new DataSet();              
                SqlConnection connection = new SqlConnection(ConnectionString);
                SqlCommand command = new SqlCommand();

                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = query;
                    command.Parameters.AddRange(parametros.ToArray());
                    return command.ExecuteNonQuery();

                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static object Scalar(string query, List<SqlParameter> parametros)
        {
            try
            {
                DataSet dt = new DataSet();            
                SqlConnection connection = new SqlConnection(ConnectionString);
                SqlCommand command = new SqlCommand();

                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = query;
                    command.Parameters.AddRange(parametros.ToArray());
                    return command.ExecuteScalar();

                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool BulkCopy(DataTable sourceDt, string feedName)
        {
            bool recordTobeAdded = false;
            if (sourceDt.Rows.Count > 0)
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.BatchSize = 10000;
                        sqlBulkCopy.NotifyAfter = 5000;
                        sqlBulkCopy.SqlRowsCopied += (sender, eventArgs) =>
                        {
                            _logger.LogInformation(eventArgs.RowsCopied + " Rows Copied of " + feedName + " in " + sqlBulkCopy.BulkCopyTimeout + " Sec " + eventArgs.RowsCopied);
                        };
                        sqlBulkCopy.DestinationTableName = "dbo.Test";
                        sqlBulkCopy.ColumnMappings.Add(sourceDt.Columns[0].ToString(), "Stock_ID");
                        sqlBulkCopy.ColumnMappings.Add(sourceDt.Columns[1].ToString(), "StockNo");
                        con.Open();
                        try
                        {
                            sqlBulkCopy.WriteToServer(sourceDt);
                            recordTobeAdded = true;
                        }
                        catch (Exception exception)
                        {
                            recordTobeAdded = false;
                            // loop through all inner exceptions to see if any relate to a constraint failure
                            bool dataExceptionFound = false;
                            Exception tmpException = exception;
                            while (tmpException != null)
                            {
                                if (tmpException is SqlException
                                   && tmpException.Message.Contains("constraint"))
                                {
                                    dataExceptionFound = true;
                                    break;
                                }
                                tmpException = tmpException.InnerException;
                            }
                            if (dataExceptionFound)
                            {
                                // call the helper method to document the errors and invalid data
                                string errorMessage = GetBulkCopyFailedData(sqlBulkCopy.DestinationTableName, feedName, sourceDt.CreateDataReader());
                                _logger.LogError("Database Exception Occurred  " + feedName + " with error" + errorMessage + " and inner Error " + exception.StackTrace);

                            }
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            return recordTobeAdded;

        }

        public static string GetBulkCopyFailedData(string tableName, string feedName, IDataReader dataReader)
        {
            StringBuilder errorMessage = new StringBuilder("Bulk copy failures:" + Environment.NewLine);
            SqlConnection connection = null;
            SqlTransaction transaction = null;
            SqlBulkCopy bulkCopy = null;
            DataTable tmpDataTable = new DataTable();

            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
                transaction = connection.BeginTransaction();
                bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.CheckConstraints, transaction);
                bulkCopy.DestinationTableName = tableName;

                // create a datatable with the layout of the data.
                DataTable dataSchema = dataReader.GetSchemaTable();
                foreach (DataRow row in dataSchema.Rows)
                {
                    tmpDataTable.Columns.Add(new DataColumn(
                       row["ColumnName"].ToString(),
                       (Type)row["DataType"]));
                }

                // create an object array to hold the data being transferred into tmpDataTable 
                //in the loop below.
                object[] values = new object[dataReader.FieldCount];

                // loop through the source data
                while (dataReader.Read())
                {
                    // clear the temp DataTable from which the single-record bulk copy will be done
                    tmpDataTable.Rows.Clear();

                    // get the data for the current source row
                    dataReader.GetValues(values);

                    // load the values into the temp DataTable
                    tmpDataTable.LoadDataRow(values, true);

                    // perform the bulk copy of the one row
                    try
                    {
                        bulkCopy.WriteToServer(tmpDataTable);

                    }
                    catch (Exception ex)
                    {
                        // an exception was raised with the bulk copy of the current row. 
                        // The row that caused the current exception is the only one in the temp 
                        // DataTable, so document it and add it to the error message.
                        DataRow faultyDataRow = tmpDataTable.Rows[0];
                        errorMessage.AppendFormat("Error: {0}{1}", ex.Message, Environment.NewLine);
                        errorMessage.AppendFormat("Row data: {0}", Environment.NewLine);
                        foreach (DataColumn column in tmpDataTable.Columns)
                        {
                            errorMessage.AppendFormat(
                               "\tColumn {0} - [{1}]{2}",
                               column.ColumnName,
                               faultyDataRow[column.ColumnName].ToString(),
                               Environment.NewLine);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Feed Name : " + feedName +
                    "Unable to document SqlBulkCopy errors. See inner exceptions for details.", ex);
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            return errorMessage.ToString();
        }
        #endregion
    }



}
