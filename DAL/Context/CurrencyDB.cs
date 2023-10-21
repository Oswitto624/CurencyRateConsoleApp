using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace DAL.Context;

public class CurrencyDB : DbContext
{
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<CurrencyRate> CurrencyRates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CurrencyDB;");
    }

    //public CurrencyDB() { }
    //public CurrencyDB(DbContextOptions<CurrencyDB> options) : base(options) { }

}
