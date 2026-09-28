import { useState } from "react";

interface TarefaFormProps {
    onCadastrar: (
        titulo: string,
        descricao: string
    ) => Promise<void>;
}

export function TarefaForm({
    onCadastrar
}: TarefaFormProps) {

    const [titulo, setTitulo] = useState("");
    const [descricao, setDescricao] = useState("");
    const [erro, setErro] = useState("");
    const [enviando, setEnviando] = useState(false);

    async function handleSubmit(
        event: React.FormEvent<HTMLFormElement>
    ) {
        event.preventDefault();

        setErro("");
        setEnviando(true);

        try {
            await onCadastrar(
                titulo,
                descricao
            );

            setTitulo("");
            setDescricao("");
        }
        catch (error) {
            if (error instanceof Error) {
                setErro(error.message);
            }
            else {
                setErro(
                    "Não foi possível cadastrar a tarefa."
                );
            }
        }
        finally {
            setEnviando(false);
        }
    }

    return (
        <form
            className="tarefa-form"
            onSubmit={handleSubmit}
        >
            <h2>Nova tarefa</h2>

            <div className="campo">
                <label htmlFor="titulo">
                    Título
                </label>

                <input
                    id="titulo"
                    type="text"
                    value={titulo}
                    onChange={(event) =>
                        setTitulo(event.target.value)
                    }
                    placeholder="Digite o título"
                />
            </div>

            <div className="campo">
                <label htmlFor="descricao">
                    Descrição
                </label>

                <textarea
                    id="descricao"
                    value={descricao}
                    onChange={(event) =>
                        setDescricao(event.target.value)
                    }
                    placeholder="Digite a descrição"
                />
            </div>

            {erro && (
                <p className="mensagem-erro">
                    {erro}
                </p>
            )}

            <button
                type="submit"
                disabled={enviando}
            >
                {enviando
                    ? "Cadastrando..."
                    : "Cadastrar tarefa"}
            </button>
        </form>
    );
}