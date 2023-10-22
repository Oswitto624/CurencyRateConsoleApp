using DAL.Context;
using Microsoft.EntityFrameworkCore;

public class DbService
{
    private readonly CurrencyDB _db;

    public DbService (CurrencyDB db)
    {
        _db = db;
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
}