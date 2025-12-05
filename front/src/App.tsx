import React from "react";
import { BrowserRouter, Link, Route, Routes } from "react-router-dom";

// Imports de TAREFAS (Estilo Fluxo)
import ListarPessoa from "./components/pages/imc/ListarPessoa";
import CadastrarPessoa from "./components/pages/imc/CadastrarPessoa";
import AlterarPessoa from "./components/pages/imc/AlterarPessoa";
import ListarPorClass from "./components/pages/imc/ListarPorClass";

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <nav>
          <ul>
            
            <li> <Link to="/">Listar Pessoas</Link> </li>
            <li> <Link to="/pages/imc/cadastrar">Cadastrar Pessoa</Link> </li>
            <li> <Link to="/pages/imc/filtrar">Filtrar por Classificação</Link> </li>
            
            { }
            <li style={{ marginLeft: "20px", listStyle: "none" }}>|</li>
            
          </ul>
        </nav>
        
        <Routes>
          {/* Rotas de TAREFAS */}
          <Route path="/" element={<ListarPessoa />} />
          <Route path="/pages/imc/listar" element={<ListarPessoa />} />
          <Route path="/pages/imc/cadastrar" element={<CadastrarPessoa />} />
          <Route path="/pages/imc/alterar/:id" element={<AlterarPessoa />} />
          <Route path="/pages/imc/filtrar" element={<ListarPorClass />} />

        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;