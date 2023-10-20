using System.Data.SqlTypes;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;

public class CurrencyService
{
    private readonly HttpClient _httpClient;

    public CurrencyService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GetCurrencyRates(DateTime date)
    {
        var url = $"https://cbr.ru/scripts/XML_daily.asp?date_req={date.Day}/{date.Month}/{date.Year}";
               
        var response = await _httpClient.GetAsync(url);

        string responseXml = string.Empty;

        if (response.IsSuccessStatusCode) responseXml = await response.Content.ReadAsStringAsync();
        else Console.WriteLine($"Ошибка в запросе. Код ошибки: {response.StatusCode}");

        return responseXml;
    }


}
