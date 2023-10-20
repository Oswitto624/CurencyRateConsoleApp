// See https://aka.ms/new-console-template for more information
public class Currency
{
    public string Code { get; set; }
    public string Name { get; set; }

    public virtual ICollection<CurrencyRate> Rate { get; set; }
}
