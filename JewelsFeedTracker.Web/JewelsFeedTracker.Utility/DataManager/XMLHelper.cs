using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace JewelsFeedTracker.Utility.DataManager
{
    public static class XMLHelper
    {
        public static List<T> ParseXML<T>(string xmlString, string rootElement)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(rootElement));
            StringReader stringReader = new StringReader(xmlString);
            List<T> resultList = (List<T>)serializer.Deserialize(stringReader);
            return resultList;

        }
        /// <summary>
        /// Converts XML string to DataTable
        /// </summary>
        /// <param name="Name">DataTable name</param>
        /// <param name="XMLString">XML string</param>
        /// <returns></returns>
        public static DataTable BuildDataTableFromXml(string XMLString)
        {
            StringReader theReader = new StringReader(XMLString);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);
            DataTable Dt = theDataSet.Tables[0];
            return Dt;
        }
    }
}
