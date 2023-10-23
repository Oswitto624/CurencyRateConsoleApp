using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

public class Currency : NamedEntity
{
    [Required]
    public string Id_cbr { get; set; } = null!;
    [Required]
    public string NumCode { get; set; } = null!;
    [Required]
    public string CharCode { get; set; } = null!;
    [Required]
    public int Nominal { get; set; }

    public ICollection<CurrencyRate> Rate { get; set; } = new HashSet<CurrencyRate>();
}
