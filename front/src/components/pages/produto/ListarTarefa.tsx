import { useEffect, useState } from "react";
import axios from "axios";
import { Tarefa } from "../../../models/Tarefa";

function ListarTarefa() {
  const [tarefas, setTarefas] = useState<Tarefa[]>([]);

  useEffect(() => {
    carregarTarefas();
  }, []);

  function carregarTarefas() {
    axios.get<Tarefa[]>("http://localhost:5000/api/tarefas/listar")
      .then((resposta) => {
        setTarefas(resposta.data);
      })
      .catch((erro) => {
        console.log("Erro:", erro);
      });
  }

  function alterarStatus(id: string) {
    axios.patch(`http://localhost:5000/api/tarefas/alterar/${id}`)
      .then((resposta) => {
        console.log("Status alterado!", resposta.data);
        carregarTarefas(); 
      })
      .catch((erro) => {
        console.log("Erro ao alterar:", erro);
      });
  }

  return (
    <div>
      <h1>Lista de Tarefas</h1>
      <table border={1}>
        <thead>
          <tr>
            <th>ID</th>
            <th>Título</th>
            <th>Categoria</th>
            <th>Status</th>
            <th>Ação</th>
          </tr>
        </thead>
        <tbody>
          {tarefas.map((tarefa) => (
            <tr key={tarefa.tarefaId}>
              <td>{tarefa.tarefaId}</td>
              <td>{tarefa.titulo}</td>
              {/* Mostra o nome da categoria se ela vier preenchida, senão mostra o ID */}
              <td>{tarefa.categoria?.nome || tarefa.categoriaId}</td>
              <td>{tarefa.status}</td>
              <td>
                <button onClick={() => alterarStatus(tarefa.tarefaId!)}>
                  Avançar Status
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ListarTarefa;