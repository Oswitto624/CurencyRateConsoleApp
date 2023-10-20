using System.Data.SqlTypes;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

public class CurrencyService
{
    private readonly HttpClient _httpClient;

    public CurrencyService()
    {
        _httpClient = new HttpClient();
    }
    /// <summary>
    /// Получение ответа от сервиса ЦБР
    /// </summary>
    /// <param name="date">Дата, на которую нужны курсы валют</param>
    /// <returns>Строка с ответом от ЦБР</returns>
    public async Task<string> GetCurrencyRates(DateTime date)
    {
        var url = $"https://cbr.ru/scripts/XML_daily.asp?date_req={date.Day}/{date.Month}/{date.Year}";
               
        var response = await _httpClient.GetAsync(url);

        string responseXmlString = string.Empty;

        if (response.IsSuccessStatusCode) responseXmlString = await response.Content.ReadAsStringAsync();
        else Console.WriteLine($"Ошибка в запросе. Код ошибки: {response.StatusCode}");

        return responseXmlString;
    }

    /// <summary>
    /// Десериализация XML от ЦБР, используя XmlSerializer
    /// </summary>
    /// <param name="xmlString">Строка с ответом от сервиса ЦБР</param>
    /// <returns>Модель даных курсов валют (промежуточная)</returns>
    public CurrencyParentXml XmlDeserializer(string xmlString)
    {
        XmlDocument xmlCurrencies = new XmlDocument();
        xmlCurrencies.LoadXml(xmlString);

        XmlElement? root = xmlCurrencies.DocumentElement;

        XmlNode? dateAttr = root?.Attributes.GetNamedItem("Date");

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CurrencyParentXml));

        using (StringReader reader = new StringReader(xmlString))
        {
            CurrencyParentXml? currencyParentXml = xmlSerializer.Deserialize(reader) as CurrencyParentXml;

            return currencyParentXml!;
        }
    }

}
