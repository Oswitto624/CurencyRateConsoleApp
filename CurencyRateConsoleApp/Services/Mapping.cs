// See https://aka.ms/new-console-template for more information
public static class Mapping
{
    public static Currency CurrencyMap(CurrencyXml currencyXml)
    {
        return new Currency
        {
            Id_cbr = currencyXml.ID,
            NumCode = currencyXml.NumCode,
            CharCode = currencyXml.CharCode,
            Name = currencyXml.Name,
            Nominal = currencyXml.Nominal,
        };
    }

    public static List<CurrencyRate> MapCurrencyRate(CurrencyParentXml currencyParentXml)
    {
        var currencyRates = new List<CurrencyRate>();

        foreach (var currencyXml in currencyParentXml.CurrencyXMLs)
        {
            currencyRates.Add(new CurrencyRate
            {
                Date = DateTime.Parse(currencyParentXml.Date),
                Value = double.Parse(currencyXml.Value),
                Currency = CurrencyMap(currencyXml)
            });
        }

        return currencyRates;
    }
}
