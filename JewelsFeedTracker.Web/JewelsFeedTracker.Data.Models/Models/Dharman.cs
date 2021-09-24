using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelsFeedTracker.Data.Models.Models
{
    public class DataList
    {
       
        public string Shape { get; set; }

      
        public double Size { get; set; }

       
        public string Color { get; set; }

       
        public string Clarity { get; set; }

       
        public string Cut { get; set; }

       
        public string Polish { get; set; }

       
        public string Sym { get; set; }

       
        public string Flour { get; set; }

       
        public double M1 { get; set; }

        
        public double M2 { get; set; }

        
        public double M3 { get; set; }

        
        public double Depth { get; set; }

        
        public double Table { get; set; }

        
        public string Ref { get; set; }

      
        public string ReportNo { get; set; }

       
        public string Detail { get; set; }

       
        public string Cert { get; set; }

        
        public double Disc { get; set; }

        
        public double Rate { get; set; }

        
        public string Location { get; set; }

        
        public string CertNo { get; set; }

        
        public string Girdle { get; set; }

       
        public string Natts { get; set; }

       
        public string TableInclusion { get; set; }

       
        public string Milky { get; set; }

        
        public string EyeClean { get; set; }

        
        public string Browness { get; set; }

        
        public string Status { get; set; }

       
        public double RapRate { get; set; }

       
        public string CertPDFURL { get; set; }

        
        public string ImageURL { get; set; }

        
        public string VideoURL { get; set; }

        public string comment { get; set; }

       
        public bool LaserInscription { get; set; }

       
        public double PavDepth { get; set; }

        
        public double CrAng { get; set; }

        
        public double GirdlePer { get; set; }

        
        public double PavAngle { get; set; }

       
        public string GirdleCondition { get; set; }

       
        public double StarLength { get; set; }

        
        public double LowerHalf { get; set; }

       
        public string ImageVideoStatus { get; set; }

        
        public double PriceCarat { get; set; }
    }

    public class Dharmananandan
    {
       
        public bool IsValidUser { get; set; }

       
        public int MessageType { get; set; }

        
        public string Message { get; set; }

        
        public int TotalCount { get; set; }

       
        public List<DataList> DataList { get; set; }
    }
    public class DharmanDemo
    {
        public string uniqID { get; set; }
        public string company { get; set; }
        public string actCode { get; set; }
        public string selectAll { get; set; }
        public string StartIndex { get; set; }
        public string Count { get; set; }
        public string columns { get; set; }
        public string finder { get; set; }
        public string sort { get; set; }
    }
}
