import React from "react";
import { BrowserRouter, Link, Route, Routes } from "react-router-dom";

// Imports de TAREFAS (Estilo Fluxo)
import ListarPessoa from "./components/pages/imc/ListarPessoa";
import CadastrarPessoa from "./components/pages/imc/CadastrarPessoa";

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <nav>
          <ul>
            {/* SEÇÃO TAREFAS */}
            <li> <Link to="/">Listar Pessoas</Link> </li>
            <li> <Link to="/pages/tarefa/cadastrar">Cadastrar Pessoa</Link> </li>
            
            { }
            <li style={{ marginLeft: "20px", listStyle: "none" }}>|</li>
            
          </ul>
        </nav>
        
        <Routes>
          {/* Rotas de TAREFAS */}
          <Route path="/" element={<ListarPessoa />} />
          <Route path="/pages/imc/listar" element={<ListarPessoa />} />
          <Route path="/pages/imc/cadastrar" element={<CadastrarPessoa />} />

        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;