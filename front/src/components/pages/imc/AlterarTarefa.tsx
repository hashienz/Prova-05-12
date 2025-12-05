import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";


function AlterarTarefa(){    
    const { id } = useParams();
    const [titulo, setTitulo] = useState("");

useEffect(() => {
    buscarTarefaAPI()
 }, []);

 async function buscarTarefaAPI(){
    try {
        const resposta = await axios.patch(`/api/tarefas/alterar/${id}`);
        setTitulo(resposta.data.titulo);
    } catch (error) {
        console.log("Erro ao buscar tarefa: " + error)
        
    }
 }
async function submeterTarefaAPI(){
       try {
      const tarefa: Tarefa = {
        titulo,
        categoriaId: 0
      };
      const resposta = await axios.patch(`http://localhost:5000/api/tarefa/alterar/${id}`, tarefa);            
    //    navigate("/")
    console.log(resposta);
    } catch (error : any) {
      if(error.status === 409){
        console.log("Esse tarefa já foi cadastrado!");
      }
    } 

    }
 async function enviarTarefa(e : any){
    e.preventDefault();
    submeterTarefaAPI();
 }

   return (
    <div>
      <h1>Alterar Tarefa</h1>
      <form onSubmit={enviarTarefa}>
        <div>
          <label>Titulo:</label>
          <input type="text"
           value={titulo} onChange={(e: any) => setTitulo(e.target.value)} />
        </div>
        <div>
          <button type="submit">Alterar</button>
        </div>
      </form>
    </div>
  );
}

export default AlterarTarefa;
