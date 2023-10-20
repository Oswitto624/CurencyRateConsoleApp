using Domain.Base;

public class Currency : NamedEntity
{
    public string Id_cbr { get; set; }
    public string NumCode { get; set; }
    public string CharCode { get; set; }
    public int Nominal { get; set; }

    public virtual ICollection<CurrencyRate> Rate { get; set; }
}
