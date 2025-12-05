import React from "react";
import { BrowserRouter, Link, Route, Routes } from "react-router-dom";

// Imports de TAREFAS (Estilo Fluxo)
import CadastrarTarefa from "./components/pages/produto/CadastrarTarefa";
import ListarTarefa from "./components/pages/produto/ListarTarefa";
import TarefasConcluidas from "./components/pages/produto/TarefasConcluidas";
import TarefasNaoConcluidas from "./components/pages/produto/TarefasNaoConcluidas";
import CadastrarFolha from "./components/pages/produto/CadastrarFolha";

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <nav>
          <ul>
            {/* SEÇÃO TAREFAS */}
            <li> <Link to="/">Listar Tarefas</Link> </li>
            <li> <Link to="/pages/tarefa/cadastrar">Cadastrar Tarefa</Link> </li>
            <li> <Link to="/pages/tarefa/concluidas">Tarefas Concluídas</Link> </li>
            <li> <Link to="/pages/tarefa/naoconcluidas">Tarefas Pendentes</Link> </li>
            
            {/* SEÇÃO FOLHA (Separador visual) */}
            <li style={{ marginLeft: "20px", listStyle: "none" }}>|</li>
            
            {/* LINK PARA O ESTILO MATEMÁTICO */}
            <li> 
                <Link to="/pages/folha/cadastrar" style={{ fontWeight: "bold", color: "blue" }}>
                    Nova Folha (Matemático)
                </Link> 
            </li>
          </ul>
        </nav>
        
        <Routes>
          {/* Rotas de TAREFAS */}
          <Route path="/" element={<ListarTarefa />} />
          <Route path="/pages/tarefa/listar" element={<ListarTarefa />} />
          <Route path="/pages/tarefa/cadastrar" element={<CadastrarTarefa />} />
          <Route path="/pages/tarefa/concluidas" element={<TarefasConcluidas />} />
          <Route path="/pages/tarefa/naoconcluidas" element={<TarefasNaoConcluidas />} />

          {/* Rota de FOLHA - NOVA! */}
          <Route path="/pages/folha/cadastrar" element={<CadastrarFolha />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;