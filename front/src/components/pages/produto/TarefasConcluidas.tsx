import { useEffect, useState } from "react";
import axios from "axios";
import { Tarefa } from "../../../models/Tarefa";

function TarefasConcluidas() {
  const [tarefas, setTarefas] = useState<Tarefa[]>([]);

  useEffect(() => {
    // ATENÇÃO: Chama o endpoint específico de CONCLUÍDAS
    axios.get<Tarefa[]>("http://localhost:5000/api/tarefas/concluidas")
      .then((resposta) => {
        setTarefas(resposta.data);
      })
      .catch((erro) => {
        console.log("Erro:", erro);
      });
  }, []);

  return (
    <div>
      <h1>Tarefas Concluídas</h1>
      <table border={1}>
        <thead>
          <tr>
            <th>ID</th>
            <th>Título</th>
            <th>Categoria</th>
            <th>Status</th>
            <th>Criado Em</th>
          </tr>
        </thead>
        <tbody>
          {tarefas.map((tarefa) => (
            <tr key={tarefa.tarefaId}>
              <td>{tarefa.tarefaId}</td>
              <td>{tarefa.titulo}</td>
              <td>{tarefa.categoria?.nome || tarefa.categoriaId}</td>
              <td>{tarefa.status}</td>
              <td>{tarefa.criadoEm}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default TarefasConcluidas;