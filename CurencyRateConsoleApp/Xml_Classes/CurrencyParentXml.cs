using System.Xml.Serialization;

[Serializable]
public class CurrencyParentXml
{
    [XmlAttribute("Date")]
    public string Date { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlElement("Valute")]
    public List<CurrencyXML> CurrencyXMLs { get; set; }
}