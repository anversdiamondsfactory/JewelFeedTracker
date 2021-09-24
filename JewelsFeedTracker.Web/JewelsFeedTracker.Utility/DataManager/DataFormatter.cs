using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Extensions.Logging;
using TimeZoneConverter;

namespace JewelsFeedTracker.Utility.RowDataManager
{
    // this class is used to perform the data operation like saving into local,conversion into various output like json, data table, csv, generic list etc.
    public static class DataFormatter
    {
        public static string responseOutput = string.Empty;
       
        /// <summary>
        /// Save csv File locally
        /// </summary>
        /// <param name="dt"></param>
        public static void SaveFileLocalFolder(DataTable dt, string filePath)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            string fileName = filePath + System.DateTime.Now.Date.ToString("MM/dd/yyyy") + ".csv";
            System.IO.File.WriteAllText(fileName, sb.ToString());

        }
        /// <summary>
        /// Convert CSV To Datatable
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static DataTable ToDatableByCSV(string url)
        {

            DataTable dt = new DataTable();
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                StreamReader sr = new StreamReader(resp.GetResponseStream());

                string currentline = string.Empty;
                bool doneHeader = false;
                string[] splitLine = null;
                string[] splitRowLine = null;
                while ((currentline = sr.ReadLine()) != null)
                {
                    currentline = DataFormatter.FastReplacer(currentline);
                    if (currentline.Contains(","))
                        splitLine = currentline.Split(",");
                    else if (currentline.Contains(";"))
                        splitLine = currentline.Split(";");
                    if (!doneHeader)
                    {
                        foreach (string item in splitLine)
                        {
                            string newVal = item;
                            newVal = newVal == "Girdle%" ? "GirdlePer" : newVal == "Stock#" ? "StockNo" : newVal == "Rap-Price" ? "RapPrice" : newVal == "Report#" ? "ReportNo"
                                : newVal == "Depth%" ? "DepthPer" : newVal == "Table%" ? "TablePer" : newVal;
                            dt.Columns.Add(newVal);
                        }
                        doneHeader = true;
                        continue;
                    }
                    dt.Rows.Add();
                    int colCount = 0;

                    if (currentline.Contains(","))
                        splitRowLine = currentline.Split(",");
                    else if (currentline.Contains(";"))
                        splitRowLine = currentline.Split(";");
                    foreach (string item in splitLine)
                    {
                        dt.Rows[dt.Rows.Count - 1][colCount] = item;
                        colCount++;
                    }

                }

                string results = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
                return dt;
            }
            return dt;
        }
        /// <summary>
        /// Convert Datable To List Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToListByDataTable<T>(DataTable dt, string feed)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToList();

            var properties = typeof(T).GetProperties();

            var result = dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();

                foreach (var pro in properties)
                {
                    string dataType = pro.PropertyType.Name;
                    if (columnNames.Contains(pro.Name))
                    {

                        //properties.SetValue(objT, Convert.ToInt32(value.AsPrimitive().Value));
                        pro.SetValue(objT, SetFieldValue(row, pro.Name, dataType), null);


                    }

                }

                return objT;
            }).ToList();
            DataFormatter.ExportCsv(result, feed); // to-to later
            return result;
        }
        /// <summary>
        /// SetFieldValue
        /// </summary>
        /// <param name="row"></param>
        /// <param name="name"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static dynamic SetFieldValue(DataRow row, string name, string dataType)
        {
            dynamic result;
            switch (dataType)
            {
                case "Decimal":
                    result = row[name] == null || row[name].ToString() == String.Empty ? 0 : decimal.Parse(row[name].ToString());
                    break;
                case "Int64":
                    result = row[name] == null || row[name].ToString() == String.Empty ? 0 : int.Parse(row[name].ToString());
                    break;
                default:
                    result = row[name] == DBNull.Value ? string.Empty : row[name].ToString();
                    break;

            }
            return result;
        }

        /// <summary>
        /// Json Deserialize To List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static IList<T> DeserializeToList<T>(string jsonString)
        {

            var array = JArray.Parse(jsonString);
            IList<T> objectsList = new List<T>();

            foreach (var item in array)
            {
                try
                {
                    // CorrectElements  
                    objectsList.Add(item.ToObject<T>());
                }
                catch (Exception ex)
                {

                }
            }

            return objectsList;
        }
        /// <summary>
        /// Check for Valid Json
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// SetFeedFileName
        /// </summary>
        /// <param name="feed"></param>
        /// <param name="FileType"></param>
        /// <returns></returns>
        public static string SetFeedFileName(string feed, char FileType)
        {
            string[] path = null;
            string fileName = string.Empty;
            path = AppDomain.CurrentDomain.BaseDirectory.Contains("bin") ? AppDomain.CurrentDomain.BaseDirectory.Split("bin") : AppDomain.CurrentDomain.BaseDirectory.Split("");
            if (!System.IO.Directory.Exists(path[0] + "FeedArchieves\\" + "\\stone_feed\\" + feed + "\\"))
                System.IO.Directory.CreateDirectory(path[0] + "FeedArchieves\\" + "\\stone_feed\\" + feed + "\\");

            string file_path = path[0] + "FeedArchieves\\" + "stone_feed\\" + feed + "\\";
            string _filename = file_path + feed + "_";
           

            if (FileType == 'R')
                fileName = _filename + "RawContent_" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".csv"; //"stone_feed\\" + feed + "\\" + "_RawContent_" + System.DateTime.Now.Date.ToString("MM/dd/yyyy  h:mm:ss") + ".csv";
            else
                fileName = _filename + "OptimisedContent_" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".csv";
            return fileName;
        }
        /// <summary>
        /// ExportCsv
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="genericList"></param>
        /// <param name="fileName"></param>
        public static void ExportCsv<T>(List<T> genericList, string fileName)
        {
            var sb = new StringBuilder();
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var finalPath = Path.Combine(basePath, fileName + ".csv");
            var header = "";
            var info = typeof(T).GetProperties();
            if (!System.IO.File.Exists(finalPath))
            {
                var file = System.IO.File.Create(finalPath);
                file.Close();
                foreach (var prop in typeof(T).GetProperties())
                {
                    header += prop.Name + "; ";
                }
                header = header.Substring(0, header.Length - 2);
                sb.AppendLine(header);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            }
            foreach (var obj in genericList)
            {
                sb = new StringBuilder();
                var line = "";
                foreach (var prop in info)
                {
                    line += prop.GetValue(obj, null) + "; ";
                }
                line = line.Substring(0, line.Length - 2);
                sb.AppendLine(line);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            }
        }

        private static string GetSplitRowData(string rowData)
        {
            string strResult = string.Empty;
            string[] strRow = rowData.Replace("{", "").Replace("}", "").Split(":");
            if (strRow[0].Equals("KEY_TO_SYMBOLS") && strRow[0].Contains(","))
            {
                strResult += strRow[0] + strRow[1].Replace(",", "");
            }
            else
                strResult += strRow[0] + strRow[1];
            return strResult;
        }

        public static DataTable GetJSONToDataTable(string JSONData)
        {
            DataTable dtUsingMethodReturn = new DataTable();
            string[] jsonStringArray = Regex.Split(JSONData.Replace("[", "").Replace("]", ""), "},{");
            List<string> ColumnsName = new List<string>();

            Regex reg = new Regex(@",(?=[^']*'([^']*'[^']*')*[^']*$)", RegexOptions.IgnoreCase);
            string strFormattedResult = string.Empty;

            foreach (string strJSONarr in jsonStringArray)
            {
                strFormattedResult = reg.Replace(strJSONarr, "");
                string[] jsonStringData = Regex.Split(strFormattedResult.Replace("{", "").Replace("}", ""), ",");
                foreach (string ColumnsNameData in jsonStringData)
                {
                    try
                    {
                        int idx = ColumnsNameData.IndexOf(":");
                        string ColumnsNameString = ColumnsNameData.Substring(0, idx).Replace("\"", "").Trim();
                        if (!ColumnsName.Contains(ColumnsNameString))
                        {
                            ColumnsName.Add(ColumnsNameString);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                    }
                }
                break;
            }
            foreach (string AddColumnName in ColumnsName)
            {
                dtUsingMethodReturn.Columns.Add(AddColumnName);
            }
            foreach (string strJSONarr in jsonStringArray)
            {
                strFormattedResult = reg.Replace(strJSONarr, "");
                string[] RowData = Regex.Split(strFormattedResult.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dtUsingMethodReturn.NewRow();
                foreach (string rowData in RowData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string RowColumns = rowData.Substring(0, idx).Replace("\"", "").Trim();
                        string RowDataString = rowData.Substring(idx + 1).Replace("\"", "").Trim();
                        nr[RowColumns] = RowDataString;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                dtUsingMethodReturn.Rows.Add(nr);
            }
            return dtUsingMethodReturn;
        }

        public static DataTable ConvertToDataTable<T>(List<T> models)
        {
            // creating a data table instance and typed it as our incoming model   
            // as I make it generic, if you want, you can make it the model typed you want.  
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties of that model  
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Loop through all the properties              
            // Adding Column name to our datatable  
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names    
                dataTable.Columns.Add(prop.Name);
            }
            // Adding Row and its value to our dataTable  
            foreach (T item in models)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows    
                    values[i] = Props[i].GetValue(item, null);
                }
                // Finally add value to datatable    
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        /// <summary>
        /// FastReplacer
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FastReplacer(string input)
        {
            return responseOutput = input.Replace("\"", "'").Replace("Girdle%", "GirdlePer")
                .Replace("Girdle%", "GirdlePer")
                .Replace("Stock#", "StockNo")
                .Replace("Rap-Price", "RapPrice")
                .Replace("Report#", "ReportNo")
                .Replace("Depth%", "DepthPer")
                .Replace("Table%", "TablePer")
                .Replace(@"\/", "/").Replace(@"\", "");
        }

        public static List<T> ConvertDataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

    }
    public static class DataTableMappingtoModel
    {

    }

}
