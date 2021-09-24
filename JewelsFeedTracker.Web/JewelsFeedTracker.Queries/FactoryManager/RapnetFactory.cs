using JewelsFeedTracker.Data.Access;
using JewelsFeedTracker.Data.Access.QueryProcessor;
using JewelsFeedTracker.Data.Models.Models;
using JewelsFeedTracker.Utility;
using JewelsFeedTracker.Utility.DataManager;
using JewelsFeedTracker.Utility.RowDataManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TimeZoneConverter;

namespace JewelsFeedTracker.FactoryManager
{    //  Rapnet Feed RAW Data Processing Logic with saving locally, manipulting with business rules applied and execution of Bulk Copy
    // command after a well-defined format in price & pricedescription tables....
    public class RapnetFactory<ChildRapnet> : FeedFactory<ChildRapnet>
    {
        public DataTable dtPrice1, dtPrice1_description;
        public bool isSuccess = false;

        FeedQueryProcessor iFeedQueryProcessor;
        TimeZoneInfo tzi = TZConvert.GetTimeZoneInfo("Central Standard Time");

        private static TimeZoneInfo INDIAN_ZONE = TZConvert.GetTimeZoneInfo("Asia/Kolkata"); // Get IST Zone and can be changed with zone based
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        public RapnetFactory()
        {
            iFeedQueryProcessor = new FeedQueryProcessor();
        }
        public async override Task GetFeedData(string RawDataUrl)
        {
            List<ChildRapnet> childRapnetList = null;
            try
            {
                string URLAuth = "https://technet.rapaport.com/HTTP/Authenticate.aspx";
                WebClient webClient = new WebClient();

                NameValueCollection formData = new NameValueCollection();
                formData["Username"] = "j2pd1y0c43oinlm5ctkxua9tdhzv9k";
                formData["Password"] = "aMPsd85M";
                byte[] responseBytes = webClient.UploadValues(URLAuth, "POST", formData);
                string ResultAuth = Encoding.UTF8.GetString(responseBytes);

                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(delegate (object sender, DownloadProgressChangedEventArgs e)
                {
                    Console.WriteLine("Downloaded:" + e.ProgressPercentage.ToString());
                });
                webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler
                    (delegate (object sender, System.ComponentModel.AsyncCompletedEventArgs e)
                    {
                        if (e.Error == null && !e.Cancelled)
                        {
                            Console.WriteLine("Download completed!");
                        }
                    });
                string fileName = DataFormatter.SetFeedFileName(FeedIdentifier.Rapnet.ToString(), 'R');
                webClient.DownloadFileAsync(new Uri(RawDataUrl + ResultAuth), fileName);                
               
                DataTable dtTarget = DataFormatter.ToDatableByCSV(RawDataUrl + ResultAuth);
                DataFormatter.SaveFileLocalFolder(dtTarget, fileName);

                await DataBusinessRulesOnFeed(dtTarget);// Business rules execution logic on Raw Data

                await iFeedQueryProcessor.SaveFeed(dtPrice1, FeedIdentifier.Rapnet.ToString());// Bulk data processing on stone_price1 Table
                await iFeedQueryProcessor.SaveFeed(dtPrice1_description, FeedIdentifier.Rapnet.ToString());// Bulk data processing on Stone_price1_description Table
                //childRapnetList = DataFormatter.ToListByDataTable<ChildRapnet>(dtTarget, DataFormatter.SetFeedFileName(FeedIdentifier.Rapnet.ToString(), 'F'));

                //DataFormatter.ExportCsv(childRapnetList, DataFormatter.SetFeedFileName(FeedIdentifier.Rapnet.ToString(), 'F'));

            }
            catch (Exception ex)
            {

            }
            
        }
        private Task<bool> DataBusinessRulesOnFeed(DataTable dtTarget)
        {
            try
            {
                //DataTable dtResult;
                string[] path = AppDomain.CurrentDomain.BaseDirectory.Contains("bin") ? AppDomain.CurrentDomain.BaseDirectory.Split("bin") : AppDomain.CurrentDomain.BaseDirectory.Split("");
                if (!System.IO.Directory.Exists(path[0] + "\\stone_feed\\" + FeedIdentifier.Rapnet.ToString() + "\\"))
                    System.IO.Directory.CreateDirectory(path[0] + "\\stone_feed\\" + FeedIdentifier.Rapnet.ToString() + "\\");
                string file_path = path[0] + "FeedArchieves\\" + "stone_feed\\" + FeedIdentifier.Rapnet.ToString() + "\\";

                var lab_array = new Dictionary<string, string> {

                { "IGI", "IGI" }, { "GIA", "GIA" },{ "DF", "DF" },
                  { "HRD", "HRD" },
                };

                var color_array = new string[] { "D", "E", "F", "G", "H", "I", "J", "K", "L" };

                var fancy_color_array = new Dictionary<string, string> {

                { "Yellow", "Yellow" }, { "Yellow Yellow", "Yellow Yellow" }};

                #region local array declaration
                ArrayList rules = new ArrayList();
                Dictionary<string, string> gia_certificates = new Dictionary<string, string>();
                Dictionary<string, string> igi_certificates = new Dictionary<string, string>();
                List<string> certificates = new List<string>();
                #endregion

                DataBaseHelper.OpenConection();

                DataTable query = DataBaseHelper.ExecuteQueryByQuery("SELECT * FROM certificate_image WHERE cert_no != '' AND local_url != '' AND status = '1' ").Tables[0];
                //collect all certificates images..
                if (query.Rows.Count > 0)
                {
                    foreach (DataRow result in query.Rows)
                    {
                        if (result["lab"].ToString().ToUpper() == "GIA")
                        {
                            gia_certificates.Add(result["cert_no"].ToString(), result["local_url"].ToString());
                        }
                        else
                        {
                            igi_certificates.Add(result["cert_no"].ToString(), result["local_url"].ToString());
                        }
                    }
                }

                // collect all options - code
                Dictionary<string, string> options = new Dictionary<string, string>();
                query = DataBaseHelper.ExecuteQueryByQuery("SELECT od.custom_field as option1,ov.code,ov.option_value_id,ovd.name as value1 FROM option od LEFT JOIN option_value ov ON(od.option_id = ov.option_id) LEFT JOIN option_value_description ovd ON(ov.option_value_id = ovd.option_value_id)  WHERE ovd.language_id = '1' ORDER BY ov.sort_order").Tables[0];
                if (query.Rows.Count > 0)
                {
                    foreach (DataRow result in query.Rows)
                    {
                        options.Add(result["option1"].ToString(), result["value1"].ToString());

                        // options[result["option1"].ToString()][result["value1"].ToString().ToUpper()] = result["code"].ToString().Trim());
                    }
                    //options["stone_cut"]["Ideal"] = "EX";
                    //options("stone_polish","Ideal") = "EX";
                    //options["stone_symmetry"]["Ideal"] = "EX";
                }

                // collect rapnet mapping information
                Dictionary<string, string> mapping = new Dictionary<string, string>();
                query = DataBaseHelper.ExecuteQueryByQuery("SELECT rvm.option_value,rvm.rapnet_option_value,ro.name FROM  rapnet_value_mapping rvm LEFT JOIN rapnet_option ro ON(rvm.rapnet_option_id = ro.rapnet_option_id)").Tables[0];

                if (query.Rows.Count > 0)
                {
                    foreach (DataRow result in query.Rows)
                    {
                        var values = result["rapnet_option_value"].ToString().Split(",");
                        foreach (var value in values)
                        {
                            mapping.Add(value, result["option_value"].ToString());
                        }
                    }
                }

                // collect stone vendor information
                Dictionary<string, string> vendors = new Dictionary<string, string>();
                Dictionary<string, string> vendors_code = new Dictionary<string, string>();
                query = DataBaseHelper.ExecuteQueryByQuery("SELECT * from stone_vendor").Tables[0];
                if (query.Rows.Count > 0)
                {
                    foreach (DataRow result in query.Rows)
                    {
                        if (!string.IsNullOrEmpty(result["vendor_number"].ToString())) { }
                        //  vendors[result["vendor_number"].ToString()] = new Dictionary<string, string>()
                        //{
                        //            "stone_vendor_id" => result["stone_vendor_id"].ToString(),
                        //"status" => result["status"].ToString(),
                        //"color" => !string.IsNullOrEmpty(result["color"].ToString()) ? explode(",",$result['color']) : '',
                        //'clarity' => !empty($result['clarity']) ? explode(",",$result['clarity']) : '',
                        //'certificate' => !empty($result['certificate']) ? explode(",",$result['certificate']) : '',
                        //'carat' => !empty($result['carat']) ? explode(",",$result['carat']) : ''
                        // }
                    }
                }
                // Geo Zone country information
                Dictionary<string, string> countries_list = new Dictionary<string, string>();
                countries_list.Add("UAE", "221");
                query = DataBaseHelper.ExecuteQueryByQuery("SELECT country_id, name, iso_code_3 FROM country").Tables[0];
                if (query.Rows.Count > 0)
                {
                    foreach (DataRow result in query.Rows)
                    {
                        countries_list.Add(result["name"].ToString(), result["iso_code_3"].ToString());
                        countries_list.Add(result["iso_code_2"].ToString(), result["country_id"].ToString());
                        countries_list.Add(result["iso_code_3"].ToString(), result["country_id"].ToString());
                    }
                }
                // collect stone vendor country information
                /* $query = $db->query("SELECT vendor_country_id, name FROM ".DB_PREFIX. "vendor_country");
                      if ($query->num_rows) {
                       foreach ($query->rows as $result) {
           $countries[$result['name']] = $result['vendor_country_id'];
                       }
                   }*/

                /// collect carat range information
                // Dictionary<string, string> ranges = new Dictionary<string, string>();
                string[] ranges = new string[] { };
                query = DataBaseHelper.ExecuteQueryByQuery("SELECT stone,shape,from_carat,to_carat FROM  stone_carat_range").Tables[0];
                if (query.Rows.Count > 0)
                {
                    ranges = query.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
                }
                rules = DataBaseHelper.GetArrayList("SELECT * FROM  stone_cut_rule");
                #region get fienam of current date from the FeedArchieves folder for th feed to process 
                DirectoryInfo dInfo = new DirectoryInfo(file_path);

                List<FileInfo> files = dInfo.GetFiles("*.csv").OrderByDescending(x => x.Name).ToList(); //Getting csv files

                bool isFileExist = false;
                foreach (FileInfo file in files)
                {
                    if (file.Name.IndexOf(indianTime.Date.ToString("yyyyMMdd")) == -1)
                        isFileExist = true;
                }
                #endregion

                int linecount = dtTarget.Rows.Count; // Directory.GetFiles(filename).Length;
                DataTable dtPrimary_stone_price1 = new DataTable();
                dtPrimary_stone_price1.Clear();
                dtPrimary_stone_price1.Columns.Add("stone_price_id");
                dtPrimary_stone_price1.Columns.Add("stone_vendor_id");
                dtPrimary_stone_price1.Columns.Add("stone");
                dtPrimary_stone_price1.Columns.Add("shape");
                dtPrimary_stone_price1.Columns.Add("crt_from");
                dtPrimary_stone_price1.Columns.Add("crt_to");
                dtPrimary_stone_price1.Columns.Add("weight");
                dtPrimary_stone_price1.Columns.Add("color");
                dtPrimary_stone_price1.Columns.Add("intensity");
                dtPrimary_stone_price1.Columns.Add("clarity");
                dtPrimary_stone_price1.Columns.Add("cut_grade");
                dtPrimary_stone_price1.Columns.Add("polish");
                dtPrimary_stone_price1.Columns.Add("symmetry");
                dtPrimary_stone_price1.Columns.Add("fluorescence_intensity");
                dtPrimary_stone_price1.Columns.Add("lab");
                dtPrimary_stone_price1.Columns.Add("carat_price");
                dtPrimary_stone_price1.Columns.Add("total_price");
                dtPrimary_stone_price1.Columns.Add("country");
                dtPrimary_stone_price1.Columns.Add("sprice");
                dtPrimary_stone_price1.Columns.Add("mprice");
                dtPrimary_stone_price1.Columns.Add("mode");
                dtPrimary_stone_price1.Columns.Add("diamond_code");
                dtPrimary_stone_price1.Columns.Add("status");
                DataRow _drPrice1 = dtPrimary_stone_price1.NewRow();

                DataTable dtPrimary_stone_price1_description = new DataTable();
                dtPrimary_stone_price1_description.Clear();
                dtPrimary_stone_price1_description.Columns.Add("stone_price_id");
                dtPrimary_stone_price1_description.Columns.Add("fluorescence_color");
                dtPrimary_stone_price1_description.Columns.Add("measurement");
                dtPrimary_stone_price1_description.Columns.Add("measlength");
                dtPrimary_stone_price1_description.Columns.Add("measwidth");
                dtPrimary_stone_price1_description.Columns.Add("measdepth");
                dtPrimary_stone_price1_description.Columns.Add("ratio");
                dtPrimary_stone_price1_description.Columns.Add("cert");
                dtPrimary_stone_price1_description.Columns.Add("stock");
                dtPrimary_stone_price1_description.Columns.Add("available");
                dtPrimary_stone_price1_description.Columns.Add("table_table");
                dtPrimary_stone_price1_description.Columns.Add("girdle");
                dtPrimary_stone_price1_description.Columns.Add("culet");
                dtPrimary_stone_price1_description.Columns.Add("culet_size");
                dtPrimary_stone_price1_description.Columns.Add("culet_condition");
                dtPrimary_stone_price1_description.Columns.Add("parcel_no_stone");
                dtPrimary_stone_price1_description.Columns.Add("cert_url");
                dtPrimary_stone_price1_description.Columns.Add("hascertfile");
                dtPrimary_stone_price1_description.Columns.Add("hasimagefile");
                dtPrimary_stone_price1_description.Columns.Add("image_url");
                dtPrimary_stone_price1_description.Columns.Add("video_url");
                dtPrimary_stone_price1_description.Columns.Add("packetID");
                dtPrimary_stone_price1_description.Columns.Add("packetNo");
                dtPrimary_stone_price1_description.Columns.Add("source");

                DataRow _drPrice1_description = dtPrimary_stone_price1_description.NewRow();
                if (isFileExist && linecount > 1)
                {
                    string sql = string.Empty;
                    int row = 1;
                    int cnt = 0;

                    for (int i = 0; i < dtTarget.Rows.Count; i++)
                    {
                        if (row == 1)
                        {
                            row++;
                            continue;
                        }
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][43].ToString()))
                        {
                            if (dtTarget.Rows[i][43].ToString() == "USA")
                                dtTarget.Rows[i][43] = "United States";
                            if (dtTarget.Rows[i][43].ToString() == "France")
                                dtTarget.Rows[i][43] = "France, Metropolitan";
                        }
                        else
                        {
                            dtTarget.Rows[i][43] = "United States";
                        }
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][43].ToString()) && dtTarget.Rows[i][43].ToString() == "UAE")
                        {
                            dtTarget.Rows[i][43] = "United Arab Emirates";
                        }
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][20].ToString()) && dtTarget.Rows[i][20].ToString().ToUpper() == "SGL") { dtTarget.Rows[i][20] = "HRD"; }

                        if (string.IsNullOrEmpty(dtTarget.Rows[i][21].ToString()) || string.IsNullOrEmpty(dtTarget.Rows[i][24].ToString()) || string.IsNullOrEmpty(dtTarget.Rows[i][26].ToString()) || !(lab_array.ContainsKey(dtTarget.Rows[i][20].ToString()))) { continue; }

                        if ((string.IsNullOrEmpty(dtTarget.Rows[i][4].ToString()) || double.Parse(dtTarget.Rows[i][4].ToString()) < 0.20)
                             && int.Parse(dtTarget.Rows[i][13].ToString()) <= -0.66) { }
                        {
                            continue;
                        }
                        string stone = "DI";
                        if (!string.IsNullOrEmpty(mapping[dtTarget.Rows[i][5].ToString()]) && !string.IsNullOrEmpty(dtTarget.Rows[i][5].ToString()))
                        {
                            dtTarget.Rows[i][5] = mapping[dtTarget.Rows[i][5].ToString()].ToUpper();
                        }
                        else { continue; }
                        string shape = string.Empty;
                        if (!string.IsNullOrEmpty(mapping[dtTarget.Rows[i][3].ToString()]) && mapping[dtTarget.Rows[i][3].ToString()] == null)
                        {
                            dtTarget.Rows[i][3] = mapping[dtTarget.Rows[i][3].ToString()];
                            if (!string.IsNullOrEmpty(options["stone_shape"] + dtTarget.Rows[i][3].ToString()[0]) && options["stone_shape"] + dtTarget.Rows[i][3].ToString()[0] == null)
                            {
                                dtTarget.Rows[i][3] = options["stone_shape"] + dtTarget.Rows[i][3].ToString().Trim()[0];
                            }
                        }
                        else if (!string.IsNullOrEmpty(options["stone_shape"] + dtTarget.Rows[i][3].ToString().Trim()[0]) && options["stone_shape"] + dtTarget.Rows[i][3].ToString().Trim()[0] == null)
                        {
                            dtTarget.Rows[i][3] = options["stone_shape"] + dtTarget.Rows[i][3].ToString().Trim()[0];
                        }

                        Dictionary<string, int> crt_range = new Dictionary<string, int>();
                        // Rapnet sheet has diamonds info
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][4].ToString()) && dtTarget.Rows[i][4].ToString() == null)
                        {
                            // crt_range = findCaratRange(ranges, 'DI', dtTarget.Rows[i][3].ToString(), dtTarget.Rows[i][4].ToString());// to do 
                        }
                        else
                        {
                            crt_range["from_carat"] = 0;
                            crt_range["to_carat"] = 0;
                        }
                        //Clarity..
                        if (!string.IsNullOrEmpty(mapping[dtTarget.Rows[i][6].ToString()]))
                        {
                            dtTarget.Rows[i][6] = mapping[dtTarget.Rows[i][4].ToString()];
                        }
                        // start for vendor // 

                        if (!string.IsNullOrEmpty(vendors[dtTarget.Rows[i][1].ToString()]) && vendors[dtTarget.Rows[i][1].ToString()] == null)
                        {
                            // start for vendor //
                            //if (isset($vendors[$data[1]])) {
                            //	if ($vendors[$data[1]]['status']) {
                            //		if(!empty($vendors[$data[1]]['certificate']) && !in_array($data[20], $vendors[$data[1]]['certificate'])) {
                            //			continue;
                            //		} elseif(!empty($vendors[$data[1]]['color']) && !in_array($data[5], $vendors[$data[1]]['color'])) {
                            //			continue;
                            //		} elseif(!empty($vendors[$data[1]]['clarity']) && !in_array($data[6], $vendors[$data[1]]['clarity'])) {
                            //			continue;
                            //		} elseif(!empty($vendors[$data[1]]['carat'])) {
                            //			$carat_available = 'no';
                            //			foreach($vendors[$data[1]]['carat'] as $carating) {
                            //				list($car_from,$car_to) = explode("-",$carating);
                            //				if($data[4] >= $car_from && $data[4] <= $car_to) {
                            //					$carat_available = 'yes';
                            //				}
                            //			}
                            //			if($carat_available == 'yes'){
                            //				$data[0] = $vendors[$data[1]]['stone_vendor_id'];
                            //			} else {
                            //				continue;
                            //			}
                            //		} else {
                            //			$data[0] = $vendors[$data[1]]['stone_vendor_id'];
                            //		}
                            //	} else {
                            //		continue;
                            //	}
                            //} elseif (isset($vendors_code[$data[2]])) {
                            //	if ($vendors_code[$data[2]]['status']) {
                            //		if(!empty($vendors_code[$data[2]]['certificate']) && !in_array($data[20], $vendors_code[$data[2]]['certificate'])) {
                            //			continue;
                            //		} elseif(!empty($vendors_code[$data[2]]['color']) && !in_array($data[5], $vendors_code[$data[2]]['color'])) {
                            //			continue;
                            //		} elseif(!empty($vendors_code[$data[2]]['clarity']) && !in_array($data[6], $vendors_code[$data[2]]['clarity'])) {
                            //			continue;
                            //		} elseif(!empty($vendors_code[$data[2]]['carat'])) {
                            //			$carat_available = 'no';
                            //			foreach($vendors_code[$data[2]]['carat'] as $carating) {
                            //				list($car_from,$car_to) = explode("-",$carating);
                            //				if($data[4] >= $car_from && $data[4] <= $car_to) {
                            //					$carat_available = 'yes';
                            //				}
                            //			}
                            //			if($carat_available == 'yes'){
                            //				$data[0] = $vendors_code[$data[2]]['stone_vendor_id'];
                            //			} else {
                            //				continue;
                            //			}
                            //		} else {
                            //			$data[0] = $vendors_code[$data[2]]['stone_vendor_id'];
                            //		}
                            //	} else {
                            //		continue;
                            //	}
                            //} elseif(!empty($data[1]) || !empty($data[2])) {
                            //	if (!empty($countries_list[$data[43]])) {
                            //		$stone_vendor_country = $data[43];
                            //	} else {
                            //		$stone_vendor_country = 'Other';
                            //	}
                            //	$db->query("INSERT INTO " . DB_PREFIX . "stone_vendor SET name='" . $db->escape($data[0]) . "',"
                            //			. "vendor_number='" . $db->escape($data[1]) . "',vendor_code='" . $db->escape($data[2]) . "',"
                            //			. "city='" . $db->escape($data[41]) . "',state='" . $db->escape($data[42]) . "',"
                            //			. "country='" . $db->escape($stone_vendor_country) . "'");

                            //	$data[0] = $db->getLastId();
                            //	$vendors[$data[1]] = array(
                            //		'stone_vendor_id' => $data[0],
                            //		'status' => 1
                            //	);
                            //} else {
                            //	continue;
                            //}		//if (isset($vendors[$data[1]])) {
                            //	if ($vendors[$data[1]]['status']) {
                            //		if(!empty($vendors[$data[1]]['certificate']) && !in_array($data[20], $vendors[$data[1]]['certificate'])) {
                            //			continue;
                            //		} elseif(!empty($vendors[$data[1]]['color']) && !in_array($data[5], $vendors[$data[1]]['color'])) {
                            //			continue;
                            //		} elseif(!empty($vendors[$data[1]]['clarity']) && !in_array($data[6], $vendors[$data[1]]['clarity'])) {
                            //			continue;
                            //		} elseif(!empty($vendors[$data[1]]['carat'])) {
                            //			$carat_available = 'no';
                            //			foreach($vendors[$data[1]]['carat'] as $carating) {
                            //				list($car_from,$car_to) = explode("-",$carating);
                            //				if($data[4] >= $car_from && $data[4] <= $car_to) {
                            //					$carat_available = 'yes';
                            //				}
                            //			}
                            //			if($carat_available == 'yes'){
                            //				$data[0] = $vendors[$data[1]]['stone_vendor_id'];
                            //			} else {
                            //				continue;
                            //			}
                            //		} else {
                            //			$data[0] = $vendors[$data[1]]['stone_vendor_id'];
                            //		}
                            //	} else {
                            //		continue;
                            //	}
                            //} elseif (isset($vendors_code[$data[2]])) {
                            //	if ($vendors_code[$data[2]]['status']) {
                            //		if(!empty($vendors_code[$data[2]]['certificate']) && !in_array($data[20], $vendors_code[$data[2]]['certificate'])) {
                            //			continue;
                            //		} elseif(!empty($vendors_code[$data[2]]['color']) && !in_array($data[5], $vendors_code[$data[2]]['color'])) {
                            //			continue;
                            //		} elseif(!empty($vendors_code[$data[2]]['clarity']) && !in_array($data[6], $vendors_code[$data[2]]['clarity'])) {
                            //			continue;
                            //		} elseif(!empty($vendors_code[$data[2]]['carat'])) {
                            //			$carat_available = 'no';
                            //			foreach($vendors_code[$data[2]]['carat'] as $carating) {
                            //				list($car_from,$car_to) = explode("-",$carating);
                            //				if($data[4] >= $car_from && $data[4] <= $car_to) {
                            //					$carat_available = 'yes';
                            //				}
                            //			}
                            //			if($carat_available == 'yes'){
                            //				$data[0] = $vendors_code[$data[2]]['stone_vendor_id'];
                            //			} else {
                            //				continue;
                            //			}
                            //		} else {
                            //			$data[0] = $vendors_code[$data[2]]['stone_vendor_id'];
                            //		}
                            //	} else {
                            //		continue;
                            //	}
                            //} elseif(!empty($data[1]) || !empty($data[2])) {
                            //	if (!empty($countries_list[$data[43]])) {
                            //		$stone_vendor_country = $data[43];
                            //	} else {
                            //		$stone_vendor_country = 'Other';
                            //	}
                            //	$db->query("INSERT INTO " . DB_PREFIX . "stone_vendor SET name='" . $db->escape($data[0]) . "',"
                            //			. "vendor_number='" . $db->escape($data[1]) . "',vendor_code='" . $db->escape($data[2]) . "',"
                            //			. "city='" . $db->escape($data[41]) . "',state='" . $db->escape($data[42]) . "',"
                            //			. "country='" . $db->escape($stone_vendor_country) . "'");

                            //	$data[0] = $db->getLastId();
                            //	$vendors[$data[1]] = array(
                            //		'stone_vendor_id' => $data[0],
                            //		'status' => 1
                            //	);
                            //} else {
                            //	continue;
                            //}

                            // end for vendor //
                        }
                        string cut_grade = string.Empty;

                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][3].ToString()) && dtTarget.Rows[i][3].ToString().ToUpper() == "RND")
                        {

                            if (!string.IsNullOrEmpty(dtTarget.Rows[i][10].ToString()) && !string.IsNullOrEmpty(options["stone_cut"] + dtTarget.Rows[i][10].ToString()[0]) && options["stone_cut"] + dtTarget.Rows[i][10].ToString()[0] == null)
                            {
                                dtTarget.Rows[i][10] = options["stone_cut"] + dtTarget.Rows[i][10].ToString()[0];
                            }
                            else
                            {
                                //dtTarget.Rows[i][10] = findStoneCut(rules, shape, polish, symmetry, dtTarget.Rows[i][11].ToString(), dtTarget.Rows[i][12].ToString());

                                if (!string.IsNullOrEmpty(dtTarget.Rows[i][10].ToString()))
                                {
                                    dtTarget.Rows[i][10] = options["stone_cut"] + dtTarget.Rows[i][10].ToString().Trim()[0];
                                }
                                else
                                {
                                    dtTarget.Rows[i][10] = "";
                                }
                            }

                            if (string.IsNullOrEmpty(dtTarget.Rows[i][10].ToString()))
                            {
                                continue;
                            }
                        }
                        else
                        {
                            dtTarget.Rows[i][10] = "";
                        }

                        string fluorescence_intensity = string.Empty;
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][8].ToString()) && !string.IsNullOrEmpty(options["stone_fluorescence"] + dtTarget.Rows[i][8].ToString()[0]) && options["stone_fluorescence"] + dtTarget.Rows[i][8].ToString()[0] == null)
                        {
                            dtTarget.Rows[i][8] = options["stone_fluorescence"] + dtTarget.Rows[i][8].ToString()[0];
                        }
                        else
                        {
                            dtTarget.Rows[i][8] = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][11].ToString()) && !string.IsNullOrEmpty(options["stone_polish"] + dtTarget.Rows[i][11].ToString()[0]) && options["stone_polish"] + dtTarget.Rows[i][11].ToString()[0] == null)
                        {
                            dtTarget.Rows[i][11] = options["stone_polish"] + dtTarget.Rows[i][11].ToString()[0];
                        }
                        else
                        {
                            continue;
                        }
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][12].ToString()) && !string.IsNullOrEmpty(options["stone_symmetry"] + dtTarget.Rows[i][12].ToString()[0]) && options["stone_symmetry"] + dtTarget.Rows[i][12].ToString()[0] == null)
                        {
                            dtTarget.Rows[i][12] = options["stone_symmetry"] + dtTarget.Rows[i][12].ToString()[0];
                        }
                        else
                        {
                            continue;
                        }

                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][13].ToString()) && !string.IsNullOrEmpty(options["stone_fluorescence_color"] + dtTarget.Rows[i][13].ToString()[0]) && options["stone_fluorescence_color"] + dtTarget.Rows[i][13].ToString()[0] == null)
                        {
                            dtTarget.Rows[i][13] = options["stone_fluorescence_color"] + dtTarget.Rows[i][13].ToString()[0];
                        }
                        else
                        {
                            dtTarget.Rows[i][13] = "";
                        }
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][14].ToString()) && !string.IsNullOrEmpty(options["stone_fluorescence"] + dtTarget.Rows[i][14].ToString()[0]) && options["stone_fluorescence"] + dtTarget.Rows[i][14].ToString()[0] == null)
                        {
                            dtTarget.Rows[i][14] = options["stone_fluorescence"] + dtTarget.Rows[i][14].ToString()[0];
                        }
                        else
                        {
                            continue;
                        }
                        if (!string.IsNullOrEmpty(mapping[dtTarget.Rows[i][20].ToString()]) && mapping[dtTarget.Rows[i][20].ToString()] == null)
                        {
                            dtTarget.Rows[i][20] = mapping[dtTarget.Rows[i][20].ToString()];
                        }
                        else
                        {
                            continue;
                        }
                        string diamond_code = string.Empty;
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][21].ToString()))
                        {
                            diamond_code = dtTarget.Rows[i][21].ToString();
                        }
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][27].ToString()))
                        {
                            dtTarget.Rows[i][24] = dtTarget.Rows[i][27].ToString();
                        }
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][29].ToString()))
                        {
                            dtTarget.Rows[i][26] = dtTarget.Rows[i][29].ToString();
                        }
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][33].ToString()) && !string.IsNullOrEmpty(options["stone_girdle"] + dtTarget.Rows[i][33].ToString()[0]) && options["stone_girdle"] + dtTarget.Rows[i][33].ToString()[0] == null)
                        {
                            dtTarget.Rows[i][33] = options["stone_girdle"] + dtTarget.Rows[i][33].ToString()[0];
                        }
                        else
                            dtTarget.Rows[i][33] = "";

                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][34].ToString()) && !string.IsNullOrEmpty(options["stone_culet"] + dtTarget.Rows[i][34].ToString()[0]) && options["stone_culet"] + dtTarget.Rows[i][34].ToString()[0] == null)
                        {
                            dtTarget.Rows[i][34] = options["stone_culet"] + dtTarget.Rows[i][34].ToString()[0];
                        }
                        else
                            dtTarget.Rows[i][34] = "";
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][35].ToString()) && !string.IsNullOrEmpty(options["stone_culet_size"] + dtTarget.Rows[i][35].ToString()[0]) && options["stone_culet_size"] + dtTarget.Rows[i][35].ToString()[0] == null)
                        {
                            dtTarget.Rows[i][35] = options["stone_culet_size"] + dtTarget.Rows[i][35].ToString()[0];
                        }
                        else
                            dtTarget.Rows[i][35] = "";
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][22].ToString()) && dtTarget.Rows[i][22].ToString() == null)
                        {
                            dtTarget.Rows[i][22] = dtTarget.Rows[i][22].ToString().Replace("/[^A-Za-z0-9\\-]/", "");
                        }
                        else dtTarget.Rows[i][22] = "";

                        List<string> unavailable_country = null;
                        if (!string.IsNullOrEmpty(countries_list[dtTarget.Rows[i][43].ToString()]))
                        {
                            dtTarget.Rows[i][43] = countries_list[dtTarget.Rows[i][43].ToString()];
                        }
                        else
                        {
                            unavailable_country.Add(dtTarget.Rows[i][43].ToString());
                            dtTarget.Rows[i][43] = 259;
                        }

                        string stone_shape = dtTarget.Rows[i][3].ToString();
                        string stone_carat = dtTarget.Rows[i][4].ToString();
                        string stone_color = dtTarget.Rows[i][5].ToString();
                        string stone_clarity = dtTarget.Rows[i][6].ToString();
                        string stone_certificate = dtTarget.Rows[i][20].ToString();
                        string stone_cut = dtTarget.Rows[i][10].ToString();
                        string stone_polish = dtTarget.Rows[i][11].ToString();
                        string stone_symmetry = dtTarget.Rows[i][12].ToString();
                        string stone_fluorescence = dtTarget.Rows[i][14].ToString();

                        if (int.Parse(dtTarget.Rows[i][24].ToString()) > 0 && int.Parse(dtTarget.Rows[i][26].ToString()) > 0)
                        {

                        }
                        else continue;

                        double SPRICE = double.Parse(dtTarget.Rows[i][26].ToString());
                        double MPRICE = double.Parse(dtTarget.Rows[i][24].ToString());
                        string image_url = "";
                        string video_url = "";
                        string cert_url = dtTarget.Rows[i][45].ToString();
                        string stone_vendor_id = dtTarget.Rows[i][0].ToString();
                        string stone_vendor_no = dtTarget.Rows[i][1].ToString();
                        string cert_no = dtTarget.Rows[i][21].ToString();

                        string local_image_url = string.Empty;
                        if (stone_certificate == "GIA" && !string.IsNullOrEmpty(gia_certificates[cert_no]) && gia_certificates[cert_no] == null)
                        {
                            local_image_url = gia_certificates[cert_no];
                        }
                        else if (!string.IsNullOrEmpty(igi_certificates[cert_no]) && igi_certificates[cert_no] == null)
                        {
                            local_image_url = igi_certificates[cert_no];
                        }

                        if (local_image_url.IndexOf("segoma") != -1)
                        {
                            local_image_url = "";
                        }
                        if (video_url.IndexOf(@"segoma") != -1)
                        {
                            video_url = "";
                        }
                       
                        _drPrice1["stone_vendor_id"] = stone_vendor_id;
                        _drPrice1["stone"] = stone;
                        _drPrice1["shape"] = shape;
                        _drPrice1["crt_from"] = crt_range["from_carat"];
                        _drPrice1["crt_to"] = crt_range["to_carat"];
                        _drPrice1["weight"] = string.Empty;
                        _drPrice1["color"] = string.Empty;
                        _drPrice1["intensity"] = string.Empty;
                        _drPrice1["clarity"] = string.Empty;
                        _drPrice1["cut_grade"] = cut_grade;
                        _drPrice1["polish"] = string.Empty;
                        _drPrice1["symmetry"] = stone_symmetry;
                        _drPrice1["fluorescence_intensity"] = fluorescence_intensity;
                        _drPrice1["lab"] = string.Empty;
                        _drPrice1["carat_price"] = stone_carat;
                        _drPrice1["total_price"] = string.Empty;
                        _drPrice1["country"] = string.Empty;
                        _drPrice1["sprice"] = SPRICE;
                        _drPrice1["mprice"] = MPRICE;
                        _drPrice1["mode"] = "";
                        _drPrice1["diamond_code"] = string.Empty;
                        _drPrice1["status"] = "1";
                        dtPrimary_stone_price1.Rows.Add(_drPrice1);
                        string stone_price_id = _drPrice1["stone_vendor_id"].ToString();

                        _drPrice1_description["stone_price_id"] = stone_price_id;
                        _drPrice1_description["fluorescence_color"] = dtTarget.Rows[i][15].ToString();
                        _drPrice1_description["measurement"] = dtTarget.Rows[i][16].ToString();
                        _drPrice1_description["measlength"] = dtTarget.Rows[i][17].ToString();
                        _drPrice1_description["measwidth"] = dtTarget.Rows[i][18].ToString();
                        _drPrice1_description["measdepth"] = dtTarget.Rows[i][19].ToString();
                        _drPrice1_description["ratio"] = dtTarget.Rows[i][21].ToString();
                        _drPrice1_description["cert"] = dtTarget.Rows[i][22].ToString();
                        _drPrice1_description["stock"] = dtTarget.Rows[i][30].ToString();
                        _drPrice1_description["available"] = dtTarget.Rows[i][63].ToString();
                        _drPrice1_description["depth"] = dtTarget.Rows[i][61].ToString();
                        _drPrice1_description["table_table"] = dtTarget.Rows[i][33].ToString();
                        _drPrice1_description["girdle"] = dtTarget.Rows[i][34].ToString();
                        _drPrice1_description["culet"] = dtTarget.Rows[i][35].ToString();
                        _drPrice1_description["culet_size"] = dtTarget.Rows[i][36].ToString();
                        _drPrice1_description["culet_condition"] = dtTarget.Rows[i][44].ToString();
                        _drPrice1_description["parcel_no_stone"] = dtTarget.Rows[i][67].ToString();
                        _drPrice1_description["cert_url"] = cert_url;
                        _drPrice1_description["hascertfile"] = "";
                        _drPrice1_description["hasimagefile"] = "";
                        _drPrice1_description["image_url"] = local_image_url;
                        _drPrice1_description["video_url"] = video_url;
                        _drPrice1_description["packetID"] = "";
                        _drPrice1_description["packetNo"] = "";
                        _drPrice1_description["source"] = "SAGARE"; // source
                        dtPrimary_stone_price1.Rows.Add(_drPrice1_description);
                        row++;

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("error is occurred", ex.ToString());
            }

            return Task.FromResult(isSuccess);
        }

        private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Action<object, AsyncCompletedEventArgs> action = (sender, e) =>
            {
                if (e.Error != null)
                {
                    throw e.Error;
                }

            };

        }

    }

}
