using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Projeto.Data;
using Projeto.Models;
using System.Formats.Asn1;

namespace Projeto.Services
{
    public class LancamentoService
    {
        private readonly AppDbContext _context;

        public LancamentoService(AppDbContext context)
        {
            _context = context;
        }
       
        public async Task Create(LancamentoModel model)
        {
            try
            {
                LancamentoModel lancamento = new()
                {
                    Descricao = model.Descricao,
                    Data = DateTime.Now.Date,
                    Valor = model.Valor,
                    Avulso = true,
                    Status = "Válido"
                };

                _context.Lancamentos.Add(lancamento);
                await _context.SaveChangesAsync();
                
            }catch
            {
                throw new Exception("Erro ao criar o lançamento");
            }

        }
        public async Task CreateNotAvulso(LancamentoModel model)
        {
            try
            {
                LancamentoModel lancamento = new()
                {
                    Descricao = model.Descricao,
                    Data = DateTime.Now.Date,
                    Valor = model.Valor,
                    Avulso = false,
                    Status = "Válido"
                };

                _context.Lancamentos.Add(lancamento);
                await _context.SaveChangesAsync();

            }
            catch
            {
                throw new Exception("Erro ao criar o lançamento");
            }

        }

        public async Task EditLancamento(int id, LancamentoModel model)
        {
            try
            {
                var lancamento = await _context.Lancamentos.FindAsync(id);

                if (lancamento == null)
                    return;

                if (lancamento.Status == "Válido" && lancamento.Avulso == true)
                {
                    lancamento.Data = model.Data;
                    lancamento.Valor = model.Valor;
                    

                    _context.Update(lancamento);
                    await _context.SaveChangesAsync();
                }

                return;
            }
            catch
            {
                throw new Exception("Erro ao editar o lançamento");
            }
        }


        public async Task<List<LancamentoModel>> ListarLancamento()
        {
            try
            {
                var lancamentos = await _context.Lancamentos.ToListAsync();
                return lancamentos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar os lançamentos: {ex.Message}");
                throw new Exception("Erro ao listar os lançamentos", ex);
            }
        }
        public async Task CancelarLancamento(int id)
        {
            try
            {
                var lancamento = await _context.Lancamentos.Where(x => x.Id == id)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (lancamento == null) return;

               if(lancamento.Status == "Válido" && lancamento.Avulso == true)
                {
                    lancamento.Status = "Cancelado";
                    _context.Update(lancamento);
                    await _context.SaveChangesAsync();
                }

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cancelar o lançamento: {ex.Message}");
                throw new Exception("Erro ao cancelar o lançamento", ex);
            }
        }

    }
}
