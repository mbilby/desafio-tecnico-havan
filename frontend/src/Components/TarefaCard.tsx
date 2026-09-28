import type {
    StatusTarefa,
    Tarefa
} from "../Types/Tarefa";

interface TarefaCardProps {
    tarefa: Tarefa;
    onAlterarStatus: (
        id: string,
        status: StatusTarefa
    ) => Promise<void>;
}

export function TarefaCard({
    tarefa,
    onAlterarStatus
}: TarefaCardProps) {

    function obterProximoStatus():
        StatusTarefa | null {

        if (tarefa.status === "Pendente") {
            return "EmAndamento";
        }

        if (tarefa.status === "EmAndamento") {
            return "Concluida";
        }

        return null;
    }

    function obterTextoBotao() {
        if (tarefa.status === "Pendente") {
            return "Iniciar";
        }

        if (tarefa.status === "EmAndamento") {
            return "Concluir";
        }

        return "";
    }

    function obterTextoStatus(status: StatusTarefa) {
        if (status === "EmAndamento") {
            return "Em andamento";
        }

        if (status === "Concluida") {
            return "Concluída";
        }

        return "Pendente";
    }

    const proximoStatus = obterProximoStatus();

    return (
        <article className="task-card">
            <div className="task-card__header">
                <div>
                    <h3 className="task-card__title">
                        {tarefa.titulo}
                    </h3>
                </div>

                <span
                    className={`task-badge task-badge--${tarefa.status}`}
                >
                    {obterTextoStatus(tarefa.status)}
                </span>
            </div>

            <p className="task-card__description">
                {tarefa.descricao || "Sem descrição informada."}
            </p>

            <div className="task-card__meta">
                <div className="task-card__meta-item">
                    <span className="task-card__label">
                        Criada em
                    </span>
                    <span className="task-card__value">
                        {new Date(
                            tarefa.dataDeCriacao
                        ).toLocaleString("pt-BR")}
                    </span>
                </div>

                {tarefa.dataDeConclusao && (
                    <div className="task-card__meta-item">
                        <span className="task-card__label">
                            Concluída em
                        </span>
                        <span className="task-card__value">
                            {new Date(
                                tarefa.dataDeConclusao
                            ).toLocaleString("pt-BR")}
                        </span>
                    </div>
                )}
            </div>

            {proximoStatus && (
                <div className="task-card__footer">
                    <button
                        className="task-card__button"
                        onClick={() =>
                            onAlterarStatus(
                                tarefa.id,
                                proximoStatus
                            )
                        }
                    >
                        {obterTextoBotao()}
                    </button>
                </div>
            )}
        </article>
    );
}