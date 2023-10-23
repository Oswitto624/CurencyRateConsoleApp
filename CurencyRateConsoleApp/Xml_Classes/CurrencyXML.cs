using System.Xml.Serialization;

public class CurrencyXml
{
    [XmlAttribute("ID")]
    public string ID { get; set; } = null!;

    [XmlElement("NumCode")]
    public string NumCode { get; set; } = null!;

    [XmlElement("CharCode")]
    public string CharCode { get; set; } = null!;

    [XmlElement("Nominal")]
    public int Nominal { get; set; }

    [XmlElement("Name")]
    public string Name { get; set; } = null!;

    [XmlElement("Value")]
    public string Value { get; set; } = null!;


    [XmlElement("VunitRate")]
    public string VunitRate { get; set; } = null!;

    //public CurrencyXml() { }

    //public CurrencyXml(string id, string numCode, string charCode, int nominal, string name, string value, string vUnitRate)
    //{
    //    ID = id;
    //    NumCode = numCode;
    //    CharCode = charCode;
    //    Nominal = nominal;
    //    Name = name;
    //    Value = value;
    //    VunitRate = vUnitRate;
    //}
}