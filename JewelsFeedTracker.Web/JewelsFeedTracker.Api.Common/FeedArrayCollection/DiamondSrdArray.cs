using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelsFeedTracker.Api.Common.FeedArrayCollection
{
    public static class DiamondSrdArray
    {
        public static Dictionary<string, string> GetFluorescence()
        {
            var fluorescence_arr = new Dictionary<string, string> {

                { "Faint", "FAINT" }, { "Medium", "MEDIUM" },
                  { "None", "NONE" }, { "Strong", "STRONG" }, { "Very Slight", "SLIGHT/ V. SLIGHT" },
                      { "Very Strong", "VERY STRONG" },
            };
            return fluorescence_arr;
        }

        public static Dictionary<string, string> GetPolish()
        {
            var polish_arr = new Dictionary<string, string> {

                { "Excellent", "EXCELLENT" }, { "Good", "GOOD" }, { "Very Good", "VERY GOOD" }
            };
            return polish_arr;
        }
        public static Dictionary<string, string> GetSymmetry()
        {
            var symmetry_arr = new Dictionary<string, string> {

                 { "Excellent", "EXCELLENT" }, { "Fair", "FAIR" }, { "Good", "GOOD" }, { "Very Good", "VERY GOOD" }
            };
            return symmetry_arr;
        }

        public static Dictionary<string, string> GetShape()
        {
            var shape_arr = new Dictionary<string, string> {

                { "Cushion", "CUSHION" }, { "Emerald", "EMERALD" },
                  { "Heart", "HEART" }, { "L Radiant", "RADIANT" }, { "Marquise", "MARQUISE" }, { "Oval", "OVAL" }, { "Pear", "PEAR" },
                  { "Princess", "PRINCESS" },{ "Round", "ROUND" }
            };
            return shape_arr;
        }

        public static Dictionary<string, string> GetCut()
        {
            var cut_arr = new Dictionary<string, string> {

                { "Excellent", "EXCELLENT" }, { "Fair", "FAIR" },
                  { "Good", "GOOD" },{ "Very Good", "VERY GOOD" },
            };
            return cut_arr;
        }
        
        public static Dictionary<string, string> GetLab()
        {
            var labs_arr = new Dictionary<string, string> {

                { "IGI", "IGI" }, { "GIA", "GIA" },
                  { "HRD", "HRD" },
            };
            return labs_arr;
        }
        public static string[] GetColor()
        {
            return new string[] { "D", "E", "F", "G", "H", "I", "J", "K", "L" };
        }
    }
}
