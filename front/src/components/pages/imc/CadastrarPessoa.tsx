import { useEffect, useState } from "react";
import axios from "axios";
import { Pessoa } from "../../../models/Pessoa";

function CadastrarPessoa() {
  const [nome, setNome] = useState("");
  const [altura, setAltura] = useState(0); 
  const [peso, setPeso] = useState(0); 
  const [pessoaId, setPessoaId] = useState(0); 
 


  
  function cadastrar(e: any) {
    e.preventDefault();

    const pessoa  : Pessoa = {
      nome: nome,
      altura: altura,
      peso: peso
  
    };

    axios.post("http://localhost:5000/api/pessoa/cadastrar", pessoa)
      .then((resposta) => {
        // console.log("Cadastrado!", resposta.data);
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
          <label>Título:</label>
          <input type="text" onChange={(e) => setNome(e.target.value)} required />
        </div>
        <div>
          <label>Descrição:</label>
          <input type="text" onChange={(e) => setAltura(e.target.value)} />
        </div>
        
      

        <button type="submit">Cadastrar</button>
      </form>
    </div>
  );
}

export default CadastrarPessoa;