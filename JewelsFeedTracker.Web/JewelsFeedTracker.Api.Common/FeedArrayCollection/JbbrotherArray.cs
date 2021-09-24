using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelsFeedTracker.Api.Common.FeedArrayCollection
{
    public static class JbbrotherArray
    {
        public static Dictionary<string, string> GetFluorescence()
        {
            var fluorescence_arr = new Dictionary<string, string> {

                { "F", "FFA" }, { "M", "FMD" },
                  { "N", "FNO" }, { "S", "FST" }, { "VS", "FVST" },                    
            };
            return fluorescence_arr;
        }

        public static Dictionary<string, string> GetPolish()
        {
            var polish_arr = new Dictionary<string, string> {

                { "EX", "EX" }, { "GD", "GD" }, { "VG", "VG" }, { "FR", "FR" }
            };
            return polish_arr;
        }
        public static Dictionary<string, string> GetSymmetry()
        {
            var symmetry_arr = new Dictionary<string, string> {

                { "EX", "EXCELLENT" }, { "FR", "FAIR" },
                  { "GD", "GOOD" },{ "VG", "VERY GOOD" },
            };
            return symmetry_arr;
        }
        public static Dictionary<string, string> GetShape()
        {
            var shape_arr = new Dictionary<string, string> {

                { "EM", "EMERALD" }, { "FS", "FANCY SHAPE" },
                  { "MS", "MARQUISE" }, { "OV", "OVAL" }, { "PS", "PEAR" }, { "PR", "PRINCESS" }, { "RT", "RADIANT" },
                  { "RD", "ROUND" },{ "SQEM", "SQUARE EMERALD" },{ "SQRT", "SQUARE RADIANT" },{ "TR", "TRILLIANT" },
                  { "SQBR", "SQUARE BRILLIANT" },{ "RECBR", "RECTANGLE BRILLIANT" },{ "TA", "TRIANGLE" },{ "ST", "STEP-TRIANGLE CUT" },{ "CUBR", "CUSHION BRILLIAN" },
                  { "CU", "CUSHION" },
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
