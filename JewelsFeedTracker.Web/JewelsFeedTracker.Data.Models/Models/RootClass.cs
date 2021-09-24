using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelsFeedTracker.Data.Models.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class FineStarRootobject
    {
        public List<FineStar> FineStars { get; set; }
    }
    public class FineStar
    {
        public string REPORT_NO { get; set; }
        public string PACKET_NO { get; set; }
        public string SHAPE { get; set; }
        public string CTS { get; set; }
        public string COLOR { get; set; }
        public string CUT { get; set; }
        public string POLISH { get; set; }
        public string SYMM { get; set; }
        public string FLS { get; set; }
        public string PURITY { get; set; }
        public string LAB { get; set; }
        public string LENGTH_1 { get; set; }
        public string WIDTH { get; set; }
        public string DEPTH { get; set; }
        public string TABLE_PER { get; set; }
        public string DEPTH_PER { get; set; }
        public string CROWN_ANGLE { get; set; }
        public string CROWN_HEIGHT { get; set; }
        public string PAV_ANGLE { get; set; }
        public string PAV_HEIGHT { get; set; }
        public string SIDE_NATTS { get; set; }
        public string CROWN_OPEN { get; set; }
        public string REPORT_COMMENT { get; set; }
        public string KEY_TO_SYMBOLS { get; set; }
        public string DISC_PER { get; set; }
        public string RAP_PRICE { get; set; }
        public string NET_RATE { get; set; }
        public string NET_VALUE { get; set; }
        public string HA { get; set; }
        public string BRILLIANCY { get; set; }
        public string SHADE { get; set; }
        public string CULET { get; set; }
        public string GIRDLE { get; set; }
        public string CERTI_LINK { get; set; }
        public string COMMENTS { get; set; }
        public string CENTER_NATTS { get; set; }
        public string SIDE_FEATHER { get; set; }
        public string CENTER_FEATHER { get; set; }
        public string EYE_CLEAN { get; set; }
        public string MEASUREMENT { get; set; }
        public string AVG_DIA { get; set; }
        public string DNA { get; set; }
        public string REAL_IMAGE { get; set; }
        public string HEART_IMAGE { get; set; }
        public string ARROW_IMAGE { get; set; }
        public string PLOTTING_IMAGE { get; set; }
        public string VIDEO { get; set; }
        public string CERTI_IMAGE { get; set; }
        public string LOCATION { get; set; }
        public string B2B_MP4 { get; set; }
        public string B2C_MP4 { get; set; }
    }


}
