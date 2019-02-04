using Microsoft.EntityFrameworkCore;
using System;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class LojaContext : DbContext
    {
        /// <summary>
        /// Exemplo de criação do ORM, lembrando que a classe que está implementando o DbSet e a classe da aplicação.
        /// </summary>
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        //classes que tem relação com classes mapeadas também seram mapeadas, porém, podem ser expecificada no DbSet
        //public DbSet<Endereco> Enderecos { get; set; }

        public LojaContext()
        { }

        public LojaContext(DbContextOptions<LojaContext> options) : base(options)
        { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=LojaDB;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PromocaoProduto>()
                .HasKey(pp => new { pp.PromocaoId, pp.ProdutoId });

            modelBuilder
                .Entity<Endereco>()
                .Property<int>("ClienteId");

            modelBuilder
                .Entity<Endereco>()
                .ToTable("Enderecos");

            modelBuilder
                .Entity<Endereco>()
                .HasKey("ClienteId");
            base.OnModelCreating(modelBuilder);
        }


    }
}