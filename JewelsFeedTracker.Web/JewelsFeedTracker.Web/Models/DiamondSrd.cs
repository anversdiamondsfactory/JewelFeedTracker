using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiWithSwagger.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Message
    {
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public string AlertTypeCode { get; set; }
    }

    public class Table
    {
        public string STOCK_ID { get; set; }
        public string StockNo { get; set; }
        public string PartyStockNo { get; set; }
        public string Shape { get; set; }
        public double Carat { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Pol { get; set; }
        public string Sym { get; set; }
        public string Fluo { get; set; }
        public string Milky { get; set; }
        public string Brown { get; set; }
        public string EyeClean { get; set; }
        public string Lab { get; set; }
        public string CertiNo { get; set; }
        public double DepthPer { get; set; }
        public double TablePer { get; set; }
        public double Rap { get; set; }
        public double RapAmount { get; set; }
        public double Discount { get; set; }
        public double PricePerCarat { get; set; }
        public double Amount { get; set; }
        public string Location { get; set; }
        public bool ISFancy { get; set; }
        public string FancyColor { get; set; }
        public string FancyColorIntensity { get; set; }
        public string FancyColorOvertone { get; set; }
        public string BlackTable { get; set; }
        public string BlackCrown { get; set; }
        public string WhiteTable { get; set; }
        public string WhiteCrown { get; set; }
        public string TableOpen { get; set; }
        public string CrownOpen { get; set; }
        public string PavOpen { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double TablePer1 { get; set; }
        public double DepthPer1 { get; set; }
        public string Measurement { get; set; }
        public double CrAngle { get; set; }
        public double CrHeight { get; set; }
        public double PavAngle { get; set; }
        public double PavHeight { get; set; }
        public double GirdlePer { get; set; }
        public string GirdleDesc { get; set; }
        public string GirdleCondition { get; set; }
        public string KeyToSymbol { get; set; }
        public string Status { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public string CertiUrl { get; set; }
    }

    public class Root
    {
        public List<Message> Messages { get; set; }
        public List<Table> Tables { get; set; }
    }


}
