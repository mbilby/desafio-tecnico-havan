export type StatusTarefa =
    | "Pendente"
    | "EmAndamento"
    | "Concluida";

export interface Tarefa {
    id: string;
    titulo: string;
    descricao: string;
    dataDeCriacao: string;
    dataDeConclusao: string | null;
    status: StatusTarefa;
}

export interface CriarTarefaRequest {
    titulo: string;
    descricao: string;
}

export interface AtualizarStatusRequest {
    status: StatusTarefa;
}