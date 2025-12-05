export interface Folha {
    id?: string;
    nome: string;
    cpf: string;
    mes: number;
    ano: number;
    horasTrabalhadas: number;
    valorHora: number;
    
    salarioBruto?: number;
    impostoRenda?: number;
    inss?: number;
    fgts?: number;
    salarioLiquido?: number;
    
    criadoEm?: string;
}