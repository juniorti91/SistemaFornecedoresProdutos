using FornecedoresApi.Data;
using FornecedoresApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FornecedoresApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private readonly AppDbContext _context;
        private static readonly HttpClient client = new HttpClient();

        public FornecedoresController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> ListarFornecedores()
        {
            return await _context.Fornecedores.Include(f => f.Produtos).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Fornecedor>> ObterFornecedorPorId(int id)
        {
            var fornecedor = await _context.Fornecedores
                .Include(f => f.Produtos)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return fornecedor;
        }

        [HttpPost]
        public async Task<ActionResult<Fornecedor>> AdicionarFornecedor(Fornecedor fornecedor)
        {
            if (await _context.Fornecedores.AnyAsync(f => f.Cnpj == fornecedor.Cnpj))
            {
                return Conflict("Fornecedor já cadastrado.");
            }

            var endereco = await ObterEnderecoPeloCep(fornecedor.Endereco);
            if (endereco != null)
            {
                fornecedor.Endereco = endereco;
            }

            _context.Fornecedores.Add(fornecedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ObterFornecedorPorId", new { id = fornecedor.Id }, fornecedor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarFornecedor(int id, Fornecedor fornecedor)
        {
            if (id != fornecedor.Id)
            {
                return BadRequest();
            }

            var endereco = await ObterEnderecoPeloCep(fornecedor.Endereco);
            if (endereco != null)
            {
                fornecedor.Endereco = endereco;
            }

            _context.Entry(fornecedor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarFornecedor(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            _context.Fornecedores.Remove(fornecedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<string> ObterEnderecoPeloCep(string cep)
        {
            try
            {
                var url = $"https://viacep.com.br/ws/{cep}/json/";
                var resposta = await client.GetStringAsync(url);

                // Verifica se a resposta não está nula ou vazia
                if (string.IsNullOrEmpty(resposta))
                {
                    Console.WriteLine("A resposta da API está vazia.");
                    return null;
                }

                // Deserializar a resposta JSON
                ViaCepResposta? dadosEndereco = JsonSerializer.Deserialize<ViaCepResposta>(resposta);

                if (dadosEndereco == null)
                {
                    Console.WriteLine("Falha ao deserializar a resposta JSON.");
                    return null;
                }

                return dadosEndereco.logradouro;
            }
            catch (Exception ex)
            {
                // Erro na consulta da API
                Console.WriteLine($"Erro ao deserializar JSON: {ex.Message}");
                return null;
            }
        }
    }
}