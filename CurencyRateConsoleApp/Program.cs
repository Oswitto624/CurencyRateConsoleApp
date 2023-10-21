using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


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

using (CurrencyDB context = new CurrencyDB())
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();


    var newCurrency = new Currency
    {
        CharCode = "ABC",
        Name = "test",
        Id_cbr = "ABC1",
        NumCode = "1234",
        Nominal = 1234,
    };

    var newCurrencyRate = new CurrencyRate
    {
        Currency = newCurrency,
        Date = new DateTime(),
        Value = 0.1,        
    };

    context.Currencies.Add(newCurrency);
    context.SaveChanges();
    context.CurrencyRates.Add(newCurrencyRate);
    context.SaveChanges();

    //context.Dispose();

}

//var dbContext = serviceProvider.GetRequiredService<DbContext>();
