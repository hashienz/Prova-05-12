import axios from "axios";
import { useEffect, useState, ChangeEvent, FormEvent } from "react";
import { useParams } from "react-router-dom";
import { Pessoa } from "../../../models/Pessoa";

function AlterarPessoa() {
  const { id } = useParams<{ id: string }>();
  const [nome, setNome] = useState("");
  const [altura, setAltura] = useState<number>(0);
  const [peso, setPeso] = useState<number>(0);

  useEffect(() => {
    async function buscarPessoaAPI() {
      if (!id) {
        console.log("ID não informado");
        return;
      }
      try {
        const resposta = await axios.get(`http://localhost:5000/api/pessoa/${id}`);
        setNome(resposta.data.nome);
        setAltura(resposta.data.altura);
        setPeso(resposta.data.peso);
      } catch (error) {
        console.log("Erro ao buscar pessoa: " + error);
      }
    }
    buscarPessoaAPI();
  }, [id]);

  function obterClassificacao(imcValue: number) {
    if (imcValue === 0) return "";
    if (imcValue < 18.5) return "Abaixo do peso";
    if (imcValue < 25) return "Peso normal";
    if (imcValue < 30) return "Sobrepeso";
    return "Obesidade";
  }

  async function submeterPessoaAPI() {
    try {
      const imc = altura > 0 ? peso / (altura * altura) : 0;
      const pessoa: Pessoa = {
        pessoaId: id ?? "",
        nome,
        altura,
        peso,
        classificacao: obterClassificacao(imc),
      };
      const resposta = await axios.patch(`http://localhost:5000/api/pessoa/alterar/${id}`, pessoa);
      console.log(resposta);
      alert("Pessoa alterada com sucesso!");
    } catch (error: any) {
      if (error.status === 409) {
        console.log("Essa pessoa já foi cadastrada!");
      } else {
        console.log("Erro ao alterar pessoa: " + error);
      }
    }
  }

  function enviarPessoa(e: FormEvent) {
    e.preventDefault();
    submeterPessoaAPI();
  }

  return (
    <div>
      <h1>Alterar Pessoa</h1>
      <form onSubmit={enviarPessoa}>
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
            required
          />
        </div>
        <div>
          <label>Altura (m):</label>
          <input
            type="number"
            step="0.01"
            value={altura}
            onChange={(e: ChangeEvent<HTMLInputElement>) => setAltura(Number(e.target.value) || 0)}
            required
          />
        </div>
        <div>
          <button type="submit">Alterar</button>
        </div>
      </form>
    </div>
  );
}

export default AlterarPessoa;
