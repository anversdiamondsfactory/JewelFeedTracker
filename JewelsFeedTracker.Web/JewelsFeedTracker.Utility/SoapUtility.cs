using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace JewelsFeedTracker.Utility
{
   public static class SoapUtility
    {
        public static BasicHttpBinding GetSoapBinding(string soapUrl)
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = 20000000;
            basicHttpBinding.MaxBufferSize = 20000000;
            basicHttpBinding.MaxBufferPoolSize = 20000000;
            return basicHttpBinding;
        }
    }
}
