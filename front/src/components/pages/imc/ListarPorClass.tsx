import { useState } from "react";
import axios from "axios";
import { Pessoa } from "../../../models/Pessoa";

function ListarPorClass() {
  const [classificacao, setClassificacao] = useState("");
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [carregado, setCarregado] = useState(false);

  const opcoes = [
    "Magreza",
    "Normal",
    "Sobrepeso",
    "Obesidade I",
    "Obesidade II",
    "Obesidade III"
  ];

  function buscarPorClassificacao() {
    if (!classificacao) return;
    axios.get<Pessoa[]>(`http://localhost:5000/api/pessoa/listar?classificacao=${encodeURIComponent(classificacao)}`)
      .then((resposta) => {
        setPessoas(resposta.data);
        setCarregado(true);
      })
      .catch(() => {
        setPessoas([]);
        setCarregado(true);
      });
  }

  return (
    <div>
      <h2>Filtrar Pessoas por Classificação</h2>
      <select value={classificacao} onChange={e => setClassificacao(e.target.value)}>
        <option value="">Selecione</option>
        {opcoes.map(op => (
          <option key={op} value={op}>{op}</option>
        ))}
      </select>
      <button onClick={buscarPorClassificacao} disabled={!classificacao}>Buscar</button>
      {carregado && (
        pessoas.length > 0 ? (
          <table border={1}>
            <thead>
              <tr>
                <th>ID</th>
                <th>Nome</th>
                <th>Altura</th>
                <th>Peso</th>
                <th>Classificação</th>
                <th>Data de Criação</th>
              </tr>
            </thead>
            <tbody>
              {pessoas.map(p => (
                <tr key={p.pessoaId}>
                  <td>{p.pessoaId}</td>
                  <td>{p.nome}</td>
                  <td>{p.altura}</td>
                  <td>{p.peso}</td>
                  <td>{p.classificacao}</td>
                  <td>{p.dataCriacao}</td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <p>Nenhuma pessoa encontrada para esta classificação.</p>
        )
      )}
    </div>
  );
}

export default ListarPorClass;
