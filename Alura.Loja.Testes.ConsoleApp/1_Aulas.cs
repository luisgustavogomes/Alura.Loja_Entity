using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Alura.Loja.Testes.ConsoleApp
{
    public partial class Program
    {
        private static void LoggerSQLViaEntity()
        {
            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var produtos = contexto.Produtos.ToList();
                ExibeEntries(contexto.ChangeTracker.Entries());

                var novoProduto = new Produto()
                {
                    Nome = "Sabão em pó",
                    Categoria = "Limpeza",
                    PrecoUnitario = 4.99
                };
                contexto.Produtos.Add(novoProduto);
                ExibeEntries(contexto.ChangeTracker.Entries());

                contexto.Produtos.Remove(novoProduto);
                ExibeEntries(contexto.ChangeTracker.Entries());

                //var prd = produtos.First();
                //contexto.Produtos.Remove(prd);

                //contexto.SaveChanges();

                var entry = contexto.Entry(novoProduto);
                Console.WriteLine(entry.Entity.ToString() + " - " + entry.State);

                ExibeEntries(contexto.ChangeTracker.Entries());


            }
        }

        private static void ExibeEntries(IEnumerable<EntityEntry> entries)
        {
            Console.WriteLine("=================");
            foreach (var e in entries)
            {
                Console.WriteLine(e.Entity.ToString() + " - " + e.State);
            }
        }

        private static void DeletandoProdutosUsandoEntity()
        {
            using (var contexto = new LojaContext())
            {
                List<Produto> produtos = contexto.Produtos.ToList();
                produtos.ForEach(p => contexto.Remove(p));
                contexto.SaveChanges();
            }
        }

        private static void RecuperarProdutosUsandoEntity()
        {
            IList<Produto> produtos = null;
            using (var contexto = new LojaContext())
            {
                produtos = contexto.Produtos.ToList();
            }

            produtos.ToList().ForEach(p => Console.WriteLine(p));

        }

        

        private static void AtualizarProduto()
        {
            Produto produtos = null;
            using (var contexto = new LojaContext())
            {
                produtos = contexto.Produtos.First();
                Console.WriteLine(produtos);
                produtos.Nome = "Harry Potter e a Ordem da Fênix";
                contexto.Produtos.Update(produtos);
                contexto.SaveChanges();
            }
        }

        private static void GravarUsandoAdoNet()
        {
            Produto p = new Produto
            {
                Nome = "Harry Potter e a Ordem da Fênix",
                Categoria = "Livros",
                PrecoUnitario = 19.89
            };

            using (var repo = new ProdutoDAO())
            {
                repo.Adicionar(p);
            }
        }

        private static void TrabalhandoRelacionamentoUmParaMuitos()
        {
            var pao = new Produto()
            {
                Nome = "Pão",
                PrecoUnitario = 0.40,
                Unidade = "Un",
                Categoria = "Padaria"
            };

            var compra = new Compra
            {
                Quantidade = 6,
                Produto = pao
            };
            compra.PrecoTotal = pao.PrecoUnitario * compra.Quantidade;

            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                contexto.Compras.Add(compra);
                ExibeEntries(contexto.ChangeTracker.Entries());
                contexto.SaveChanges();
            }
        }
    }
}
