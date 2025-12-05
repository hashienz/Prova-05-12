import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Pessoa } from "../../../models/Pessoa";


function AlterarPessoa(){    
    const { id } = useParams();
    const [nome, setNome] = useState("");

useEffect(() => {
    buscarPessoaAPI()
 }, []);

 async function buscarPessoaAPI(){
    try {
        const resposta = await axios.patch(`/api/pessoa/alterar/${id}`);
        setNome(resposta.data.nome);
    } catch (error) {
        console.log("Erro ao buscar o nome: " + error)
        
    }
 }
async function submeterTarefaAPI(){
       try {
      const pessoa: Pessoa = {
        nome,
        pessoaId: " "
      };
      const resposta = await axios.patch(`http://localhost:5000/api/pessoa/alterar/${id}`, pessoa);            
    //    navigate("/")
    console.log(resposta);
    } catch (error : any) {
      if(error.status === 409){
        console.log("Esse tarefa já foi cadastrado!");
      }
    } 

    }
 async function enviarPessoa(e : any){
    e.preventDefault();
    submeterTarefaAPI();
 }

   return (
    <div>
      <h1>Alterar Tarefa</h1>
      <form onSubmit={enviarPessoa}>
        <div>
          <label>Nome:</label>
          <input type="text"
           value={nome} onChange={(e: any) => setNome(e.target.value)} />
        </div>
        <div>
          <button type="submit">Alterar</button>
        </div>
      </form>
    </div>
  );
}

export default AlterarPessoa;
