using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

#region Попытка подключения БД v1
//var services = new ServiceCollection();

//var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json")
//    .Build();

//var connectionString = configuration.GetConnectionString("SqlServer");

//services.AddDbContext<CurrencyDB>(opt =>
//{
//    opt.UseSqlServer(connectionString);
//});

//var serviceProvider = services.BuildServiceProvider();
//var dbContext = serviceProvider.GetRequiredService<DbContext>();
#endregion

#region Костыльное создание БД, используя тестовые данные v2
//using (CurrencyDB context = new CurrencyDB())
//{
//    context.Database.EnsureDeleted();
//    context.Database.EnsureCreated();


//    var newCurrency = new Currency
//    {
//        CharCode = "ABC1",
//        Name = "test1",
//        Id_cbr = "ABC1",
//        NumCode = "1234",
//        Nominal = 1,
//    };

//    var newCurrencyRate = new CurrencyRate
//    {
//        Currency = newCurrency,
//        Date = new DateTime(),
//        Value = 37,
//    };

//    await context.Currencies.AddAsync(newCurrency);
//    await context.SaveChangesAsync();
//    await context.CurrencyRates.AddAsync(newCurrencyRate);
//    await context.SaveChangesAsync();
//}
#endregion



var service = new CurrencyService();

DateTime currentDate = DateTime.Now;

var xmlString = await service.GetCurrencyRatesAsync(currentDate);

var currencies = service.XmlDeserializer(xmlString);

if (!await service.CheckOldData())
{
    #region Рассчёт дат за последний месяц
    DateTime[] dates = new DateTime[DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month - 1)];

    for (int i = 0; i < dates.Length; i++)
    {
        dates[i] = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, i + 1);
    }

    // Проверка на високосный год
    if (DateTime.IsLeapYear(DateTime.Now.Year) && DateTime.Now.Month == 3)
    {
        dates[dates.Length - 1] = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, dates.Length);
    }
    #endregion

    foreach (var date in dates)
    {
        await service.CheckAndAddOrUpdate(currencies);
    }
}




await service.CheckAndAddOrUpdate(currencies);

