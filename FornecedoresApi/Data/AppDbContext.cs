using FornecedoresApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FornecedoresApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<ProdutoFornecedor> ProdutoFornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdutoFornecedor>()
                .HasOne(pf => pf.Produto)
                .WithMany()
                .HasForeignKey(pf => pf.ProdutoId);

            modelBuilder.Entity<ProdutoFornecedor>()
                .Property(pf => pf.ValorCompra)
                .HasColumnType("decimal(18,2)");
        }
    }
}