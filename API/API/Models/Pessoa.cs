using System;

namespace API.Models;

public class Pessoa
{
public string PessoaId {get; set;} = Guid.NewGuid().ToString();
public string Nome {get; set;}
public double  Altura {get; set;}
public double Peso {get; set;}
public double Imc {get; set;}
public string Classificacao {get; set;}
public DateTime DataCriacao {get; set;} = DateTime.Now; 
}