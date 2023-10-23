using DAL.Context;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Data.SqlTypes;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

public class CurrencyService
{
    private readonly DbService _db;

    public CurrencyService(DbService db) 
    {
        _db = db;
    }

    public CurrencyService():this(new DbService(new CurrencyDB())) { }

    /// <summary>
    /// Получение ответа от сервиса ЦБР
    /// </summary>
    /// <param name="date">Дата, на которую нужны курсы валют</param>
    /// <returns>Строка с ответом от ЦБР</returns>
    public async Task<string> GetCurrencyRatesAsync(DateTime date)
    {
        string dateNormalized = date.ToString("dd/MM/yyyy");
        var url = $"https://cbr.ru/scripts/XML_daily.asp?date_req={dateNormalized}";
        //var url = $"https://cbr.ru/scripts/XML_daily.asp?date_req={date.Day}/{date.Month}/{date.Year}";

        string responseXmlString = string.Empty;
        
        using(var httpClient  = new HttpClient())
        {
            var response = httpClient.GetAsync(url);

            #region Encoding
            //долго не выходило получить корректный доступ к содержимому XML
            //пришлось решать проблему с отсутствующей кодировкой windows-1254, которая используется в XML от ЦБР
            //спасибо решению на https://stackoverflow.com/questions/33579661/encoding-getencoding-cant-work-in-uwp-app
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1254");
            #endregion

            if (response.Result.IsSuccessStatusCode) responseXmlString = await response.Result.Content.ReadAsStringAsync();
            else Console.WriteLine($"Ошибка в запросе. Код ошибки: {response.Result.StatusCode}");

            return responseXmlString;
        }       
    }

    /// <summary>
    /// Десериализация XML от ЦБР, используя XmlSerializer
    /// </summary>
    /// <param name="xmlString">Строка с ответом от сервиса ЦБР</param>
    /// <returns>Модель даных курсов валют (промежуточная)</returns>
    public List<Currency> XmlDeserializer(string xmlString)
    {
        XmlDocument xmlCurrencies = new XmlDocument();
        xmlCurrencies.LoadXml(xmlString);

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CurrencyParentXml), new XmlRootAttribute("ValCurs"));

        using (StringReader reader = new StringReader(xmlString))
        {
            CurrencyParentXml? currencyParentXml = xmlSerializer.Deserialize(reader) as CurrencyParentXml;

            List<Currency> currency = Mapping.MapCurrency(currencyParentXml);
            
            return currency;
        }
    }

    /// <summary>
    /// Метод для проверки сущестования валюты в бд. При нахождении совпадения - добавляет только курс валюты. При отсутствии совпадения - добавляет данные валюты и её курс.
    /// </summary>
    /// <param name="currencies">список валют</param>
    public async Task CheckAndAddOrUpdate(List<Currency> currencies)
    {
        foreach (Currency currency in currencies)
        {
            var expectedCurrency = await _db.GetByCbrId(currency.Id_cbr);
            if (expectedCurrency is null)
            {
                await _db.AddAsync(currency);
            }
            else
            {
                var currencyRate = currency.Rate.Single();
                currencyRate.Currency = expectedCurrency;
                await _db.UpdateCurrencyRateAsync(currencyRate);
            }
        }
    }

    /// <summary>
    /// Проверка есть ли данные в БД
    /// </summary>
    /// <returns>true если есть, false если нет</returns>
    public async Task<bool> CheckOldData()
    {
        if (await _db.GetCurrencyRatesCountAsync() == 0) return false;

        return true;
    }
}
