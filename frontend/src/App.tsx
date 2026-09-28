import {
    useCallback,
    useEffect,
    useState
} from "react";

import "./App.css";

import { TarefaForm }
    from "./Components/TarefaForm";

import { TarefaList }
    from "./Components/TarefaList";

import {
    atualizarStatus,
    criarTarefa,
    listarTarefas
} from "./Services/TarefaService";

import type {
    StatusTarefa,
    Tarefa
} from "./Types/Tarefa";

function App() {

    const [tarefas, setTarefas] =
        useState<Tarefa[]>([]);

    const [erro, setErro] =
        useState("");

    const [carregando, setCarregando] =
        useState(true);

    const carregarTarefas =
        useCallback(async () => {

            try {
                setErro("");

                const dados =
                    await listarTarefas();

                setTarefas(dados);
            }
            catch (error) {

                if (error instanceof Error) {
                    setErro(error.message);
                }

            }
            finally {
                setCarregando(false);
            }

        }, []);

    useEffect(() => {
        carregarTarefas();
    }, [carregarTarefas]);

    async function handleCadastrar(
        titulo: string,
        descricao: string
    ) {

        await criarTarefa({
            titulo,
            descricao
        });

        await carregarTarefas();
    }

    async function handleAlterarStatus(
        id: string,
        status: StatusTarefa
    ) {

        await atualizarStatus(
            id,
            status
        );

        await carregarTarefas();
    }

    return (
        <main className="container">

            <h1>
                Gerenciamento de Tarefas
            </h1>

            <TarefaForm
                onCadastrar={
                    handleCadastrar
                }
            />

            {erro && (
                <p className="mensagem-erro">
                    {erro}
                </p>
            )}

            {carregando
                ? (
                    <p>
                        Carregando tarefas...
                    </p>
                )
                : (
                    <TarefaList
                        tarefas={tarefas}
                        onAlterarStatus={
                            handleAlterarStatus
                        }
                    />
                )
            }

        </main>
    );
}

export default App;