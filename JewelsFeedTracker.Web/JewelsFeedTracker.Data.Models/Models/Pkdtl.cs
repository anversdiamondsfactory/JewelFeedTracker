using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelsFeedTracker.Data.Models.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class PKTDTL
    {
        public string Loat_NO { get; set; }
        public string Status { get; set; }
        public string Shape { get; set; }
        public string Weight { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }
        public string Fluorescence { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Depth { get; set; }
        public string TotalDepth { get; set; }
        public string Table { get; set; }
        public double Discount { get; set; }
        public string Rap { get; set; }
        public double Dcaret { get; set; }
        public double NetDollar { get; set; }
        public string Lab { get; set; }
        public string CertiNo { get; set; }
        public string Inscription { get; set; }
        public string CrownAngle { get; set; }
        public string CrownHeight { get; set; }
        public string pavAngle { get; set; }
        public string PavDepth { get; set; }
        public string KeytoSymbols { get; set; }
        public string Natts { get; set; }
        public string Comment { get; set; }
        public string HNA { get; set; }
        public string EyeC { get; set; }       
        public string GirdlePer { get; set; }
        public string Girdle { get; set; }
        public string Culet { get; set; }
        public string GirdleCondition { get; set; }
        public string Location { get; set; }
        public string DIAMONDIMG_URL { get; set; }
        public string VIDEO_URL { get; set; }
    }

    public class RootPKTDTL
    {
        public List<PKTDTL> PKTDTL { get; set; }
    }


}
