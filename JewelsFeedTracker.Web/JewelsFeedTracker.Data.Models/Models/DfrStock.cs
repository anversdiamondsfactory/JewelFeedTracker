using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelsFeedTracker.Data.Models.Models
{
	public class DfrStock
    {
        public string instock_option_id { get; set; }
        public string tag_no { get; set; }
        public string RawMaterialTypeNm { get; set; }
        public string CT { get; set; }
        public string ShapeNm { get; set; }
        public string ClarityNm { get; set; }
        public string ColourNm { get; set; }
        public string Fluorescence { get; set; }
        public string GirdleNm { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }
        public string ProductCertificate { get; set; }
        public object CertificateNO { get; set; }
        public string InventoryBy { get; set; }
        public string PerCtGBP { get; set; }
        public string CTPriceGBP { get; set; }
    }

}
