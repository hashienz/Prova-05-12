import { useState, ChangeEvent, FormEvent } from "react";
import axios from "axios";
import { Pessoa } from "../../../models/Pessoa";

function CadastrarPessoa() {
  const [nome, setNome] = useState("");
  const [altura, setAltura] = useState<number>(0);
  const [peso, setPeso] = useState<number>(0);

  function cadastrar(e: FormEvent) {
    e.preventDefault();

    const imc = altura > 0 ? peso / (altura * altura) : 0;

    function obterClassificacao(imcValue: number) {
      if (imcValue === 0) return "";
      if (imcValue < 18.5) return "Abaixo do peso";
      if (imcValue < 25) return "Peso normal";
      if (imcValue < 30) return "Sobrepeso";
      return "Obesidade";
    }

    const pessoa: Pessoa = {
      nome,
      altura,
      peso,
      classificacao: obterClassificacao(imc),
    };

    axios
      .post("http://localhost:5000/api/pessoa/cadastrar", pessoa)
      .then((resposta) => {
        alert("Pessoa cadastrada com sucesso!");
      })
      .catch((erro) => {
        console.log("Erro:", erro);
      });
  }

  return (
    <div>
      <h1>Cadastrar Pessoa</h1>
      <form onSubmit={cadastrar}>
        <div>
          <label>Nome:</label>
          <input
            type="text"
            value={nome}
            onChange={(e: ChangeEvent<HTMLInputElement>) => setNome(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Peso (kg):</label>
          <input
            type="number"
            step="0.01"
            value={peso}
            onChange={(e: ChangeEvent<HTMLInputElement>) => setPeso(Number(e.target.value) || 0)}
          />
        </div>
        <div>
          <label>Altura (m):</label>
          <input
            type="number"
            step="0.01"
            value={altura}
            onChange={(e: ChangeEvent<HTMLInputElement>) => setAltura(Number(e.target.value) || 0)}
          />
        </div>

        <button type="submit">Cadastrar</button>
      </form>
    </div>
  );
}

export default CadastrarPessoa;