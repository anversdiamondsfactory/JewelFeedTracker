using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelsFeedTracker.Data.Models.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class PRI
    {
        public string CRT { get; set; }
    }

    public class Redexim
    {
        public string PA { get; set; }

        public string StockNo { get; set; }
        public string Stock { get; set; }
        public string Shape { get; set; }
        public string Weight { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string CutGrade { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }
        public string Fluor { get; set; }
                
        public string RapPrice { get; set; }

        // Report#
        public string ReportNo { get; set; }
       
        public string Measurements { get; set; }
        public string Lab { get; set; }
        public string Comment { get; set; }
        public string DISC { get; set; }
        public PRI PRI { get; set; }
        public string Final { get; set; }
        public string AMT { get; set; }
        public string GirdleThin { get; set; }
        public string GirdleThick { get; set; }

		public string GirdlePer { get; set; }
		public string Girdle { get; set; }

		public string DepthPer { get; set; }
		public string Depth { get; set; }

		public string TablePer { get; set; }
		public string Table { get; set; }
        public string GirdleCondition { get; set; }
        public string CuletSize { get; set; }
        public string Crown { get; set; }
        public string Height { get; set; }
        public string CrownAngle { get; set; }
        public string PavilionDepth { get; set; }
        public string PavilionAngle { get; set; }
        public string KEYTOSYMBOLS { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string DiamondImage { get; set; }
        public string StarLength { get; set; }
        public string LrHalf { get; set; }
        public string Milky { get; set; }
        public string ReportIssueDate { get; set; }
        public string Shade { get; set; }
        public string NoBGM { get; set; }
        public string SA { get; set; }
        public string Video { get; set; }
        public string Link { get; set; }
        public string FIELD45 { get; set; }
        public string FIELD46 { get; set; }
        public string FIELD47 { get; set; }
        public string FIELD48 { get; set; }
        public string FIELD49 { get; set; }
        public string FIELD50 { get; set; }
        public string FIELD51 { get; set; }
        public string FIELD52 { get; set; }
    }


}
