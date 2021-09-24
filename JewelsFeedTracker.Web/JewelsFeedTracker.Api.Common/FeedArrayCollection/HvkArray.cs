using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelsFeedTracker.Api.Common.FeedArrayCollection
{
    public static class HvkArray
    {
        public static Dictionary<string, string> GetFluorescence()
        {
            var fluorescence_arr = new Dictionary<string, string> {

                { "FNT", "FAINT" }, { "MED", "MEDIUM" },
                  { "NONE", "NONE" }, { "STG", "STRONG" }, { "VSL", "SLIGHT/ V. SLIGHT" },
                      { "VST", "VERY STRONG" }
            };
            return fluorescence_arr;
        }

        public static Dictionary<string, string> GetSymmetry()
        {
            var symmetry_arr = new Dictionary<string, string> {

                { "EX", "EXCELLENT" }, { "FR", "FAIR" },
                  { "GD", "GOOD" },{ "VG", "VERY GOOD" },
            };
            return symmetry_arr;
        }

        public static Dictionary<string, string> GetPolish()
        {
            var polish_arr = new Dictionary<string, string> {

                { "EX", "EXCELLENT" }, { "GD", "GOOD" }, { "VG", "VERY GOOD" }
            };
            return polish_arr;
        }

        public static Dictionary<string, string> GetShape()
        {
            var shape_arr = new Dictionary<string, string> {

                { "RD", "RADIANT" }, { "HEART", "HEART" },
                  { "Cushion", "CUSHION" }, { "EM", "CUSHION" }, { "Oval", "OVAL" }, { "Pear", "PEAR" }, { "Princess", "PRINCESS" },
               
            };
            return shape_arr;
        }
        public static Dictionary<string, string> GetCut()
        {
            var cut_arr = new Dictionary<string, string> {

                { "EX", "EXCELLENT" }, { "FR", "FAIR" },
                  { "GD", "GOOD" },{ "VG", "VERY GOOD" },
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
