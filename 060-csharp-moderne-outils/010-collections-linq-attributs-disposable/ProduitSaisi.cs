using System.ComponentModel.DataAnnotations;

public sealed class ProduitSaisi
{
    [Required]
    public string Nom { get; init; } = "";

    [Range(0.01, 10000)]
    public decimal Prix { get; init; }
}
