using Microsoft.AspNetCore.Mvc;
using Projeto.Models;
using Projeto.Services;

namespace Projeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LancamentoController : ControllerBase
    {
        private readonly LancamentoService _lancamentoService;
        public LancamentoController(LancamentoService lancamentoService)
        {
            _lancamentoService = lancamentoService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLancamento([FromBody] LancamentoModel model)
        {
            try
            {
                await _lancamentoService.Create(model);
                return Ok(new { Message = "Lançamento criado com sucesso" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar o lançamento: {ex.Message}");
                return BadRequest(new { ErrorMessage = "Erro ao criar o lançamento", ExceptionMessage = ex.Message });
            }
        }
        [HttpPost("createNotAvulso")]
        public async Task<IActionResult> CreateLancamentoNotAvulso([FromBody] LancamentoModel model)
        {
            try
            {
                await _lancamentoService.CreateNotAvulso(model);
                return Ok(new { Message = "Lançamento criado com sucesso" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar o lançamento: {ex.Message}");
                return BadRequest(new { ErrorMessage = "Erro ao criar o lançamento", ExceptionMessage = ex.Message });
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditLancamento([FromRoute] int id, [FromBody] LancamentoModel model)
        {
            try
            {
                await _lancamentoService.EditLancamento(id, model);
                return Ok(new { Message = "Lançamento editado com sucesso" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao editar o lançamento: {ex.Message}");
                return BadRequest(new { ErrorMessage = "Erro ao editar o lançamento", ExceptionMessage = ex.Message });
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListarLancamento()
        {
            try
            {
                 var lancamentos = await _lancamentoService.ListarLancamento();
                return Ok(new { Lancamentos = lancamentos, Message = "Lançamentos listados com sucesso" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar os lançamentos: {ex.Message}");
                return BadRequest(new { ErrorMessage = "Erro ao listar os lançamentos", ExceptionMessage = ex.Message });
            }
        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelarLancamento([FromRoute] int id)
        {
            try
            {
                await _lancamentoService.CancelarLancamento(id);
                return Ok(new { Message = "Lançamento cancelado com sucesso" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cancelar o lançamento: {ex.Message}");
                return BadRequest(new { ErrorMessage = "Erro ao cancelar o lançamento", ExceptionMessage = ex.Message });
            }
        }
    }
   
}
