using JewelsFeedTracker.Api.Common.FeedArrayCollection;
using JewelsFeedTracker.Data.Access;
using JewelsFeedTracker.Data.Access.QueryProcessor;
using JewelsFeedTracker.Utility;
using JewelsFeedTracker.Utility.DataManager;
using JewelsFeedTracker.Utility.RowDataManager;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TimeZoneConverter;

namespace JewelsFeedTracker.FactoryManager
{
    //  Sagar Feed RAW Data Processing Logic with saving locally, manipulting with business rules applied and execution of Bulk Copy
    // command after a well-defined format in price & pricedescription tables....
    public class SagarFactory<tranStock> : FeedFactory<tranStock>
    {
        public DataTable dtPrice1, dtPrice1_description;
        public bool isSuccess = false;

        FeedQueryProcessor iFeedQueryProcessor;
        TimeZoneInfo tzi = TZConvert.GetTimeZoneInfo("Central Standard Time");

        private static TimeZoneInfo INDIAN_ZONE = TZConvert.GetTimeZoneInfo("Asia/Kolkata"); // Get IST Zone and can be changed with zone based
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        public SagarFactory()
        {
            iFeedQueryProcessor = new FeedQueryProcessor();
        }
        public async override Task GetFeedData(string RawDataUrl) 
        {
            List<tranStock> tranStockList = new List<tranStock>();
            try
            {
                string xmlStr = string.Empty;

                using (var wc = new WebClient())
                {
                    xmlStr = wc.DownloadString(RawDataUrl);
                    string modifiedXml = xmlStr.Replace("\"", "'").Replace("Girdle%", "GirdlePer").Replace(@"\/", "/").Replace(@"\", "");
                    string fileName = DataFormatter.SetFeedFileName(FeedIdentifier.Sagar.ToString(), 'R');
                    DataTable dtTarget = XMLHelper.BuildDataTableFromXml(modifiedXml);

                    DataFormatter.SaveFileLocalFolder(dtTarget, fileName);
                    await DataBusinessRulesOnFeed(dtTarget);// Business rules execution logic on Raw Data
                     if (dtPrice1 != null && dtPrice1.Rows.Count > 0)
                        await iFeedQueryProcessor.SaveFeed(dtPrice1, FeedIdentifier.Sagar.ToString());// Bulk data processing on stone_price1 Table
                    if (dtPrice1_description != null && dtPrice1_description.Rows.Count > 0)
                        await iFeedQueryProcessor.SaveFeed(dtPrice1_description, FeedIdentifier.Sagar.ToString());// Bulk data processing on Stone_price1_description Table

                    //tranStockList = XMLHelper.ParseXML<tranStock>(modifiedXml, "Inventory");
                    //DataFormatter.ExportCsv(tranStockList, DataFormatter.SetFeedFileName(FeedIdentifier.Sagar.ToString(), 'F'));
                }
            }
            catch (Exception ex)
            {
                Log.Error("exception is occurred in " + FeedIdentifier.Sagar.ToString(), ex.ToString());
            }
        }
        private Task<bool> DataBusinessRulesOnFeed(DataTable dtTarget)
        {
            try
            {
                //DataTable dtResult;
                string[] path = AppDomain.CurrentDomain.BaseDirectory.Contains("bin") ? AppDomain.CurrentDomain.BaseDirectory.Split("bin") : AppDomain.CurrentDomain.BaseDirectory.Split("");
                if (!System.IO.Directory.Exists(path[0] + "\\stone_feed\\" + FeedIdentifier.Sagar.ToString() + "\\"))
                    System.IO.Directory.CreateDirectory(path[0] + "\\stone_feed\\" + FeedIdentifier.Sagar.ToString() + "\\");
                string file_path = path[0] + "FeedArchieves\\" + "stone_feed\\" + FeedIdentifier.Sagar.ToString() + "\\";

                var lab_array = AkarshArray.GetLab();
                var color_array = AkarshArray.GetColor();
                #region local array declaration
                ArrayList rules = new ArrayList();
                Dictionary<string, string> gia_certificates = new Dictionary<string, string>();
                Dictionary<string, string> igi_certificates = new Dictionary<string, string>();
                List<string> certificates = new List<string>();
                #endregion

                var fluorescence_arr = SagarArray.GetFluorescence();
                var polish_arr = SagarArray.GetPolish();
                var symmetry_arr = SagarArray.GetSymmetry();
                var shape_arr = SagarArray.GetShape();
                var cut_arr = SagarArray.GetCut();
                var lab_arr = SagarArray.GetLab();

                DataBaseHelper.OpenConection();
                rules = DataBaseHelper.GetArrayList("SELECT * FROM  stone_cut_rule");

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
                // Geo Zone country information
                Dictionary<string, string> countries_list = new Dictionary<string, string>();
                query = DataBaseHelper.ExecuteQueryByQuery("SELECT country_id, name, iso_code_3 FROM country").Tables[0];
                if (query.Rows.Count > 0)
                {
                    foreach (DataRow result in query.Rows)
                    {
                        countries_list.Add(result["name"].ToString(), result["iso_code_3"].ToString());
                        countries_list.Add(result["iso_code_3"].ToString(), result["country_id"].ToString());
                    }
                }
                /// collect carat range information
                // Dictionary<string, string> ranges = new Dictionary<string, string>();
                string[] ranges = new string[] { };
                query = DataBaseHelper.ExecuteQueryByQuery("SELECT stone,shape,from_carat,to_carat FROM  stone_carat_range").Tables[0];
                if (query.Rows.Count > 0)
                {
                    ranges = query.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
                }

                int stone_vendor_id = 1830;
                string get_stone_price = DataBaseHelper.DataReader("SELECT max(stone_price_id) as maxer FROM stone_price1 WHERE 1 ");
                int stone_price_id = int.Parse(get_stone_price);

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
                var shape_ignore = new string[] { "FXS", "TRA" };
                var colors_arr = RedeximArray.GetColor();

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
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][10].ToString()) && dtTarget.Rows[i][10].ToString().ToUpper() == "SGL") { dtTarget.Rows[i][10] = "HRD"; }
                        if (!(lab_array.ContainsKey(dtTarget.Rows[i][10].ToString()))) { continue; }
                        if (!color_array.Contains(dtTarget.Rows[i][3].ToString())) { continue; }

                        if ((string.IsNullOrEmpty(dtTarget.Rows[i][2].ToString()) || double.Parse(dtTarget.Rows[i][2].ToString()) < 0.20) ||
                             (!string.IsNullOrEmpty(dtTarget.Rows[i][13].ToString()) && (int.Parse(dtTarget.Rows[i][13].ToString()) <= -66)))
                        {
                            continue;
                        }
                        string stone = "DI";                      
                        if (!cut_arr.ContainsKey(dtTarget.Rows[i][5].ToString())) { dtTarget.Rows[i][5] = cut_arr[dtTarget.Rows[i][5].ToString()]; }
                        if (!shape_arr.ContainsKey(dtTarget.Rows[i][1].ToString())) { dtTarget.Rows[i][1] = shape_arr[dtTarget.Rows[i][1].ToString()]; }

                        if (!polish_arr.ContainsKey(dtTarget.Rows[i][6].ToString())) { dtTarget.Rows[i][6] = polish_arr[dtTarget.Rows[i][6].ToString()]; }
                        if (!symmetry_arr.ContainsKey(dtTarget.Rows[i][7].ToString())) { dtTarget.Rows[i][7] = symmetry_arr[dtTarget.Rows[i][7].ToString()]; }
                        if (!fluorescence_arr.ContainsKey(dtTarget.Rows[i][8].ToString())) { dtTarget.Rows[i][7] = fluorescence_arr[dtTarget.Rows[i][8].ToString()]; }

                        string shape = string.Empty;
                        if (!string.IsNullOrEmpty(mapping[dtTarget.Rows[i]["Shape"].ToString()]) && mapping[dtTarget.Rows[i]["Shape"].ToString()] == null)
                        {
                            shape = mapping[dtTarget.Rows[i]["Shape"].ToString()].ToUpper();
                            if (!string.IsNullOrEmpty(options["stone_shape"] + shape[0]) && options["stone_shape"] + shape[0] != null)
                            {
                                shape = options["stone_shape"] + shape[0];
                            }
                        }
                        else if (!string.IsNullOrEmpty(options["stone_shape"] + dtTarget.Rows[i]["Shape"].ToString().Trim()[0]) && options["stone_shape"] + dtTarget.Rows[i]["Shape"].ToString().Trim()[0] == null)
                        {
                            shape = options["stone_shape"] + dtTarget.Rows[i]["Shape"].ToString().Trim()[0];
                        }
                        else
                        {
                            continue;
                        }

                        Dictionary<string, int> crt_range = new Dictionary<string, int>();
                        string weight = dtTarget.Rows[i][2].ToString();
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][2].ToString()) && dtTarget.Rows[i][2].ToString() == null)
                        {
                            // crt_range = findCaratRange(ranges, 'DI', shape, weight);// to do 
                        }
                        else
                        {
                            crt_range["from_carat"] = 0;
                            crt_range["to_carat"] = 0;
                        }

                        string color = dtTarget.Rows[i][3].ToString();
                        string intensity = "";
                        string symmetry = string.Empty;
                        string polish = string.Empty;
                        string clarity = string.Empty;
                        if (!string.IsNullOrEmpty(mapping[dtTarget.Rows[i][4].ToString()]))
                        {
                            dtTarget.Rows[i][4] = mapping[dtTarget.Rows[i][4].ToString()];
                        }
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][4].ToString()) && !string.IsNullOrEmpty(options["stone_clarity"] + dtTarget.Rows[i][4].ToString()[0]) && options["stone_clarity"] + dtTarget.Rows[i][4].ToString()[0]== null) 
                        {
                            clarity = options["stone_clarity"] + dtTarget.Rows[i][4].ToString()[0];
                        }
                        else
                        {
                            continue;
                        }

                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][6].ToString()) && !string.IsNullOrEmpty(options["stone_polish"] + dtTarget.Rows[i][6].ToString()[0]) && options["stone_polish"] + dtTarget.Rows[i][6].ToString()[0] == null)
                        {
                            polish = options["stone_polish"] + dtTarget.Rows[i][6].ToString()[0];
                        }
                        else
                        {
                            continue;
                        }
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][7].ToString()) && !string.IsNullOrEmpty(options["stone_symmetry"] + dtTarget.Rows[i][7].ToString()[0]) && options["stone_symmetry"] + dtTarget.Rows[i][7].ToString()[0] == null)
                        {
                            symmetry = options["stone_symmetry"] + dtTarget.Rows[i][7].ToString()[0];
                        }
                        else
                        {
                            continue;
                        }

                        string cut_grade = string.Empty;

                        if (!string.IsNullOrEmpty(shape) && shape == "RND")
                        {

                            if (!string.IsNullOrEmpty(dtTarget.Rows[i][5].ToString()) && !string.IsNullOrEmpty(options["stone_cut"] + dtTarget.Rows[i][5].ToString()[0]) && options["stone_cut"] + dtTarget.Rows[i][5].ToString()[0] == null)
                            {
                                cut_grade = options["stone_cut"] + dtTarget.Rows[i][5].ToString()[0];
                            }
                            else
                            {
                                //cut_grade = findStoneCut(rules, shape, polish, symmetry, dtTarget.Rows[i][11].ToString(), dtTarget.Rows[i][12].ToString());

                                if (!string.IsNullOrEmpty(cut_grade))
                                {
                                    cut_grade = options["stone_cut"] + cut_grade.Trim()[0];
                                }
                                else
                                {
                                    cut_grade = "";
                                }
                            }

                            if (string.IsNullOrEmpty(cut_grade))
                            {
                                continue;
                            }

                        }
                        else
                        {
                            cut_grade = "";
                        }

                        string fluorescence_intensity = string.Empty;
                        if (!string.IsNullOrEmpty(dtTarget.Rows[i][8].ToString()) && !string.IsNullOrEmpty(options["stone_fluorescence"] + dtTarget.Rows[i][8].ToString()[0]) && options["stone_fluorescence"] + dtTarget.Rows[i][8].ToString()[0] == null)
                        {
                            fluorescence_intensity = options["stone_fluorescence"] + dtTarget.Rows[i][8].ToString()[0];
                        }
                        else
                        {
                            continue;
                        }

                        if (int.Parse(dtTarget.Rows[i][12].ToString()) > 0 && int.Parse(dtTarget.Rows[i][14].ToString()) > 0) { }
                        else
                        {
                            continue;
                        }
                        int country = 0;
                        string lab = dtTarget.Rows[i][10].ToString();
                        double carat_price = double.Parse(dtTarget.Rows[i][14].ToString());
                        double total_price = double.Parse(dtTarget.Rows[i][14].ToString()) * double.Parse(dtTarget.Rows[i][2].ToString());  
                        country = int.Parse(countries_list["IND"]);
                        double sprice = total_price;
                        double mprice = carat_price;
                        string fluorescence_color = string.Empty;
                        int measlength = 0;
                        int measwidth = 0;

                        int measdepth = 0;
                        int ratio = 0;
                        string measurement = dtTarget.Rows[i][9].ToString();
                        string[] meas_arr = measurement.Split("-");
                        string[] meas_arrr = meas_arr[1].Split("*");
                         measurement = meas_arr[0] + " x " + meas_arrr[0] + " x " + meas_arrr[1];


                        measlength = int.Parse(meas_arr[0]);
                        measwidth = int.Parse(meas_arrr[0]);
                        measdepth = int.Parse(meas_arrr[1]);
                        ratio = measlength / measwidth;

                        string cert = dtTarget.Rows[i][11].ToString();
                        string stock = "";
                        string available = "";
                        string depth = dtTarget.Rows[i][15].ToString();
                        string table_table = dtTarget.Rows[i][16].ToString();
                        string girdle = "";
                        string culet = "";
                        string culet_size = "";
                        string culet_condition = "";
                        string parcel_no_stone = "";
                        string cert_url = "";
                        string hascertfile = "";
                        string hasimagefile = "0";
                        string image_url = "";
                        string video_url ="";
                        string packetID = "";
                        string packetNo = ""; //stock no
                        stone_price_id++;
                        string local_image_url = string.Empty;
                        if (lab == "GIA" && !string.IsNullOrEmpty(gia_certificates[cert]) && gia_certificates[cert] == null)
                        {
                            local_image_url = gia_certificates[cert];
                        }
                        else if (!string.IsNullOrEmpty(igi_certificates[cert]) && igi_certificates[cert] == null)
                        {
                            local_image_url = igi_certificates[cert];
                        }

                        if (local_image_url.IndexOf("segoma") != -1)
                        {
                            local_image_url = "";
                        }
                        if (video_url.IndexOf(@"segoma") != -1)
                        {
                            video_url = "";
                        }
                        _drPrice1["stone_price_id"] = stone_price_id;
                        _drPrice1["stone_vendor_id"] = stone_vendor_id;
                        _drPrice1["stone"] = stone;
                        _drPrice1["shape"] = shape;
                        _drPrice1["crt_from"] = crt_range["from_carat"];
                        _drPrice1["crt_to"] = crt_range["to_carat"];
                        _drPrice1["weight"] = weight;
                        _drPrice1["color"] = color;
                        _drPrice1["intensity"] = intensity;
                        _drPrice1["clarity"] = clarity;
                        _drPrice1["cut_grade"] = cut_grade;
                        _drPrice1["polish"] = polish;
                        _drPrice1["symmetry"] = symmetry;
                        _drPrice1["fluorescence_intensity"] = fluorescence_intensity;
                        _drPrice1["lab"] = lab;
                        _drPrice1["carat_price"] = carat_price;
                        _drPrice1["total_price"] = total_price;
                        _drPrice1["country"] = country;
                        _drPrice1["sprice"] = sprice;
                        _drPrice1["mprice"] = mprice;
                        _drPrice1["mode"] = "s";
                        _drPrice1["diamond_code"] = cert;
                        _drPrice1["status"] = "1";
                        dtPrimary_stone_price1.Rows.Add(_drPrice1);

                        _drPrice1_description["stone_price_id"] = stone_price_id;
                        _drPrice1_description["fluorescence_color"] = fluorescence_color;
                        _drPrice1_description["measurement"] = measurement;
                        _drPrice1_description["measlength"] = measlength;
                        _drPrice1_description["measwidth"] = measwidth;
                        _drPrice1_description["measdepth"] = measdepth;
                        _drPrice1_description["ratio"] = ratio;
                        _drPrice1_description["cert"] = cert;
                        _drPrice1_description["stock"] = stock;
                        _drPrice1_description["available"] = available;
                        _drPrice1_description["depth"] = depth;
                        _drPrice1_description["table_table"] = table_table;
                        _drPrice1_description["girdle"] = girdle;
                        _drPrice1_description["culet"] = culet;
                        _drPrice1_description["culet_size"] = culet_size;
                        _drPrice1_description["culet_condition"] = culet_condition;
                        _drPrice1_description["parcel_no_stone"] = parcel_no_stone;
                        _drPrice1_description["cert_url"] = cert_url;
                        _drPrice1_description["hascertfile"] = hascertfile;
                        _drPrice1_description["hasimagefile"] = hasimagefile;
                        _drPrice1_description["image_url"] = local_image_url;
                        _drPrice1_description["video_url"] = video_url;
                        _drPrice1_description["packetID"] = packetID;
                        _drPrice1_description["packetNo"] = packetNo;
                        _drPrice1_description["source"] = "SAGARE"; // source
                        dtPrimary_stone_price1.Rows.Add(_drPrice1_description);
                        row++;
                    }
                    isSuccess = true;
                    dtPrice1 = dtPrimary_stone_price1;
                    dtPrice1_description = dtPrimary_stone_price1_description;
                }
            }
            catch (Exception ex)
            {
                Log.Error("error is occurred", ex.ToString());
            }

            return Task.FromResult(isSuccess);
        }
    }

}
