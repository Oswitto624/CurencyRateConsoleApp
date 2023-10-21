using Microsoft.EntityFrameworkCore;

namespace DAL.Context;


public class CurrencyDB : DbContext
{
    public DbSet<Currency> Currencies { get; set; }

    public DbSet<CurrencyRate> CurrencyRates { get; set; }

    public CurrencyDB(DbContextOptions<CurrencyDB> options) : base(options) { }
}
