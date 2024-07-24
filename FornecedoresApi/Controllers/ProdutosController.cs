using FornecedoresApi.Data;
using FornecedoresApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FornecedoresApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> ListarProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> ListarProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> AdicionarProduto(Produto produto)
        {
            if (await _context.Produtos.AnyAsync(p => p.Descricao == produto.Descricao))
            {
                return Conflict("Produto já cadastrado.");
            }

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ListarProduto), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarProduto(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            var produtoDoBanco = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

            if (produtoDoBanco == null)
            {
                return NotFound();
            }

            produtoDoBanco.Descricao = produto.Descricao;
            produtoDoBanco.UnidadeDeMedida = produto.UnidadeDeMedida;

            _context.Entry(produtoDoBanco).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}