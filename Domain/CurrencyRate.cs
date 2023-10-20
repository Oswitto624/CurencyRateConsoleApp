using Domain.Base;

public class CurrencyRate : Entity
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public double Value { get; set; }

    public virtual Currency Currency { get; set; }
}
