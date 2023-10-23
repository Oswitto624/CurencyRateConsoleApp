using System.Xml.Serialization;

[XmlRoot("ValCurs")]
public class CurrencyParentXml
{
    [XmlAttribute("Date")]
    public string Date { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlElement("Valute")]
    public List<CurrencyXml> ValuteXml { get; set; }
}