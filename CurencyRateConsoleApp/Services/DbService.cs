using DAL.Context;
using Microsoft.EntityFrameworkCore;

public class DbService
{
    private readonly CurrencyDB _db;

    public DbService (CurrencyDB db)
    {
        _db = db;
    }

    public async Task<Currency> AddAsync(Currency currency, CancellationToken Cancel = default)
    {
        if (currency == null) throw new ArgumentNullException(nameof(currency));

        await _db.Currencies.AddAsync(currency);
        _db.SaveChanges();

        return currency;
    }

    public async Task<IEnumerable<Currency>> GetAllAsync(CancellationToken Cancel = default)
    {
        var currencies = await _db.Currencies
            .Include(c => c.Rate)
            .ToArrayAsync(Cancel)
            .ConfigureAwait(false);
        
        return currencies;
    }

    public async Task<Currency?> GetByIdAsync(int id, CancellationToken Cancel = default)
    {
        var currency = await _db.Currencies
            .Include(c => c.Rate)
            .FirstOrDefaultAsync(c => c.Id == id, Cancel)
            .ConfigureAwait(false);

        return currency;
    }

    public async Task<Currency?> GetByCbrId(string id_cbr)
    {
        var currency = await _db.Currencies.Where(c => c.Id_cbr == id_cbr).FirstOrDefaultAsync();
        
        return currency;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken Cancel = default)
    {
        var currency = await GetByIdAsync(id, Cancel);

        if (currency is null) return false;
        
        _db.Remove(currency);
        await _db.SaveChangesAsync(Cancel);

        return true;
    }

    public async Task<Currency> UpdateAsync(Currency currency, CancellationToken cancel = default)
    {
        if (currency == null) throw new ArgumentNullException(nameof(currency));

        _db.Update(currency);

        await _db.SaveChangesAsync(cancel).ConfigureAwait(false);

        return currency;
    }

    public async Task<CurrencyRate> UpdateCurrencyRateAsync(CurrencyRate currencyRate, CancellationToken cancel = default)
    {
        if (currencyRate == null) throw new ArgumentNullException(nameof(currencyRate));

        var currency = await _db.Currencies.Include(c => c.Rate).FirstOrDefaultAsync(c => c.Id == currencyRate.Id, cancel).ConfigureAwait(false);

        if (currency == null) throw new ArgumentException(nameof(currencyRate));

        var new_CurrencyRate = new CurrencyRate
        {
            Date = currencyRate.Date,
            Value = currencyRate.Value,
        };

        await _db.CurrencyRates.AddAsync(new_CurrencyRate);

        currency.Rate.Add(currencyRate);

        _db.Update(currency);

        await _db.SaveChangesAsync(cancel).ConfigureAwait(false);

        return currencyRate;
    }

    public async Task<int> GetCurrencyRatesCountAsync(CancellationToken Cancel = default)
    {
        var count = await _db.Currencies.CountAsync(Cancel).ConfigureAwait(false);
        return count;
    }

}