using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using System.Data.Sql;
using WebApiWithSwagger.Models;
using System.Data.SqlClient;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;
using Nancy.Json;
using JewelsFeedTracker.Data.Models.Models;
using Microsoft.AspNetCore.Hosting;
using JewelsFeedTracker.Utility.RowDataManager;
using Microsoft.Extensions.Logging;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using WebApiWithSwagger.ErrorHandler;
using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using System.Text.RegularExpressions;
using JewelsFeedTracker.Data.Access;

namespace WebApiWithSwagger.Controllers
{
    public class Details
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class demo
    {
        public string uniqID { get; set; }
        public string company { get; set; }
        public string actCode { get; set; }
        public string selectAll { get; set; }
        public string StartIndex { get; set; }
        public string Count { get; set; }
        public string columns { get; set; }
        public string finder { get; set; }
        public string sort { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]

    public class ValuesController : ControllerBase
    {

        //private readonly IFeedQueryProcessor<DfrStock> _IFeedQueryProcessor;
        public readonly string _connectionString;
        private readonly ILogger<ValuesController> _logger;
        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
            //_IFeedQueryProcessor = feedquery;
            ValueSamples.Initialize();
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Values()
        {

            //throw new Exception("Error when call empl ist");


            _logger.LogWarning("THIS IS A CUSTOM MESSAGE");

            try
            {
                _logger.LogWarning("warning msg");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return ValueSamples.MyValue.GetValueOrDefault(1);
            // }
        }
        //[Route("/errors")]
        //public IActionResult Error(
        //   [FromServices] IWebHostEnvironment webHostEnvironment)
        //{
        //    var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        //    var exceptionType = context.Error.GetType();

        //    if (exceptionType == typeof(ArgumentException)
        //        || exceptionType == typeof(ArgumentNullException)
        //        || exceptionType == typeof(ArgumentOutOfRangeException))
        //    {
        //        if (webHostEnvironment.IsDevelopment())
        //        {
        //            return ValidationProblem(
        //                context.Error.StackTrace,
        //                title: context.Error.Message);
        //        }

        //        return ValidationProblem(context.Error.Message);
        //    }

        //    if (exceptionType == typeof(NotFoundResult))
        //    {
        //        return NotFound(context.Error.Message);
        //    }

        //    if (webHostEnvironment.IsDevelopment())
        //    {
        //        return Problem(
        //            context.Error.StackTrace,
        //            title: context.Error.Message
        //            );
        //    }

        //    return Problem();
        //}
        // GET api/values/redexim
        [HttpGet("redexim")]
        public IActionResult Redexim()
        {
            ///// redexim.php
            var dataList1 = GetCSV("http://183.87.182.182:85/Certified%20Stock%20List%20!!!.csv");
            List<Redexim> lstData = ToListbyTable<Redexim>(dataList1).ToList();
            return Ok(lstData);
        }

        // GET api/values/dfe
        [HttpGet("dfe")]
        public IActionResult Dfe()
        {
            // throw new Exception("Error when call empl ist");
            ///// dfe.php DFR Model
            //List<DfrStock> list = _IFeedQueryProcessor.GetAllRecords("http://dfe.diamondsfactory.com/pd/DFR_Stock_Stone.csv");
            DataTable dt = GetCSV("http://dfe.diamondsfactory.com/pd/DFR_Stock_Stone.csv");
            //DataFormatter.SaveFileLocalFolder(dt);

            string[] path = AppDomain.CurrentDomain.BaseDirectory.Contains("bin") ? AppDomain.CurrentDomain.BaseDirectory.Split("bin") : AppDomain.CurrentDomain.BaseDirectory.Split("");
            List<DfrStock> lstdfestrock = ToListbyTable<DfrStock>(dt);
            string fileName = path[0] + "FeedArchieves\\" + "DFE_ListContent_" + System.DateTime.Now.Date.ToString("MM/dd/yyyy") + ".csv";
            DataFormatter.ExportCsv<DfrStock>(lstdfestrock, fileName);

            return Ok(lstdfestrock);
        }



        // GET api/values/sagar
        [HttpGet("sagar")]
        public IActionResult Sagar()
        {
            string jsonStr = string.Empty;
            ///// sagar.php
            List<tranStock> objList;
            using (var wc = new WebClient())
            {
                // Inventory Model
                jsonStr = wc.DownloadString("http://www.sagarenterprise.in/api/StockApi.aspx?uname=paulkumar009&pwd=UGF1bEAxMjM=-aV4BNaNOWEU=");

                objList = XMLHelper.ParseXML<tranStock>((jsonStr.Replace("\"", "'").Replace("Girdle%", "GirdlePer").Replace(@"\/", "/").Replace(@"\", "")), "Inventory");

                //XmlSerializer serializer = new XmlSerializer(typeof(List<tranStock>), new XmlRootAttribute("Inventory"));
                //StringReader stringReader = new StringReader(jsonStr);
                //List<tranStock> productList = (List<tranStock>)serializer.Deserialize(stringReader);

                // To convert JSON text contained in string json into an XML node              
                //pkdtlobj = JsonConvert.DeserializeObject<RootPKTDTL>(jsonStr.Replace("\"", "'").Replace("Girdle%", "GirdlePer").Replace(@"\/", "/").Replace(@"\", ""));
            }
            return Ok(objList);
        }
        public Object GetSerialData<T>(string sXML)
        {
            Object obj = null;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

                using (StringReader stringReader = new StringReader(sXML))
                {
                    obj = xmlSerializer.Deserialize(stringReader);
                }
            }
            catch (Exception ex) { }
            return obj;
        }

        public static DataTable BuildDataTableFromXml(string XMLString)
        {
            //var doc = XDocument.Parse("<Diamond>" + XMLString + "</Diamond>");
            //var nodeToRemove = doc.Descendants().Where(o => o.Name.LocalName == "MESSAGE");
            //nodeToRemove.Remove();
            //XMLString = doc.ToString();

            StringReader theReader = new StringReader(XMLString);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);
            DataTable Dt = theDataSet.Tables[0];
            return Dt;
        }
        // GET api/values/diamond_srd
        [HttpGet("diamond_srd")]
        public IActionResult Diamond_srd()
        {
            //// diamond_srd.php Model
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = 20000000;
            basicHttpBinding.MaxBufferSize = 20000000;
            basicHttpBinding.MaxBufferPoolSize = 20000000;
            EndpointAddress endpointAddress1 = new EndpointAddress("http://api.srd.world/webservice.asmx?WSDL");
            ServiceReference3.WebServiceSoapClient client1 = new ServiceReference3.WebServiceSoapClient(basicHttpBinding, endpointAddress1);
            string token = "7d27fddc-2603-4bf5-bfda-a781ead336d5";
            
            var XMLString = client1.GetSearchDiamond_XMLAsync("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", token).Result;
            var doc = XDocument.Parse("<Diamond>" + XMLString.InnerXml + "</Diamond>");
            var nodeToRemove = doc.Descendants().Where(o => o.Name.LocalName == "MESSAGE");
            nodeToRemove.Remove();
            string XMLString1 = doc.ToString();
            DataTable dt = BuildDataTableFromXml(XMLString1);
            return Ok(dt);
        }

        // GET api/values/jbbrother
        [HttpGet("jbbrother")]
        public IActionResult Jbbrother()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://services.diamondsfactory.com/index.php?route=api/download_stone_feed&name=jbbrother", DataFormatter.SetFeedFileName("JBBROTHER", 'R'));

            DataTable dt = DataFormatter.ToDatableByCSV("https://services.diamondsfactory.com/index.php?route=api/download_stone_feed&name=jbbrother");
            bool isSuccess = false;
            if (dt.Rows.Count > 0)
            {
                DataBaseHelper.BulkCopy(dt, "JBBROTHER");
                //DataTable JsonDataTable = (DataTable)JsonConvert.DeserializeObject("https://services.diamondsfactory.com/index.php?route=api/download_stone_feed&name=jbbrother", (typeof(DataTable)));
                
                if (isSuccess)
                {
                    //Log.Information("JB Brother feed redords " + dt.Rows.Count + " has been saved successfully.");
                }
            }
            ///// jbbrother.php
            var dataList1 = GetCSV("https://services.diamondsfactory.com/index.php?route=api/download_stone_feed&name=jbbrother");
            List<JbBrother> lstJbbrother = ToListbyTable<JbBrother>(dataList1);
            return Ok(lstJbbrother);
        }
        // GET api/values/dfe
        [HttpGet("rapnet")]
        public IActionResult Rapnet()
        {
            //You must request a ticket using HTTPS protocol
            string URLAuth = "https://technet.rapaport.com/HTTP/Authenticate.aspx";
            WebClient webClient = new WebClient();

            NameValueCollection formData = new NameValueCollection();
            formData["Username"] = "j2pd1y0c43oinlm5ctkxua9tdhzv9k";
            formData["Password"] = "aMPsd85M";
            byte[] responseBytes = webClient.UploadValues(URLAuth, "POST", formData);
            string ResultAuth = Encoding.UTF8.GetString(responseBytes);

            //After receiving an encrypted ticket, you can use it to authenticate your session.
            //Now you can choose to change the protocol to HTTP so it works faster.
            //Sample URL query - http://technet.rapaport.com/HTTP/RapLink/download.aspx?ShapeIDs=1&Programmatically=yes
            //string URL = "GENERATED_URL_QUERY";
            //WebRequest webRequest = WebRequest.Create(URL);

            //webRequest.Method = "POST";
            //webRequest.ContentType = "application/x-www-form-urlencoded";
            //webRequest.Headers.Add(System.Net.HttpRequestHeader.AcceptEncoding, "gzip");
            //Stream reqStream = webRequest.GetRequestStream();
            //string postData = "ticket=" + ResultAuth;
            //byte[] postArray = Encoding.ASCII.GetBytes(postData);
            //reqStream.Write(postArray, 0, postArray.Length);
            //reqStream.Close();

            string url = "http://technet.rapaport.com/HTTP/DLS/GetFile.aspx?ShapeIDs=1,2,3,4,9,17,7,8,11,15,16&WeightFrom=0.20&WeightTo=30.00&ColorIDs=1,2,3,4,5,6,7,8&ClarityIDs=1,2,3,4,5,6,7,8&LabIDs=1,4,5,2,10,11,34,35,38&SortBy=Owner&White=1&Fancy=1&Programmatically=yes&Version=1.0&UseCheckedCulommns=1&cCT=1&cCERT=1&cCLAR=1&cCOLR=1&cCRTCM=1&cCountry=1&cCITY=1&cCulet=1&cCuletSize=1&cCuletCondition=1&cCUT=1&cDPTH=1&cFLR=1&cGIRDLE=1&cLOTNN=1&cMEAS=1&cMeasLength=1&cMeasWidth=1&cMeasDepth=1&cPOL=1&cPX=1&cDPX=1&cTPr=1&cCashTot=1&cRapSpec=1&cRatio=1&cCashDisc=1&cCash=1&cOWNER=1&cAct=1&cNC=1&cSHP=1&cSTATE=1&cSTOCK_NO=1&cSYM=1&cTBL=1&cSTONES=1&cCertificateImage=1&cImageURL=1&cCertID=1&cAvailability=1&cFluorColor=1&cFluorIntensity=1&cDateUpdated=1&ticket=" + ResultAuth;
            // string JsonSting = webClient.DownloadString("https://services.diamondsfactory.com/index.php?route=api/download_stone_feed&name=harikrishna");
            DataTable JsonDataTable = (DataTable)JsonConvert.DeserializeObject(url, (typeof(DataTable)));
            JsonDataTable.TableName = "JSON_MAST";


            ///// rapnet.php
            var objRapnet = GetCSV("http://technet.rapaport.com/HTTP/DLS/GetFile.aspx?ShapeIDs=1,2,3,4,9,17,7,8,11,15,16&WeightFrom=0.20&WeightTo=30.00&ColorIDs=1,2,3,4,5,6,7,8&ClarityIDs=1,2,3,4,5,6,7,8&LabIDs=1,4,5,2,10,11,34,35,38&SortBy=Owner&White=1&Fancy=1&Programmatically=yes&Version=1.0&UseCheckedCulommns=1&cCT=1&cCERT=1&cCLAR=1&cCOLR=1&cCRTCM=1&cCountry=1&cCITY=1&cCulet=1&cCuletSize=1&cCuletCondition=1&cCUT=1&cDPTH=1&cFLR=1&cGIRDLE=1&cLOTNN=1&cMEAS=1&cMeasLength=1&cMeasWidth=1&cMeasDepth=1&cPOL=1&cPX=1&cDPX=1&cTPr=1&cCashTot=1&cRapSpec=1&cRatio=1&cCashDisc=1&cCash=1&cOWNER=1&cAct=1&cNC=1&cSHP=1&cSTATE=1&cSTOCK_NO=1&cSYM=1&cTBL=1&cSTONES=1&cCertificateImage=1&cImageURL=1&cCertID=1&cAvailability=1&cFluorColor=1&cFluorIntensity=1&cDateUpdated=1&ticket=" + ResultAuth);
            List<ChildRapnet> rapneList = ToListbyTable<ChildRapnet>(objRapnet);

            //using (var sqlCopy = new SqlBulkCopy(connectionstring))
            //{
            //    sqlCopy.DestinationTableName = "[Employee]";
            //    sqlCopy.BatchSize = 500;
            //    using (var reader = ObjectReader.Create(emplist, copyParameters))
            //    {
            //        sqlCopy.WriteToServer(reader);
            //    }
            //}
            using (var command = new SqlCommand(@"SELECT * FROM certificate_image WHERE cert_no != '' AND local_url != '' AND status = '1'", new SqlConnection(_connectionString)))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read()) // Don't assume we have any rows.
                    {
                        int ord = reader.GetOrdinal("col_1");
                        string data1 = reader.GetString(ord); // Handles nulls and empty strings.
                    }

                }
            }
            string query = "SELECT * FROM certificate_image WHERE cert_no != '' AND local_url != '' AND status = '1'";
            List<Array> gia_certificates = new List<Array>();
            List<Array> igi_certificates = new List<Array>();
            List<Array> certificates = new List<Array>();
            using (SqlDataAdapter sda = new SqlDataAdapter(query, new SqlConnection(_connectionString)))
            {
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);

                    foreach (DataRow item in dt.Rows)
                    {
                        string imgPath = item["ImagePath"].ToString();
                        //if (imgPath == "GIA")
                        //{
                        //$gia_certificates.Add("cert_n") = $result['local_url'];
                        //			}
                        //			else
                        //			{
                        //$igi_certificates[$result['cert_no']] = $result['local_url'];
                        //			//}
                    }
                }

                foreach (var item in rapneList)
                {

                }

                return Ok(objRapnet);
            }
        }

        // GET api/values/glowstar
        [HttpGet("glowstar")]
        public IActionResult Glowstar()
        {
            var pkdtlobj = new List<PKTDTL>();
            string jsonStr = string.Empty;
            ///// glowstar.php
            using (var wc = new WebClient())
            {
                jsonStr = wc.DownloadString("https://www.glowstaronline.com/inventory/website/navgrahaa.php?un=NavGrahaa&p=bd339db4a2fc08665267ae07989f0e04");
                pkdtlobj = JsonConvert.DeserializeObject<List<PKTDTL>>(jsonStr.Replace("\"", "'").Replace("Girdle%", "GirdlePer").Replace(@"\/", "/").Replace(@"\", ""));
            }
            return Ok(pkdtlobj);
        }
        // GET api/values/hvk
        [HttpGet("hvk")]
        public async Task<IActionResult> hvk()
        {
            XmlElement data = null;
            //// hvk.php
            //You must request a ticket using HTTPS protocol
            // string URLAuth = "http://stock.hvkonline.com/HVK_API_WebService.asmx?op=ClientLogin";

            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = 20000000;
            basicHttpBinding.MaxBufferSize = 20000000;
            basicHttpBinding.MaxBufferPoolSize = 20000000;

            EndpointAddress endpointAddress1 = new EndpointAddress("http://stock.hvkonline.com/HVK_API_WebService.asmx?WSDL");
            ServiceReference2.HVK_API_WebServiceSoapClient client = new ServiceReference2.HVK_API_WebServiceSoapClient(basicHttpBinding, endpointAddress1);
            var token = await client.ClientLoginAsync("navgrahaa", "123456");
            System.Threading.Thread.Sleep(1 * 60 * 1000);

            if (token != null)
                data = await client.GetStockAsync(token.InnerText.ToString().Replace("Row1", "HvkStock"));

            // XmlDocument origXml = new XmlDocument();
            //origXml.LoadXml("<Rows>"+ data.InnerXml + "</Rows>");//"<Row><SrNo>1</SrNo><StoneId>3896</StoneId><StoneNo>NCS-9</StoneNo><Shape>Pear</Shape><Shade>WH</Shade><Milkey>NM</Milkey><Black>NB</Black><Carat>0.46</Carat><Color>D</Color><Clarity>VVS1</Clarity><Cut></Cut><Polish>EX</Polish><Sym>VG</Sym><Flu>FNT</Flu><RapaRate>2900</RapaRate><WebsiteDiscount>-51</WebsiteDiscount><WebsiteRate>1421</WebsiteRate><WebsiteAmount>653.66</WebsiteAmount><Depthper>66.8</Depthper><Tableper>54</Tableper><Diameter>5.29</Diameter><Height>2.79</Height><Length>6.4</Length><Width>4.18</Width><CrownAng>0</CrownAng><CrownHeight>0</CrownHeight><PavAng>0</PavAng><PavDepth>0</PavDepth><LAB>GIA</LAB><VERIFY_CERT_URL>http://www.gia.edu/cs/Satellite?reportno=6295867981&amp;childpagename=GIA%2FPage%2FReportCheck&amp;pagename=GIA%2FDispatcher&amp;c=Page&amp;cid=1355954554547</VERIFY_CERT_URL><Labreportno>6295867981</Labreportno><Keytosymbols>Pinpoint, Feather</Keytosymbols><IMAGE_A_URL></IMAGE_A_URL><IMAGE_B_URL></IMAGE_B_URL><IMAGE_H_URL></IMAGE_H_URL><IMAGE_W_URL></IMAGE_W_URL><VIDEO_URL>http://www.hvkonline.com/DiamondVideos/NCS-9.html</VIDEO_URL><CERTIFICATE_URL>http://www.hvkonline.com/Certificate/NCS-9.pdf</CERTIFICATE_URL><LOCATIONCODE>INDIA</LOCATIONCODE><LOCATIONCODETOOLTIP>INDIA</LOCATIONCODETOOLTIP></Row>");

            List<Row> objList = XMLHelper.ParseXML<Row>("<Rows>" + data.InnerXml + "</Rows>", "Rows");
            return Ok(objList);
        }

        // GET api/values/harikrishna
        [HttpGet("harikrishna")]
        public IActionResult Harikrishna()
        {
            //// harikrishna.php 
            var dataList = GetCSV("https://services.diamondsfactory.com/index.php?route=api/download_stone_feed&name=harikrishna");
            List<HariStock> lstdfestrock = ToListbyTable<HariStock>(dataList);
            return Ok(dataList);
        }
        // GET api/values/akarsh
        [HttpGet("akarsh")]
        public async Task<IActionResult> Akarsh()
        {
            Regex reg = new Regex(@",(?=[^']*'([^']*'[^']*')*[^']*$)", RegexOptions.IgnoreCase);
            
            //string strVal = reg.Replace(responseString, "");

            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = 20000000;
            basicHttpBinding.MaxBufferSize = 20000000;
            basicHttpBinding.MaxBufferPoolSize = 20000000;
            /// akarsh.php
            IList<Akarsh> objList = null;
            EndpointAddress endpointAddress = new EndpointAddress("http://akarshexports.com/getfullstock.asmx?WSDL");
            ServiceReference1.GetFullStockSoapClient client1 = new ServiceReference1.GetFullStockSoapClient(basicHttpBinding, endpointAddress);
            var response = await client1.GetWebStockAsync();
           
            System.Threading.Thread.Sleep(1 / 10 * 60 * 1000);
            // string jsonString = @"{""Rows" + "\"" + ":" + response.Body.GetWebStockResult + "}";
            string jsonString = response.Body.GetWebStockResult;
            //string strVal = reg.Replace(jsonString, "");
            if (DataFormatter.IsValidJson(jsonString))
            {
                WebClient webClient = new WebClient();
                // string JsonSting = webClient.DownloadString("https://services.diamondsfactory.com/index.php?route=api/download_stone_feed&name=harikrishna");
                DataTable JsonDataTable = (DataTable)JsonConvert.DeserializeObject(jsonString, (typeof(DataTable)));
                JsonDataTable.TableName = "JSON_MAST";

                DataTable dt = DataFormatter.GetJSONToDataTable(jsonString);
                JObject o = JObject.Parse(jsonString);
                JArray a = (JArray)o["Rows"];
                objList = a.ToObject<IList<Akarsh>>();
            }
            return Ok(objList);
        }
        // GET api/values/dharmanandan
        [HttpGet("dharmanandan")]
        public IActionResult Dharmanandan()
        {
            /// dharmanandan.php
            HttpClient client = new HttpClient();
            demo product = new demo
            {
                uniqID = "21953",
                company = "Diamond Factory",
                actCode = "Diam#Fact12#78",
                selectAll = "",
                StartIndex = "",
                Count = "",
                columns = "",
                finder = "",
                sort = ""
            };
            client.BaseAddress = new Uri("http://www.dharamhk.com/dharamwebapi/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string json = JsonConvert.SerializeObject(product);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var requestUri = new Uri($"api/StockDispApi/getDiamondData", UriKind.Relative);
            var response = client.PostAsync(requestUri, httpContent);
            System.Threading.Thread.Sleep(1 * 60 * 1000);
            var jsonresult = response.Result.Content.ReadAsStringAsync();
            //HttpResponseMessage response1 = client.PostAsJsonAsync("api/StockDispApi/getDiamondData", product).Result;
            //var data = response1.Content.ReadAsStringAsync();		
            //List<Dharman> lstdfestrock = ToListbyTable<Dharman>(result.ToString());
            Dharmananandan listData = new JavaScriptSerializer().Deserialize<Dharmananandan>(jsonresult.Result);
            return Ok(listData.DataList);

        }

        // GET api/values/finestar
        [HttpGet("finestar")]
        public IActionResult Finestar()
        {
            string jsonStr = string.Empty;
            var finelistobj = new List<FineStar>();
            ///// finestar.php
            using (var wc = new WebClient())
            {
                jsonStr = wc.DownloadString("https://finestardiamonds.com/api/Stock/GetFullStockInventory?Username=karan.jhaveri@navgrahaa.com&Password=karan123&Company=NAVGRAHAAJEWELSPRIVATELIMITED");
                string modfiedString = jsonStr.Replace(@"\", @"");
                modfiedString = modfiedString.TrimStart('\"');
                modfiedString = modfiedString.TrimEnd('\"');
                modfiedString = modfiedString.Replace("\\", "");
                //DataTable dt = (DataTable)JsonConvert.DeserializeObject(modfiedString, (typeof(DataTable)));
                finelistobj = JsonConvert.DeserializeObject<List<FineStar>>(modfiedString);

                // var rootobj0 = JsonConvert.DeserializeObject<List<FineStar>>(modfiedString);

            }
            return Ok(finelistobj);
        }

        // GET api/values/compress
        [HttpGet("compress")]
        public async Task<ActionResult<string>> Compress()
        {
            //UriBuilder builder = new UriBuilder("http://www.dharamhk.com/dharamwebapi/api/StockDispApi/getDiamondData");
            //builder.Query = "uniqID='21953'&company ='Diamond Factory'&actCode= 'Diam#Fact12#78'&selectAll=''&StartIndex= ''&Count= ''&columns= ''&finder= ''&sort: ''";
            using (var client1 = new HttpClient())
            {
                //// rapnet.php
                var listrapnet = GetCSV("http://technet.rapaport.com/HTTP/DLS/GetFile.aspx?ShapeIDs=1,2,3,4,9,17,7,8,11,15,16&WeightFrom=0.20&WeightTo=30.00&ColorIDs=1,2,3,4,5,6,7,8&ClarityIDs=1,2,3,4,5,6,7,8&LabIDs=1,4,5,2,10,11,34,35,38&SortBy=Owner&White=1&Fancy=1&Programmatically=yes&Version=1.0&UseCheckedCulommns=1&cCT=1&cCERT=1&cCLAR=1&cCOLR=1&cCRTCM=1&cCountry=1&cCITY=1&cCulet=1&cCuletSize=1&cCuletCondition=1&cCUT=1&cDPTH=1&cFLR=1&cGIRDLE=1&cLOTNN=1&cMEAS=1&cMeasLength=1&cMeasWidth=1&cMeasDepth=1&cPOL=1&cPX=1&cDPX=1&cTPr=1&cCashTot=1&cRapSpec=1&cRatio=1&cCashDisc=1&cCash=1&cOWNER=1&cAct=1&cNC=1&cSHP=1&cSTATE=1&cSTOCK_NO=1&cSYM=1&cTBL=1&cSTONES=1&cCertificateImage=1&cImageURL=1&cCertID=1&cAvailability=1&cFluorColor=1&cFluorIntensity=1&cDateUpdated=1&ticket=29d282e895c041ca9c4ca3f3bff0dfc3");
            }

            ///// dinesh.php
            //EndpointAddress endpointAddress1 = new EndpointAddress("http://stock.hvkonline.com/HVK_API_WebService.asmx?WSDL");
            //ServiceReference2.HVK_API_WebServiceSoapClient client1 = new ServiceReference2.HVK_API_WebServiceSoapClient(basicHttpBinding, endpointAddress1);
            //string token = "005EF6A2D14FFA40988A2D25FEDF58580100000046C46C11EB4D7C4E25616509F2182B4C5F4913C6B6F60A95AB53DE86C889196F";
            //var xlment = await client1.GetStockAsync(token);

            //// harikrishna.php 
            var dataList = GetCSV("https://services.diamondsfactory.com/index.php?route=api/download_stone_feed&name=harikrishna");

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://sjworldapi.azurewebsites.net/share/sjapi.asmx/GetData?LoginName=nice&PassWord=nice321"))
                {
                    var response = await httpClient.SendAsync(request);
                }
            }


            using (var httpClient = new HttpClient())
            {

                var dataList0 = GetCSV("https://services.diamondsfactory.com/index.php?route=api/download_stone_feed&name=harikrishna");
                // dinesh.php
                Uri myUri = new Uri("https://websvr.jbbros.com/jbapi.aspx?UserId=navgrahaausd&APIKey=CD35EDF0-9E35-4F18-8767-A9D055B6EC9A&Action=S&Shape=CUMBR,EM,FS,HRT,MQ,OV,PS,PR,RT,RD,SQEM,SQRT,TR,SQBR,RECBR,TA,CUBR,CU&CaratFrom=0.01&CaratTo=99.99&Color=D,E,F,G,H,I,J,K,L,M,N,O,D,E+,F+,G+,H+,I+,J+,K+,L+,M+,N+,O+,P+&Purity=IF,VS1,VS2,VVS1,VVS2,SI1,SI2,SI3,I1,I2,I3,IF,VS1+,VS2+,VVS1+,VVS2+,SI1+,SI2+,SI3+,I1+,I2+,I3+&Lab=GIA&PG=1", UriKind.Absolute);

                string url = "https://websvr.jbbros.com/jbapi.aspx?UserId=navgrahaausd&APIKey=CD35EDF0-9E35-4F18-8767-A9D055B6EC9A&Action=S&Shape=CUMBR,EM,FS,HRT,MQ,OV,PS,PR,RT,RD,SQEM,SQRT,TR,SQBR,RECBR,TA,CUBR,CU&CaratFrom=0.01&CaratTo=99.99&Color=D,E,F,G,H,I,J,K,L,M,N,O,D,E+,F+,G+,H+,I+,J+,K+,L+,M+,N+,O+,P+&Purity=IF,VS1,VS2,VVS1,VVS2,SI1,SI2,SI3,I1,I2,I3,IF,VS1+,VS2+,VVS1+,VVS2+,SI1+,SI2+,SI3+,I1+,I2+,I3+&Lab=GIA&PG=1";

                XmlDocument xdoc = new XmlDocument();
                string jsonStr;
                ///// dfe.php
                var dataList1 = GetCSV("http://dfe.diamondsfactory.com/pd/DFR_Stock_Stone.csv");
                using (var wc = new WebClient())
                {
                    ///// finestar.php
                    jsonStr = wc.DownloadString("https://finestardiamonds.com/api/Stock/GetFullStockInventory?Username=karan.jhaveri@navgrahaa.com&Password=karan123&Company=NAVGRAHAAJEWELSPRIVATELIMITED");
                   
                    var rootobj = JsonConvert.DeserializeObject<FineStarRootobject>(jsonStr.Replace(@"\", ""));

                }
                ///// glowstar.php
                using (var wc = new WebClient())
                {
                    jsonStr = wc.DownloadString("https://www.glowstaronline.com/inventory/website/navgrahaa.php?un=NavGrahaa&p=bd339db4a2fc08665267ae07989f0e04");
                    var pkdtlobj = JsonConvert.DeserializeObject<RootPKTDTL>(jsonStr.Replace("\"", "'").Replace("Girdle%", "GirdlePer").Replace(@"\/", "/").Replace(@"\", ""));
                }
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(jsonStr);
                xdoc.Load("https://finestardiamonds.com/api/Stock/GetFullStockInventory?Username=karan.jhaveri@navgrahaa.com&Password=karan123&Company=NAVGRAHAAJEWELSPRIVATELIMITED");

                XmlElement root = xdoc.DocumentElement;
                XmlNodeList nodes = root.SelectNodes("/string/");
                //XmlElement elt = xdoc.SelectSingleNode("//SubMenu[@id='STE']") as XmlElement;
                XmlNodeList nodeList = xdoc.GetElementsByTagName("string");
                string Short_Fall = string.Empty;
                foreach (XmlNode node in nodeList)
                {
                    Short_Fall = node.InnerText;
                }


                //var client = new HttpClient();
                //string reqUrl = url;
                //var prodResp = await client.GetAsync(reqUrl);
                //if (!prodResp.IsSuccessStatusCode)
                //{

                //}

                //var prods = await prodResp.Content.ReadAsAsync<object>();
                //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(myUri);
                //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                //StreamReader sr = new StreamReader(resp.GetResponseStream());
                //var results = sr.ReadToEnd();
                //sr.Close();
                //using (var client = new WebClient())
                //{
                //	var content = client.DownloadData(url);
                //	using (var stream = new MemoryStream(content))
                //	{

                //	}
                //}
                //var data =	await httpClient.GetAsync(myUri);
                var request = await httpClient.GetAsync("https://websvr.jbbros.com/jbapi.aspx?UserId=navgrahaausd&APIKey=CD35EDF0-9E35-4F18-8767-A9D055B6EC9A&Action=S&Shape=CUMBR,EM,FS,HRT,MQ,OV,PS,PR,RT,RD,SQEM,SQRT,TR,SQBR,RECBR,TA,CUBR,CU&CaratFrom=0.01&CaratTo=99.99&Color=D,E,F,G,H,I,J,K,L,M,N,O,D,E+,F+,G+,H+,I+,J+,K+,L+,M+,N+,O+,P+&Purity=IF,VS1,VS2,VVS1,VVS2,SI1,SI2,SI3,I1,I2,I3,IF,VS1+,VS2+,VVS1+,VVS2+,SI1+,SI2+,SI3+,I1+,I2+,I3+&Lab=GIA&PG=1");

            }
            string str = "zipdfdfdsfdsfd dfdsfdsf dfdsfsdfdsfsdfdsfdsfdsfd";

            return "";
        }
        public static DataTable GetCSV(string url)
        {
            const Int32 BufferSize = 128;
            DataTable dt = new DataTable();
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                StreamReader sr = new StreamReader(resp.GetResponseStream());

                string currentline = string.Empty;
                bool doneHeader = false;
                //using (var streamReader = new StreamReader(resp.GetResponseStream(), Encoding.UTF8, true, BufferSize))
                //{

                //    String line;
                //    Dictionary<string, string> jsonRow = new Dictionary<string, string>();

                //    while ((line = streamReader.ReadLine()) != null)
                //    {

                //        string[] parts = line.Split(',');

                //        string key_ = parts[0];
                //        string value = parts[1];


                //        if (!jsonRow.Keys.Contains(key_))
                //        {
                //            jsonRow.Add(key_, value);
                //        }

                //    }
                //    //var json = new System.Text.Json.Serialization().Serialize(jsonRow);


                //}
                string[] splitLine = null;
                string[] splitRowLine = null;
                while ((currentline = sr.ReadLine()) != null)
                {
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
                        dt.Columns.Add("");
                        //dt.Columns.Add("");
                        continue;
                    }
                    dt.Rows.Add();
                    int colCount = 0;
                    //string[] strData = currentline.Split(",");
                    //int newColumns = strData.Length - dt.Columns.Count;
                    //if (strData.Length > dt.Columns.Count)
                    //{
                    //    for (int i = 1; i <= newColumns; i++)
                    //    {
                    //        dt.Columns.Add("");
                    //    }
                    //}
                    if (currentline.Contains(","))
                        splitRowLine = currentline.Split(",");
                    else if (currentline.Contains(";"))
                        splitRowLine = currentline.Split(";");
                    foreach (string item in splitLine)
                    {
                        dt.Rows[dt.Rows.Count - 1][colCount] = item;
                        colCount++;
                    }
                    //if (strData.Length > dt.Columns.Count)
                    //{
                    //    dt.Columns.Remove("");
                    //}
                }


                List<DataRow> rows = dt.Rows.Cast<DataRow>().ToList();
                //string results = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
                return dt;
            }
            return dt;
        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
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

        private static List<T> ToListbyTable<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToList();

            var properties = typeof(T).GetProperties();

            return dt.AsEnumerable().Select(row =>
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
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return ValueSamples.MyValue.GetValueOrDefault(id);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            ValueSamples.MyValue.Add(id, value);
        }

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
        private bool IsValidJson(string strInput)
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

    }

    public static class SerializerCache<T>
    {
        public static readonly XmlSerializer Instance = new XmlSerializer(
            typeof(List<T>), new XmlRootAttribute(typeof(T).Name + "TranStock"));
    }


    public static class XMLHelper
    {
        public static List<T> ParseXML<T>(string xmlString, string rootElement) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(rootElement));
            StringReader stringReader = new StringReader(xmlString);
            List<T> resultList = (List<T>)serializer.Deserialize(stringReader);
            return resultList;

            //XmlSerializer serializer = SerializerCache<T>.Instance;
            //MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(@this));

            //if (!string.IsNullOrEmpty(@this) && (serializer != null) && (memStream != null))
            //{
            //    return serializer.Deserialize(memStream) as List<T>;
            //}
            //else
            //{
            //    return null;
            //}
        }

    }


}


