public class Currency
{
    public int Id { get; set; }
    public string NumCode { get; set; }
    public string CharCode { get; set; }
    public string Name { get; set; }
    public int Nominal { get; set; }

    public virtual ICollection<CurrencyRate> Rate { get; set; }
}
