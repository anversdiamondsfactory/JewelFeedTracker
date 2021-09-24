using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelsFeedTracker.Data.Models.Models
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
        public string Carat { get; set; }
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
        public string DepthPer { get; set; }
        public string TablePer { get; set; }
        public string Rap { get; set; }
        public string RapAmount { get; set; }
        public string Discount { get; set; }
        public string PricePerCarat { get; set; }
        public string Amount { get; set; }
        public string Location { get; set; }
        public string ISFancy { get; set; }
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
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string TablePer1 { get; set; }
        public string DepthPer1 { get; set; }
        public string Measurement { get; set; }
        public string CrAngle { get; set; }
        public string CrHeight { get; set; }
        public string PavAngle { get; set; }
        public string PavHeight { get; set; }
        public string GirdlePer { get; set; }
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
