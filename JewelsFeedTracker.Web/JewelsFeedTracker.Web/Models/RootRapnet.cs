using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiWithSwagger.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ChildRapnet
    {
        public string SellerName { get; set; }
        public string RapNetAccountID { get; set; }
        public string NameCode { get; set; }
        public string Shape { get; set; }
        public string Weight { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string FancyColor { get; set; }
        public string FancyIntensity { get; set; }
        public string Fancycolorovertones { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }
        public string FluorescenceColor { get; set; }
        public string FluorescenceIntensity { get; set; }
        public string Measurements { get; set; }
        public string MeasLength { get; set; }
        public string MeasWidth { get; set; }
        public string MeasDepth { get; set; }
        public string Ratio { get; set; }
        public string Lab { get; set; }
        public string CertificateNumber { get; set; }
        public string StockNumber { get; set; }
        public string Treatment { get; set; }
        public string PricePerCarat { get; set; }
        public string PricePercentage { get; set; }
        public string TotalPrice { get; set; }
        public string CashPricePerCarat { get; set; }
        public string CashPricePercentage { get; set; }
        public string TotalCashPrice { get; set; }
        public string Availability { get; set; }
        public string Depth { get; set; }
        public string Table { get; set; }
        public string Girdle { get; set; }
        public string Culet { get; set; }
        public string CuletSize { get; set; }
        public string CuletCondition { get; set; }
        public string Crown { get; set; }
        public string Pavilion { get; set; }
        public string CertComments { get; set; }
        public string MemberComments { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string NumberofDiamonds { get; set; }
        public string CertificateURL { get; set; }
        public string ImageURL { get; set; }
        public string RapSpec { get; set; }
        public string DateUpdated { get; set; }
        public string Milky { get; set; }
        public string Blackinclusion { get; set; }
        public string CenterInclusion { get; set; }
        public string Shade { get; set; }
        public string ClarityEnhanced { get; set; }
        public string ColorEnhanced { get; set; }
        public string HPHT { get; set; }
        public string Irradiated { get; set; }
        public string LaserDrilled { get; set; }
        public string OtherTreatment { get; set; }
        public string PavilionDepth { get; set; }
        public string PavilionAngle { get; set; }
        public string TablePercent { get; set; }
        public string Suppliercountry { get; set; }
        public string DepthPercent { get; set; }
        public string CrownAngle { get; set; }
        public string CrownHeight { get; set; }
        public string LaserInscription { get; set; }
        public string DiamondID { get; set; }
    }

    public class RootRapnet
    {
        public List<ChildRapnet> ChildRapnets { get; set; }
    }


}
