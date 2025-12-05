import { useEffect, useState } from "react";
import axios from "axios";
import { Pessoa } from "../../../models/Pessoa";

function ListarPessoa() {
  const [pessoas, setPessoa] = useState<Pessoa[]>([]);

  useEffect(() => {
    carregarPessoas();
  }, []);

  function carregarPessoas() {
    axios.get<Pessoa[]>(" http://localhost:5000/api/pessoa/listar")
      .then((resposta) => {
        setPessoa(resposta.data);
      })
      .catch((erro) => {
        console.log("Erro:", erro);
      });
  }

  function alterarPessoa(id: string) {
    axios.patch(` http://localhost:5000/api/pessoas/alterar/${id}`)
      .then((resposta) => {
        console.log("Status alterado!", resposta.data);
        carregarPessoas(); 
      })
      .catch((erro) => {
        console.log("Erro ao alterar:", erro);
      });
  }

  return (
    <div>
      <h1>Lista de Pessoas</h1>
      <table border={1}>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>Altura</th>
            <th>Peso</th>
            <th>Classificacao</th>
          </tr>
        </thead>
        <tbody>
          {pessoas.map((pessoa) => (
            <tr key={pessoa.pessoaId}>
              <td>{pessoa.pessoaId}</td>
              <td>{pessoa.nome}</td>
              { }
              <td>{pessoa.altura}</td>
              <td>{pessoa.peso}</td>
              <td>{pessoa.classificacao}</td>
              <td>{pessoa.dataCriacao}</td>

            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ListarPessoa;