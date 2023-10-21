using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

public class CurrencyRate : Entity
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public double Value { get; set; }

    [Required]
    public Currency Currency { get; set; } = null!;
}
