using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelsFeedTracker.Api.Common.FeedArrayCollection
{
    public static class DfeArray
    {
        public static Dictionary<string, string> GetFluorescence()
        {
            var fluorescence_arr = new Dictionary<string, string> {

                { "Faint", "FAINT" }, { "Medium", "MEDIUM" },
                  { "MEDIUM BLUE", "MEDIUM" }, { "Medium Blue", "MEDIUM" }, { "None", "NONE" }, { "Strong", "STRONG" },
                   { "STRONG BLUE", "STRONG" },{ "STRONG Blue", "STRONG" },
                      { "Slight", "SLIGHT/ V. SLIGHT" },{ "Very Strong", "VERY STRONG" },{ "Very Slight", "SLIGHT/ V. SLIGHT" },
            };
            return fluorescence_arr;
        }
   
        public static Dictionary<string, string> GetPolish()
        {
            var polish_arr = new Dictionary<string, string> {

                { "EX", "EXCELLENT" }, { "Excellent", "EXCELLENT" },
                  { "GOOD", "GOOD" }, { "Good", "GOOD" },
                   { "Fair", "FAIR" }, { "Ideal", "IDEAL" },
                    { "Very Good", "VERY GOOD" }, { "VG", "VERY GOOD" },
            };
            return polish_arr;
        }

        public static Dictionary<string, string> GetSymmetry()
        {
            var symmetry_arr = new Dictionary<string, string> {

                { "Excellent", "EXCELLENT" }, { "Fair", "FAIR" },
                  { "Good", "GOOD" }, { "Ideal", "Ideal" },{ "Very Good", "VERY GOOD" },{ "Ver Good", "VERY GOOD" },
            };
            return symmetry_arr;
        }        

        public static Dictionary<string, string> GetCut()
        {
            var cut_arr = new Dictionary<string, string> {

                { "Excellent", "EXCELLENT" }, { "Fair", "FAIR" },
                  { "Good", "GOOD" },{ "Very Good", "VERY GOOD" },
            };
            return cut_arr;
        }
   
        public static Dictionary<string, string> GetShape()
        {
            var shape_arr = new Dictionary<string, string> {

                { "Round", "ROUND" }, { "Asscher", "ASSCHER" },
                  { "Cushion", "CUSHION" },{ "Emerald", "EMERALD" },
                    { "Heart", "HEART" },{ "Marquise", "MARQUISE" },
                      { "Oval", "OVAL" },{ "Pear", "PEAR" },
                       { "Princess", "PRINCESS" },{ "Radiant", "RADIANT" },
            };
            return shape_arr;
        }
        public static Dictionary<string, string> GetLab()
        {
            var lab_arr = new Dictionary<string, string> {

                { "IGI", "IGI" }, { "GIA", "GIA" },{ "DF", "DF" },
                  { "HRD", "HRD" },
            };
            return lab_arr;
        }
        public static string[] GetClarity()
        {
            return new string[] { "IF", "VVS1", "VVS2", "VS1", "VS2", "SI1", "SI2", "I1" };
        }
        public static string[] GetColor()
        {
            return new string[] { "D", "E", "F", "G", "H", "I", "J", "K", "L" };
        }
        public static string[] GetShapes_cond()
        {
            return new string[] { "RND", "PRN", "EMR", "CUS", "HRT", "ASC", "MQS", "OVL", "PER", "RAD", "BAG", "TRI", "OCT", "ILU" };
        }
    }
}
