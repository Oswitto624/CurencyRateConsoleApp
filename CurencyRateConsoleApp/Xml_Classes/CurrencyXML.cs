using System.Xml.Serialization;

public class CurrencyXML
{
    [XmlAttribute("ID")]
    public string ID { get; set; }

    [XmlElement("NumCode")]
    public string NumCode { get; set; }

    [XmlElement("CharCode")]
    public string CharCode { get; set; }

    [XmlElement("Nominal")]
    public int Nominal { get; set; }

    [XmlElement("Name")]
    public string Name { get; set; }

    [XmlElement("Value")]
    public string Value { get; set; }


    [XmlElement("VunitRate")]
    public string VunitRate { get; set; }

    public CurrencyXML() { }

    public CurrencyXML(string id, string numCode, string charCode, int nominal, string name, string value, string vUnitRate)
    {
        ID = id;
        NumCode = numCode;
        CharCode = charCode;
        Nominal = nominal;
        Name = name;
        Value = value;
        VunitRate = vUnitRate;
    }
}