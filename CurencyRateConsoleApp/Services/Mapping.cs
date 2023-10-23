// See https://aka.ms/new-console-template for more information
public static class Mapping
{
    public static List<Currency> MapCurrency(CurrencyParentXml currencyParentXml)
    {
        var currencies = new List<Currency>();

        foreach (var currencyXml in currencyParentXml.ValuteXml)
        {
            var currency = new Currency
            {
                Id_cbr = currencyXml.ID,
                NumCode = currencyXml.NumCode,
                CharCode = currencyXml.CharCode,
                Name = currencyXml.Name,
                Nominal = currencyXml.Nominal,
            };

            var currencyRate = new CurrencyRate
            {
                Date = DateTime.Parse(currencyParentXml.Date),
                Value = double.Parse(currencyXml.Value),
                Currency = currency
            };

            currency.Rate = new List<CurrencyRate> { currencyRate };
            currencies.Add(currency);
        }

        return currencies;
    }
}
