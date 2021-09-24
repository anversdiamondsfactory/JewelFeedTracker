using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebApiWithSwagger.Models
{
	// using System.Xml.Serialization;
	// XmlSerializer serializer = new XmlSerializer(typeof(Row));
	// using (StringReader reader = new StringReader(xml))
	// {
	//    var test = (Row)serializer.Deserialize(reader);
	// }

	[XmlRoot(ElementName = "Row")]
	public class Row
	{
		[XmlElement(ElementName = "SrNo")]
		public string SrNo { get; set; }

		[XmlElement(ElementName = "StoneId")]
		public string StoneId { get; set; }

		[XmlElement(ElementName = "StoneNo")]
		public string StoneNo { get; set; }

		[XmlElement(ElementName = "Shape")]
		public string Shape { get; set; }

		[XmlElement(ElementName = "Shade")]
		public string Shade { get; set; }

		[XmlElement(ElementName = "Milkey")]
		public string Milkey { get; set; }

		[XmlElement(ElementName = "Black")]
		public string Black { get; set; }

		[XmlElement(ElementName = "Carat")]
		public string Carat { get; set; }

		[XmlElement(ElementName = "Color")]
		public string Color { get; set; }

		[XmlElement(ElementName = "Clarity")]
		public string Clarity { get; set; }

		[XmlElement(ElementName = "Cut")]
		public string Cut { get; set; }

		[XmlElement(ElementName = "Polish")]
		public string Polish { get; set; }

		[XmlElement(ElementName = "Sym")]
		public string Sym { get; set; }

		[XmlElement(ElementName = "Flu")]
		public string Flu { get; set; }

		[XmlElement(ElementName = "RapaRate")]
		public string RapaRate { get; set; }

		[XmlElement(ElementName = "WebsiteDiscount")]
		public string WebsiteDiscount { get; set; }

		[XmlElement(ElementName = "WebsiteRate")]
		public string WebsiteRate { get; set; }

		[XmlElement(ElementName = "WebsiteAmount")]
		public string WebsiteAmount { get; set; }

		[XmlElement(ElementName = "Depthper")]
		public string Depthper { get; set; }

		[XmlElement(ElementName = "Tableper")]
		public string Tableper { get; set; }

		[XmlElement(ElementName = "Diameter")]
		public string Diameter { get; set; }

		[XmlElement(ElementName = "Height")]
		public string Height { get; set; }

		[XmlElement(ElementName = "Length")]
		public string Length { get; set; }

		[XmlElement(ElementName = "Width")]
		public string Width { get; set; }

		[XmlElement(ElementName = "CrownAng")]
		public string CrownAng { get; set; }

		[XmlElement(ElementName = "CrownHeight")]
		public string CrownHeight { get; set; }

		[XmlElement(ElementName = "PavAng")]
		public string PavAng { get; set; }

		[XmlElement(ElementName = "PavDepth")]
		public string PavDepth { get; set; }

		[XmlElement(ElementName = "LAB")]
		public string LAB { get; set; }

		[XmlElement(ElementName = "VERIFY_CERT_URL")]
		public string VERIFYCERTURL { get; set; }

		[XmlElement(ElementName = "Labreportno")]
		public string Labreportno { get; set; }

		[XmlElement(ElementName = "Keytosymbols")]
		public string Keytosymbols { get; set; }

		[XmlElement(ElementName = "IMAGE_A_URL")]
		public string IMAGEAURL { get; set; }

		[XmlElement(ElementName = "IMAGE_B_URL")]
		public string IMAGEBURL { get; set; }

		[XmlElement(ElementName = "IMAGE_H_URL")]
		public string IMAGEHURL { get; set; }

		[XmlElement(ElementName = "IMAGE_W_URL")]
		public string IMAGEWURL { get; set; }

		[XmlElement(ElementName = "VIDEO_URL")]
		public string VIDEOURL { get; set; }

		[XmlElement(ElementName = "CERTIFICATE_URL")]
		public string CERTIFICATEURL { get; set; }

		[XmlElement(ElementName = "LOCATIONCODE")]
		public string LOCATIONCODE { get; set; }

		[XmlElement(ElementName = "LOCATIONCODETOOLTIP")]
		public string LOCATIONCODETOOLTIP { get; set; }
	}

}
