import { useEffect, useState } from "react";
import axios from "axios";
import { Tarefa } from "../../../models/Tarefa";
import { Categoria } from "../../../models/Categoria";

function CadastrarTarefa() {
  const [titulo, setTitulo] = useState("");
  const [descricao, setDescricao] = useState("");
  const [categoriaId, setCategoriaId] = useState(0); 
  const [categorias, setCategorias] = useState<Categoria[]>([]); 


  useEffect(() => {
    axios.get<Categoria[]>("http://localhost:5000/api/categoria/listar")
      .then((resposta) => {
        setCategorias(resposta.data);
      })
      .catch((erro) => {
        console.log("Erro ao carregar categorias:", erro);
      });
  }, []);

  function cadastrar(e: any) {
    e.preventDefault();

    const tarefa: Tarefa = {
      titulo: titulo,
      descricao: descricao,
      status: "Não iniciada",
      categoriaId: categoriaId
    };

    axios.post("http://localhost:5000/api/tarefas/cadastrar", tarefa)
      .then((resposta) => {
        console.log("Cadastrado!", resposta.data);
        alert("Tarefa cadastrada com sucesso!");
      })
      .catch((erro) => {
        console.log("Erro:", erro);
      });
  }

  return (
    <div>
      <h1>Cadastrar Tarefa</h1>
      <form onSubmit={cadastrar}>
        <div>
          <label>Título:</label>
          <input type="text" onChange={(e) => setTitulo(e.target.value)} required />
        </div>
        <div>
          <label>Descrição:</label>
          <input type="text" onChange={(e) => setDescricao(e.target.value)} />
        </div>
        
        {/* SELECT DE CATEGORIAS */}
        <div>
          <label>Categoria:</label>
          <select onChange={(e) => setCategoriaId(Number(e.target.value))} required>
            <option value="0">Selecione uma categoria...</option>
            {categorias.map((categoria) => (
              <option key={categoria.categoriaId} value={categoria.categoriaId}>
                {categoria.nome}
              </option>
            ))}
          </select>
        </div>

        <button type="submit">Cadastrar</button>
      </form>
    </div>
  );
}

export default CadastrarTarefa;