// See https://aka.ms/new-console-template for more information
public class CurrencyRate
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string CurrencyCode { get; set; }
    public double Value { get; set; }

    public virtual Currency Currency { get; set; }
}
