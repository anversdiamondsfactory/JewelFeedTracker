using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiWithSwagger.Models
{
    public class DataList
    {
        [JsonProperty("Shape")]
        public string Shape { get; set; }

        [JsonProperty("Size")]
        public double Size { get; set; }

        [JsonProperty("Color")]
        public string Color { get; set; }

        [JsonProperty("Clarity")]
        public string Clarity { get; set; }

        [JsonProperty("Cut")]
        public string Cut { get; set; }

        [JsonProperty("Polish")]
        public string Polish { get; set; }

        [JsonProperty("Sym")]
        public string Sym { get; set; }

        [JsonProperty("Flour")]
        public string Flour { get; set; }

        [JsonProperty("M1")]
        public double M1 { get; set; }

        [JsonProperty("M2")]
        public double M2 { get; set; }

        [JsonProperty("M3")]
        public double M3 { get; set; }

        [JsonProperty("Depth")]
        public double Depth { get; set; }

        [JsonProperty("Table")]
        public double Table { get; set; }

        [JsonProperty("Ref")]
        public string Ref { get; set; }

        [JsonProperty("ReportNo")]
        public string ReportNo { get; set; }

        [JsonProperty("Detail")]
        public string Detail { get; set; }

        [JsonProperty("Cert")]
        public string Cert { get; set; }

        [JsonProperty("Disc")]
        public double Disc { get; set; }

        [JsonProperty("Rate")]
        public double Rate { get; set; }

        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("CertNo")]
        public string CertNo { get; set; }

        [JsonProperty("Girdle")]
        public string Girdle { get; set; }

        [JsonProperty("Natts")]
        public string Natts { get; set; }

        [JsonProperty("TableInclusion")]
        public string TableInclusion { get; set; }

        [JsonProperty("Milky")]
        public string Milky { get; set; }

        [JsonProperty("EyeClean")]
        public string EyeClean { get; set; }

        [JsonProperty("Browness")]
        public string Browness { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("RapRate")]
        public double RapRate { get; set; }

        [JsonProperty("CertPDFURL")]
        public string CertPDFURL { get; set; }

        [JsonProperty("ImageURL")]
        public string ImageURL { get; set; }

        [JsonProperty("VideoURL")]
        public string VideoURL { get; set; }

        [JsonProperty("comment")]
        public string comment { get; set; }

        [JsonProperty("LaserInscription")]
        public bool LaserInscription { get; set; }

        [JsonProperty("PavDepth")]
        public double PavDepth { get; set; }

        [JsonProperty("CrAng")]
        public double CrAng { get; set; }

        [JsonProperty("GirdlePer")]
        public double GirdlePer { get; set; }

        [JsonProperty("PavAngle")]
        public double PavAngle { get; set; }

        [JsonProperty("GirdleCondition")]
        public string GirdleCondition { get; set; }

        [JsonProperty("StarLength")]
        public double StarLength { get; set; }

        [JsonProperty("LowerHalf")]
        public double LowerHalf { get; set; }

        [JsonProperty("ImageVideoStatus")]
        public string ImageVideoStatus { get; set; }

        [JsonProperty("Price/Carat")]
        public double PriceCarat { get; set; }
    }

    public class Dharman
    {
        [JsonProperty("IsValidUser")]
        public bool IsValidUser { get; set; }

        [JsonProperty("MessageType")]
        public int MessageType { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("TotalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("DataList")]
        public DataList[] DataList { get; set; }
    }
}
