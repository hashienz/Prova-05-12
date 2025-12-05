using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Folha
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Nome { get; set; }
    public string Cpf { get; set; } 
    public int Mes { get; set; }
    public int Ano { get; set; }
    public int HorasTrabalhadas { get; set; }
    public double ValorHora { get; set; }

    // --- Campos Calculados ---
    public double SalarioBruto { get; set; }
    public double ImpostoRenda { get; set; }
    public double Inss { get; set; }
    public double SalarioLiquido { get; set; }
}