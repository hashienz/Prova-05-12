import { useState } from "react";
import axios from "axios";
import { Folha } from "../../../models/Folha";

function CadastrarFolha() {
  // --- ESTADOS PARA OS CAMPOS DO FORMULÁRIO ---
  const [nome, setNome] = useState("");
  const [cpf, setCpf] = useState("");
  const [mes, setMes] = useState("");
  const [ano, setAno] = useState("");
  const [horas, setHoras] = useState("");
  const [valor, setValor] = useState("");

  // --- ESTADO PARA GUARDAR O RESULTADO (A FOLHA CALCULADA) ---
  const [folhaCalculada, setFolhaCalculada] = useState<Folha | null>(null);

  function enviarFolha(e: any) {
    e.preventDefault();

    // Montamos o objeto para enviar (sem os campos calculados)
    const folha = {
      nome: nome,
      cpf: cpf,
      mes: Number(mes),
      ano: Number(ano),
      horasTrabalhadas: Number(horas),
      valorHora: Number(valor)
    };

  
    axios.post("http://localhost:5000/api/folha/cadastrar", folha)
      .then((resposta) => {
        // SUCESSO!
        // O Back-end devolve a folha completa, já com os cálculos.
        setFolhaCalculada(resposta.data); 
        alert("Folha cadastrada e calculada com sucesso!");
      })
      .catch((erro) => {
        // TRATAMENTO DE ERROS (Validar duplicidade, etc.)
        if (erro.response && erro.response.status === 400) {
          alert("Erro de Validação: " + erro.response.data);
        } else {
          console.log("Erro:", erro);
          alert("Ocorreu um erro ao cadastrar.");
        }
      });
  }

  return (
    <div style={{ padding: "20px" }}>
      <h1>Cadastro de Folha (Matemático)</h1>

      <form onSubmit={enviarFolha}>
        <div style={{ marginBottom: "10px" }}>
          <label>Nome:</label><br/>
          <input type="text" value={nome} onChange={(e) => setNome(e.target.value)} required />
        </div>

        <div style={{ marginBottom: "10px" }}>
          <label>CPF:</label><br/>
          <input type="text" value={cpf} onChange={(e) => setCpf(e.target.value)} required />
        </div>

        <div style={{ marginBottom: "10px" }}>
          <label>Mês:</label><br/>
          <input type="number" value={mes} onChange={(e) => setMes(e.target.value)} required />
        </div>

        <div style={{ marginBottom: "10px" }}>
          <label>Ano:</label><br/>
          <input type="number" value={ano} onChange={(e) => setAno(e.target.value)} required />
        </div>

        <div style={{ marginBottom: "10px" }}>
          <label>Horas Trabalhadas:</label><br/>
          <input type="number" value={horas} onChange={(e) => setHoras(e.target.value)} required />
        </div>

        <div style={{ marginBottom: "10px" }}>
          <label>Valor da Hora:</label><br/>
          <input type="number" value={valor} onChange={(e) => setValor(e.target.value)} required />
        </div>

        <button type="submit">Calcular e Salvar</button>
      </form>

      {/* --- ÁREA DE RESULTADO --- */}
      {/* Só aparece se o cadastro funcionar e tivermos uma folhaCalculada */}
      {folhaCalculada && (
        <div style={{ marginTop: "20px", border: "1px solid green", padding: "10px" }}>
          <h3>Resultado do Cálculo:</h3>
          <p><strong>Funcionário:</strong> {folhaCalculada.nome}</p>
          <p><strong>Salário Bruto:</strong> {folhaCalculada.salarioBruto?.toFixed(2)} $</p>
          <p><strong>Imposto de Renda:</strong> {folhaCalculada.impostoRenda?.toFixed(2)} $</p>
          <p><strong>INSS:</strong> {folhaCalculada.inss?.toFixed(2)} $</p>
          <p><strong>FGTS:</strong> {folhaCalculada.fgts?.toFixed(2)} $</p>
          <h4 style={{ color: "blue" }}>Salário Líquido: {folhaCalculada.salarioLiquido?.toFixed(2)} €</h4>
        </div>
      )}
    </div>
  );
}

export default CadastrarFolha;