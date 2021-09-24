using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelsFeedTracker.Api.Common.FeedArrayCollection
{
    public static class HrikrishnaArray
    {
        public static Dictionary<string, string> GetFluorescence()
        {
            var fluorescence_arr = new Dictionary<string, string> {

                { "F", "FAINT" }, { "M", "MEDIUM" },
                  { "N", "NONE" }, { "SL", "SLIGHT" }, { "ST", "STRONG" },
                      { "VS", "VERY STRONG" },{ "VSL", "VERY SLIGHT" },
            };
            return fluorescence_arr;
        }

        public static Dictionary<string, string> GetPolish()
        {
            var polish_arr = new Dictionary<string, string> {

                { "EX", "EXCELLENT" }, { "G", "GOOD" },
                  { "GD", "GOOD" }, { "VG", "VERY GOOD" },{ "F", "FAIR" }
            };
            return polish_arr;
        }
        public static Dictionary<string, string> GetSymmetry()
        {
            var symmetry_arr = new Dictionary<string, string> {

                { "EX", "EXCELLENT" }, { "FR", "FAIR" },
                  { "G", "GOOD" }, { "GD", "GOOD" },{ "VG", "VERY GOOD" },
            };
            return symmetry_arr;
        }

        public static Dictionary<string, string> GetCut()
        {
            var cut_arr = new Dictionary<string, string> {

                { "EX", "EXCELLENT" }, { "F", "FAIR" },
                  { "G", "GOOD" },{ "VG", "VERY GOOD" },
            };
            return cut_arr;
        }

        public static Dictionary<string, string> GetShape()
        {
            var shape_arr = new Dictionary<string, string> {

                { "RMB", "RMB" }, { "TRIANGULAR", "TRIANGULAR" },
                  { "PEAR MIXED CUT", "PEAR MIXED CUT" },
            };
            return shape_arr;
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
        public static Dictionary<string, string> GetLocation()
        {
            var location_arr = new Dictionary<string, string> {

                { "TRANSIT", "TRANSIT" }, { "TRIP", "TRIP" },
                  { "UPCOMING", "UPCOMING" },
            };
            return location_arr;
        }
        public static Dictionary<string, string> GetHarikrishnaShape()
        {
            var labs_arr = new Dictionary<string, string> {

                { "CUSHION MBR", "CUSHION MODIFIED" },
            };
            return labs_arr;
        }
    }
}
