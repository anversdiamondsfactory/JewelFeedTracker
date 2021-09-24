using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel;
using Newtonsoft.Json;
using WebApiWithSwagger.Models;

namespace WebApiWithSwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController2 : ControllerBase
    {
        private readonly static int bufferSize = 64 * 1024;
        private byte[] compressed;
        public ValuesController2()
        {
            ValueSamples.Initialize();
        }      

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<Dictionary<int, string>>> Get()
        {
            var test = "foo bar baz very long string for example hdgfgfhfghfghfghfghfghfghfghfghfghfghfhg";


            var compressed = CompressBuffer(Encoding.UTF8.GetBytes(test));
            var decompressed = DeCompressBuffer(compressed);
            //BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            //basicHttpBinding.MaxReceivedMessageSize = 20000000;
            //basicHttpBinding.MaxBufferSize = 20000000;
            //basicHttpBinding.MaxBufferPoolSize = 20000000;

            //EndpointAddress endpointAddress = new EndpointAddress("http://akarshexports.com/getfullstock.asmx?WSDL");
            //ServiceReference1.GetFullStockSoapClient client = new ServiceReference1.GetFullStockSoapClient(basicHttpBinding, endpointAddress);
            //var response = await client.GetWebStockAsync();
            //response.Body contains the result
            return ValueSamples.MyValue;
        }

        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        // GET api/values/compress
        [HttpGet("compress{inputData}")]
        public async Task<ActionResult<byte[]>> Compress(byte[] inputData)
        {
            var test = "foo bar baz very long string for example hdgfgfhfghfghfghfghfghfghfghfghfghfghfhg";

            var compressed = CompressBuffer(Encoding.UTF8.GetBytes(test));
            return compressed;           
            if (inputData == null)
            {
                throw new ArgumentNullException("inputData must be non-null");
            }

            //using (var mso = new MemoryStream())
            //{
            //    using (var gs = new BufferedStream(new GZipStream(mso, CompressionMode.Compress), bufferSize))
            //    {
            //        gs.Write(inputData, 0, inputData.Length);
            //    }

            //    resArray = mso.ToArray();
            //}

            //return resArray;
            #region MyRegion


            //// harikrishna.php 
            //var dataList = GetCSV("https://services.diamondsfactory.com/index.php?route=api/download_stone_feed&name=harikrishna");

            //        using (var httpClient = new HttpClient())
            //        {
            //            using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://sjworldapi.azurewebsites.net/share/sjapi.asmx/GetData?LoginName=nice&PassWord=nice321"))
            //            {
            //                var response = await httpClient.SendAsync(request);
            //            }
            //        }


            //        using (var httpClient = new HttpClient())
            //        {
            ////            ServicePointManager.ServerCertificateValidationCallback =
            ////new RemoteCertificateValidationCallback(ValidateServerCertificate);

            //            var dataList0 = GetCSV("https://services.diamondsfactory.com/index.php?route=api/download_stone_feed&name=harikrishna");
            //            // dinesh.php
            //            Uri myUri = new Uri("https://websvr.jbbros.com/jbapi.aspx?UserId=navgrahaausd&APIKey=CD35EDF0-9E35-4F18-8767-A9D055B6EC9A&Action=S&Shape=CUMBR,EM,FS,HRT,MQ,OV,PS,PR,RT,RD,SQEM,SQRT,TR,SQBR,RECBR,TA,CUBR,CU&CaratFrom=0.01&CaratTo=99.99&Color=D,E,F,G,H,I,J,K,L,M,N,O,D,E+,F+,G+,H+,I+,J+,K+,L+,M+,N+,O+,P+&Purity=IF,VS1,VS2,VVS1,VVS2,SI1,SI2,SI3,I1,I2,I3,IF,VS1+,VS2+,VVS1+,VVS2+,SI1+,SI2+,SI3+,I1+,I2+,I3+&Lab=GIA&PG=1", UriKind.Absolute);

            //            string url = "https://websvr.jbbros.com/jbapi.aspx?UserId=navgrahaausd&APIKey=CD35EDF0-9E35-4F18-8767-A9D055B6EC9A&Action=S&Shape=CUMBR,EM,FS,HRT,MQ,OV,PS,PR,RT,RD,SQEM,SQRT,TR,SQBR,RECBR,TA,CUBR,CU&CaratFrom=0.01&CaratTo=99.99&Color=D,E,F,G,H,I,J,K,L,M,N,O,D,E+,F+,G+,H+,I+,J+,K+,L+,M+,N+,O+,P+&Purity=IF,VS1,VS2,VVS1,VVS2,SI1,SI2,SI3,I1,I2,I3,IF,VS1+,VS2+,VVS1+,VVS2+,SI1+,SI2+,SI3+,I1+,I2+,I3+&Lab=GIA&PG=1";
            //            ///// finestar.php
            //            XmlDocument xdoc = new XmlDocument();
            //            string jsonStr;
            //            ///// dfe.php
            //            var dataList = GetCSV("http://dfe.diamondsfactory.com/pd/DFR_Stock_Stone.csv");
            //            using (var wc = new WebClient())
            //            { 
            //                jsonStr = wc.DownloadString("https://finestardiamonds.com/api/Stock/GetFullStockInventory?Username=karan.jhaveri@navgrahaa.com&Password=karan123&Company=NAVGRAHAAJEWELSPRIVATELIMITED");
            //                //var RootObject = JsonConvert.DeserializeObject<List<Child>>(@"[{'REPORT_NO':'6197242335','PACKET_NO':'142992171','SHAPE':'ROUND','CTS':'1.4','COLOR':'L','CUT':'EX','POLISH':'EX','SYMM':'EX','FLS':'MED','PURITY':'VVS2','LAB':'GIA','LENGTH_1':'7.18','WIDTH':'7.14','DEPTH':'4.39','TABLE_PER':'62','DEPTH_PER':'61.3','CROWN_ANGLE':'34','CROWN_HEIGHT':'12.5','PAV_ANGLE':'41.6','PAV_HEIGHT':'44.5','SIDE_NATTS':'SN0','CROWN_OPEN':'N','REPORT_COMMENT':'Additional clouds are not shown. Additional pinpoints are not shown.','KEY_TO_SYMBOLS':'Cloud, Pinpoint, Needle','DISC_PER':'-30','RAP_PRICE':'4200','NET_RATE':'2940','NET_VALUE':'4116','HA':'VG','BRILLIANCY':'EX','SHADE':'LYL','CULET':'NON','GIRDLE':'STK','CERTI_LINK':'','COMMENTS':'','CENTER_NATTS':'CN0','SIDE_FEATHER':'SW1','CENTER_FEATHER':'CW1','EYE_CLEAN':'Y','MEASUREMENT':'7.18 X 7.14 X 4.39','AVG_DIA':'0.00','DNA':'http://diadna.com/diamondview.aspx?pid=142992171','REAL_IMAGE':'https://s3.ap-south-1.amazonaws.com/finestargroup/RealImages/142992171.jpg','HEART_IMAGE':'https://s3.ap-south-1.amazonaws.com/finestargroup/HeartImages/142992171.jpg','ARROW_IMAGE':'https://s3.ap-south-1.amazonaws.com/finestargroup/ArrowImages/142992171.jpg','PLOTTING_IMAGE':'https://s3.ap-south-1.amazonaws.com/finestargroup/PlottingImages/6197242335.gif','VIDEO':'https://s3.ap-south-1.amazonaws.com/finestargroup/viewer4/Vision360.html?d=142992171&s=100&v=2&sv=0,1,2,3,4&z=1&btn=1,2,3,5&s=100','CERTI_IMAGE':'https://s3.ap-south-1.amazonaws.com/finestargroup/CertiImages/6197242335.pdf','LOCATION':'INDIA','B2B_MP4':'https://s3.ap-south-1.amazonaws.com/finestargroup/Mov/142992171.mp4','B2C_MP4':'https://finestargroup.s3.ap-south-1.amazonaws.com/viewer3/MP4_Videos/142992171.mp4'},{'REPORT_NO':'2195378212','PACKET_NO':'646829610','SHAPE':'CS','CTS':'0.11','COLOR':'FB','CUT':'','POLISH':'EX','SYMM':'VG','FLS':'FNT','PURITY':'SI1','LAB':'GIA','LENGTH_1':'2.69','WIDTH':'2.61','DEPTH':'1.74','TABLE_PER':'0','DEPTH_PER':'0','CROWN_ANGLE':'34.77','CROWN_HEIGHT':'12.73','PAV_ANGLE':'59.3','PAV_HEIGHT':'45.33','SIDE_NATTS':'SN0','CROWN_OPEN':'N','REPORT_COMMENT':'','KEY_TO_SYMBOLS':'','DISC_PER':'0','RAP_PRICE':'60000','NET_RATE':'60000','NET_VALUE':'6600','HA':'','BRILLIANCY':'EX','SHADE':'FGB','CULET':'','GIRDLE':'','CERTI_LINK':'','COMMENTS':'','CENTER_NATTS':'CN0','SIDE_FEATHER':'SW0','CENTER_FEATHER':'CW0','EYE_CLEAN':'Y','MEASUREMENT':'2.69 X 2.61 X 1.74','AVG_DIA':'1.03','DNA':'http://diadna.com/diamondview.aspx?pid=646829610','REAL_IMAGE':'https://s3.ap-south-1.amazonaws.com/finestargroup/RealImages/646829610.jpg','HEART_IMAGE':'','ARROW_IMAGE':'','PLOTTING_IMAGE':'','VIDEO':'','CERTI_IMAGE':'https://s3.ap-south-1.amazonaws.com/finestargroup/CertiImages/2195378212.pdf','LOCATION':'INDIA','B2B_MP4':'https://s3.ap-south-1.amazonaws.com/finestargroup/Mov/646829610.mp4','B2C_MP4':'https://finestargroup.s3.ap-south-1.amazonaws.com/viewer3/MP4_Videos/646829610.mp4'},{'REPORT_NO':'2195378212','PACKET_NO':'646829610','SHAPE':'CS','CTS':'0.11','COLOR':'FB','CUT':'','POLISH':'EX','SYMM':'VG','FLS':'FNT','PURITY':'SI1','LAB':'GIA','LENGTH_1':'2.69','WIDTH':'2.61','DEPTH':'1.74','TABLE_PER':'0','DEPTH_PER':'0','CROWN_ANGLE':'34.77','CROWN_HEIGHT':'12.73','PAV_ANGLE':'59.3','PAV_HEIGHT':'45.33','SIDE_NATTS':'SN0','CROWN_OPEN':'N','REPORT_COMMENT':'','KEY_TO_SYMBOLS':'','DISC_PER':'0','RAP_PRICE':'60000','NET_RATE':'60000','NET_VALUE':'6600','HA':'','BRILLIANCY':'EX','SHADE':'FGB','CULET':'','GIRDLE':'','CERTI_LINK':'','COMMENTS':'','CENTER_NATTS':'CN0','SIDE_FEATHER':'SW0','CENTER_FEATHER':'CW0','EYE_CLEAN':'Y','MEASUREMENT':'2.69 X 2.61 X 1.74','AVG_DIA':'1.03','DNA':'http://diadna.com/diamondview.aspx?pid=646829610','REAL_IMAGE':'https://s3.ap-south-1.amazonaws.com/finestargroup/RealImages/646829610.jpg','HEART_IMAGE':'','ARROW_IMAGE':'','PLOTTING_IMAGE':'','VIDEO':'','CERTI_IMAGE':'https://s3.ap-south-1.amazonaws.com/finestargroup/CertiImages/2195378212.pdf','LOCATION':'INDIA','B2B_MP4':'https://s3.ap-south-1.amazonaws.com/finestargroup/Mov/646829610.mp4','B2C_MP4':'https://finestargroup.s3.ap-south-1.amazonaws.com/viewer3/MP4_Videos/646829610.mp4'},{'REPORT_NO':'2195378212','PACKET_NO':'646829610','SHAPE':'CS','CTS':'0.11','COLOR':'FB','CUT':'','POLISH':'EX','SYMM':'VG','FLS':'FNT','PURITY':'SI1','LAB':'GIA','LENGTH_1':'2.69','WIDTH':'2.61','DEPTH':'1.74','TABLE_PER':'0','DEPTH_PER':'0','CROWN_ANGLE':'34.77','CROWN_HEIGHT':'12.73','PAV_ANGLE':'59.3','PAV_HEIGHT':'45.33','SIDE_NATTS':'SN0','CROWN_OPEN':'N','REPORT_COMMENT':'','KEY_TO_SYMBOLS':'','DISC_PER':'0','RAP_PRICE':'60000','NET_RATE':'60000','NET_VALUE':'6600','HA':'','BRILLIANCY':'EX','SHADE':'FGB','CULET':'','GIRDLE':'','CERTI_LINK':'','COMMENTS':'','CENTER_NATTS':'CN0','SIDE_FEATHER':'SW0','CENTER_FEATHER':'CW0','EYE_CLEAN':'Y','MEASUREMENT':'2.69 X 2.61 X 1.74','AVG_DIA':'1.03','DNA':'http://diadna.com/diamondview.aspx?pid=646829610','REAL_IMAGE':'https://s3.ap-south-1.amazonaws.com/finestargroup/RealImages/646829610.jpg','HEART_IMAGE':'','ARROW_IMAGE':'','PLOTTING_IMAGE':'','VIDEO':'','CERTI_IMAGE':'https://s3.ap-south-1.amazonaws.com/finestargroup/CertiImages/2195378212.pdf','LOCATION':'INDIA','B2B_MP4':'https://s3.ap-south-1.amazonaws.com/finestargroup/Mov/646829610.mp4','B2C_MP4':'https://finestargroup.s3.ap-south-1.amazonaws.com/viewer3/MP4_Videos/646829610.mp4'}]".Replace("/",""));
            //                var rootobj = JsonConvert.DeserializeObject<FineStarRootobject>(jsonStr.Replace(@"\", ""));

            //            }
            //            ///// glowstar.php
            //            using (var wc = new WebClient())
            //            {
            //                jsonStr = wc.DownloadString("https://www.glowstaronline.com/inventory/website/navgrahaa.php?un=NavGrahaa&p=bd339db4a2fc08665267ae07989f0e04");
            //                var pkdtlobj = JsonConvert.DeserializeObject<RootPKTDTL>(jsonStr.Replace("\"", "'").Replace("Girdle%", "GirdlePer").Replace(@"\/", "/").Replace(@"\", ""));
            //            }
            //            var xmlDoc = new XmlDocument();
            //            xmlDoc.LoadXml(jsonStr);
            //            xdoc.Load("https://finestardiamonds.com/api/Stock/GetFullStockInventory?Username=karan.jhaveri@navgrahaa.com&Password=karan123&Company=NAVGRAHAAJEWELSPRIVATELIMITED");

            //            XmlElement root = xdoc.DocumentElement;
            //            XmlNodeList nodes = root.SelectNodes("/string/");
            //            //XmlElement elt = xdoc.SelectSingleNode("//SubMenu[@id='STE']") as XmlElement;
            //            XmlNodeList nodeList = xdoc.GetElementsByTagName("string");
            //            string Short_Fall = string.Empty;
            //            foreach (XmlNode node in nodeList)
            //            {
            //                Short_Fall = node.InnerText;
            //            }
            //            ///



            //            var client = new HttpClient();
            //            string reqUrl = url;
            //            var prodResp = await client.GetAsync(reqUrl);
            //            if (!prodResp.IsSuccessStatusCode)
            //            {

            //            }

            //            var prods = await prodResp.Content.ReadAsAsync<object>();
            //            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(myUri);
            //            //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            //            //StreamReader sr = new StreamReader(resp.GetResponseStream());
            //            //var results = sr.ReadToEnd();
            //            //sr.Close();
            //            //using (var client = new WebClient())
            //            //{
            //            //	var content = client.DownloadData(url);
            //            //	using (var stream = new MemoryStream(content))
            //            //	{

            //            //	}
            //            //}
            //            //var data =	await httpClient.GetAsync(myUri);
            //            var request = await httpClient.GetAsync("https://websvr.jbbros.com/jbapi.aspx?UserId=navgrahaausd&APIKey=CD35EDF0-9E35-4F18-8767-A9D055B6EC9A&Action=S&Shape=CUMBR,EM,FS,HRT,MQ,OV,PS,PR,RT,RD,SQEM,SQRT,TR,SQBR,RECBR,TA,CUBR,CU&CaratFrom=0.01&CaratTo=99.99&Color=D,E,F,G,H,I,J,K,L,M,N,O,D,E+,F+,G+,H+,I+,J+,K+,L+,M+,N+,O+,P+&Purity=IF,VS1,VS2,VVS1,VVS2,SI1,SI2,SI3,I1,I2,I3,IF,VS1+,VS2+,VVS1+,VVS2+,SI1+,SI2+,SI3+,I1+,I2+,I3+&Lab=GIA&PG=1");

            //        }
            //string str = "zipdfdfdsfdsfd dfdsfdsf dfdsfsdfdsfsdfdsfdsfdsfd";
            #endregion


        }


        // GET api/values/decompress
        [HttpPost("decompress{inputData}")]
        public ActionResult<string> Decompress(byte[] inputData)
        {
            byte[] resArray;
            var test = "foo bar baz very long string for example hdgfgfhfghfghfghfghfghfghfghfghf anver sadat";

            compressed = CompressBuffer(Encoding.UTF8.GetBytes(test));
            inputData = compressed;

            string resString = string.Empty;

            if (inputData == null)
            {
                throw new ArgumentNullException("inputData must be non-null");
            }

            byte[] maxibytes;
            using (var compressedMs = new MemoryStream(inputData))
            {
                using (var decompressedMs = new MemoryStream())
                {
                    using (var gzs = new BufferedStream(new GZipStream(compressedMs, CompressionMode.Decompress), bufferSize))
                    {
                        gzs.CopyTo(decompressedMs);
                    }
                    maxibytes = decompressedMs.ToArray();
                }
            }

            resString = Encoding.ASCII.GetString(maxibytes.ToArray(), 0, maxibytes.ToArray().Length);
            return resString;
        }

        // GET api/values/decompress
        [HttpGet("decompress2{inputData}")]
        public ActionResult<string> Decompress2(byte[] inputData)
        {
            string resString = string.Empty;
            if (inputData == null)
            {
                throw new ArgumentNullException("inputData must be non-null");
            }
            using (var msi = new MemoryStream(inputData))
            using (var mso = new MemoryStream())
            {
                using (var gs = new BufferedStream(new GZipStream(msi, CompressionMode.Decompress), bufferSize))
                {
                    gs.CopyTo(mso);
                }

                resString = Encoding.ASCII.GetString(mso.ToArray(), 3, mso.ToArray().Length - 3);
            }
            return resString;
        }


        public static DataTable GetCSV(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());

            DataTable dt = new DataTable();

            string currentline = string.Empty;
            bool doneHeader = false;
            while ((currentline = sr.ReadLine()) != null)
            {
                if (!doneHeader)
                {
                    foreach (string item in currentline.Split(","))
                    {
                        dt.Columns.Add(item);
                    }
                    doneHeader = true;
                    continue;
                }
                dt.Rows.Add();
                int colCount = 0;
                foreach (string item in currentline.Split(","))
                {
                    dt.Rows[dt.Rows.Count - 1][colCount] = item;
                    //List<DataRow> rows1 = dt.Rows.Cast<DataRow>().ToList();
                    colCount++;
                }
            }
            List<DataRow> rows = dt.Rows.Cast<DataRow>().ToList();
            //string results = sr.ReadToEnd();
            sr.Close();

            return dt;
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return ValueSamples.MyValue.GetValueOrDefault(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            var maxKey = ValueSamples.MyValue.Max(x => x.Key);

            ValueSamples.MyValue.Add(maxKey + 1, value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            ValueSamples.MyValue.Add(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ValueSamples.MyValue.Remove(id);
        }
    }
}