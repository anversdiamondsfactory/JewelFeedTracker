using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiWithSwagger.Models
{
	// using System.Xml.Serialization;
	// XmlSerializer serializer = new XmlSerializer(typeof(Inventory));
	// using (StringReader reader = new StringReader(xml))
	// {
	//    var test = (Inventory)serializer.Deserialize(reader);
	// }

	public class tranStock
	{
		public string RefNo { get; set; }

		public string Availability { get; set; }

		public string Weight { get; set; }

		public string Colorcode { get; set; }

		public string ClarityName { get; set; }

		public string PolishName { get; set; }

		public string SymName { get; set; }
		public string FLName { get; set; }
		public string FLColor { get; set; }
		public string Diameter { get; set; }
		public string CertName { get; set; }
		public string CertNo { get; set; }
		public string Treatment { get; set; }
		public string RapRate { get; set; }
		public string RapDown { get; set; }
		public string Rate { get; set; }
		public string CashPriceDiscount { get; set; }
		public string RapPrice { get; set; }
		public string FancyColor { get; set; }
		public string FancyColorstringensity { get; set; }
		public string FancyColorOvertone { get; set; }
		public string TotDepth { get; set; }
		public string Table { get; set; }
		public string GirdleMinMs { get; set; }
		public string GirdleMaxMs { get; set; }
		public string GirdlePer { get; set; }
		public string GirdleCondition { get; set; }
		public string Culet { get; set; }
		public string CuletCondition { get; set; }
		public string CH { get; set; }
		public string CA { get; set; }
		public string PavilionPer { get; set; }
		public string PA { get; set; }
		public string LaserInscription { get; set; }
		public string Comments { get; set; }
		public string Location { get; set; }
		public string State { get; set; }
		public string City { get; set; }
		public string IsMatchedPairSeparable { get; set; }
		public string PairStock { get; set; }
		public string AllowRapLinkFeed { get; set; }
		public string ParcelStones { get; set; }
		public string CertificateFilename { get; set; }
		public string DiamondImage { get; set; }
		public string File { get; set; }
		public string TradeShow { get; set; }
		public string KeyToSymbols { get; set; }
		public string ColorShade { get; set; }
		public string Strln { get; set; }
		public string CenterInclusion { get; set; }
		public string Brown { get; set; }
		public string Green { get; set; }
		public string Milky { get; set; }
		public string Black { get; set; }
		public string ReportIssueDate { get; set; }
		public string ReportType { get; set; }
		public string LABLocation { get; set; }
		public string Brand { get; set; }
		public string CutName { get; set; }
		public string PropCode { get; set; }
	}

	public class Inventory
	{
		public List<tranStock> TranStock { get; set; }
	}


}
