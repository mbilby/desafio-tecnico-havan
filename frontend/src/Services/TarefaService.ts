import type {
    AtualizarStatusRequest,
    CriarTarefaRequest,
    StatusTarefa,
    Tarefa
} from "../Types/Tarefa";

const API_URL = "http://localhost:5000/api/tarefas";

interface ApiError {
    mensagem?: string;
}

async function tratarResposta(response: Response) {
    if (!response.ok) {
        const erro: ApiError = await response.json();

        throw new Error(
            erro.mensagem ?? "Ocorreu um erro na requisição."
        );
    }

    return response.json();
}

export async function listarTarefas(): Promise<Tarefa[]> {
    const response = await fetch(API_URL);

    return tratarResposta(response);
}

export async function criarTarefa(
    tarefa: CriarTarefaRequest
): Promise<Tarefa> {

    const response = await fetch(API_URL, {
        method: "POST",

        headers: {
            "Content-Type": "application/json"
        },

        body: JSON.stringify(tarefa)
    });

    return tratarResposta(response);
}

export async function atualizarStatus(
    id: string,
    status: StatusTarefa
): Promise<Tarefa> {

    const request: AtualizarStatusRequest = {
        status
    };

    const response = await fetch(
        `${API_URL}/${id}/status`,
        {
            method: "PATCH",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify(request)
        }
    );

    return tratarResposta(response);
}