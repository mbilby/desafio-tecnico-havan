using Microsoft.AspNetCore.Mvc;
using Questao5.Services;
using Questao5.Domain.Entities;
using Questao5.DTOs;

namespace Questao5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaService _service;

        public TarefasController(ITarefaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Criar(
            [FromBody] CriarTarefaRequest request)
        {
            try
            {
                Tarefa tarefa = _service.Criar(request);

                return CreatedAtAction(
                    nameof(BuscarPorId),
                    new { id = tarefa.Id },
                    tarefa
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    mensagem = ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult ListarTodas()
        {
            return Ok(
                _service.ListarTodas()
            );
        }

        [HttpGet("{id:guid}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(
                    _service.BuscarPorId(id)
                );
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    mensagem = ex.Message
                });
            }
        }

        [HttpGet("ativas")]
        public IActionResult ListarAtivas()
        {
            return Ok(
                _service.ListarAtivas()
            );
        }

        [HttpGet("concluidas")]
        public IActionResult ListarConcluidasPorPeriodo(
            [FromQuery] DateTime dataInicio,
            [FromQuery] DateTime dataFim)
        {
            try
            {
                return Ok(
                    _service.ListarConcluidasPorPeriodo(
                        dataInicio,
                        dataFim
                    )
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    mensagem = ex.Message
                });
            }
        }

        [HttpPatch("{id:guid}/status")]
        public IActionResult AtualizarStatus(
            Guid id,
            [FromBody] AtualizarStatusRequest request)
        {
            try
            {
                Tarefa tarefa =
                    _service.AtualizarStatus(
                        id,
                        request.Status
                    );

                return Ok(tarefa);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    mensagem = ex.Message
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    mensagem = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    mensagem = ex.Message
                });
            }
        }
    }
}