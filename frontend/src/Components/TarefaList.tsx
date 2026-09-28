import type {
    StatusTarefa,
    Tarefa
} from "../Types/Tarefa";

import { TarefaCard } from "./TarefaCard";

interface TarefaListProps {
    tarefas: Tarefa[];

    onAlterarStatus: (
        id: string,
        status: StatusTarefa
    ) => Promise<void>;
}

export function TarefaList({
    tarefas,
    onAlterarStatus
}: TarefaListProps) {

    if (tarefas.length === 0) {
        return (
            <p>
                Nenhuma tarefa cadastrada.
            </p>
        );
    }

    return (
        <section className="tarefas">

            <h2>Tarefas</h2>

            <div className="tarefas-lista">

                {tarefas.map((tarefa) => (
                    <TarefaCard
                        key={tarefa.id}
                        tarefa={tarefa}
                        onAlterarStatus={
                            onAlterarStatus
                        }
                    />
                ))}

            </div>

        </section>
    );
}