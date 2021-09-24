using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace JewelsFeedTracker.Data.Access
{   
    public class SingletonCon1
    {       
        private string defaultConnectionString;       
        public SingletonCon1(IConfiguration iconfiguration)
        {
            defaultConnectionString = iconfiguration.GetConnectionString("SqlServerConnection");
        }

        public  string DefaultConnectionString
        {
            get
            {               
                return defaultConnectionString;
            }
        }
       
    }

}