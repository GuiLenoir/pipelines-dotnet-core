namespace GaloRinha.Api.Models;

public class Galo
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public int Vitorias { get; set; }

    public int Derrotas { get; set; }
}
