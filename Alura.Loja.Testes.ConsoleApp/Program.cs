using Microsoft.EntityFrameworkCore;
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
        public static void Main(string[] args)
        {


            Console.WriteLine("Aplicação finalizada, click enter para fechar!");
            Console.ReadKey();
        }

        private static void SelecionandoItemEmColecoesRelacionadas()
        {
            using (var contexto = new LojaContext())
            {
                var loggerFactory = contexto.GetInfrastructure<IServiceProvider>().GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var produto = contexto
                        .Produtos
                        .Include(p => p.Compras)
                        .FirstOrDefault();

                contexto.Entry(produto)
                    .Collection(p => p.Compras)
                    .Query()
                    .Where(c => c.PrecoTotal > 10)
                    .Load();

                foreach (var item in produto.Compras)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static void SelecionandoUmParaUm()
        {
            using (var contexto = new LojaContext())
            {
                var loggerFactory = contexto.GetInfrastructure<IServiceProvider>().GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var cliente = contexto
                    .Clientes
                    .Include(c => c.EnderecoDeEntrega)
                    .FirstOrDefault();
                Console.WriteLine(cliente.EnderecoDeEntrega.Logradouro);

                var produto = contexto
                    .Produtos
                    .Include(p => p.Compras)
                    .Where(p => p.Id == 8)
                    .FirstOrDefault();

                foreach (var item in produto.Compras)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static void SelecionandoMuitosParaMuitos()
        {
            using (var contexto = new LojaContext())
            {
                var loggerFactory = contexto.GetInfrastructure<IServiceProvider>().GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var promocao = contexto
                    .Promocoes
                    .Include(p => p.Produtos)
                    .ThenInclude(pp => pp.Produto)
                    .FirstOrDefault();
                Console.WriteLine("\n==================\n");
                foreach (var item in promocao.Produtos)
                {
                    Console.WriteLine(item.Produto);
                }
            }
        }

        private static void IncluirPromocao()
        {
            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var promocao = new Promocao()
                {
                    Descricao = "Janeiro",
                    DataInicio = new DateTime(2018, 1, 1),
                    DataTermino = new DateTime(2018, 1, 31)
                };

                var produtos = contexto
                    .Produtos
                    .Where(p => p.Categoria == "Bebidas")
                    .ToList();
                foreach (var item in produtos)
                {
                    promocao.IncluirProduto(item);
                }

                contexto.Promocoes.Add(promocao);
                ExibeEntries(contexto.ChangeTracker.Entries());
                contexto.SaveChanges();
            }
        }

        private static void UmParaUm()
        {
            var luis = new Cliente()
            {
                Nome = "Luis",
                EnderecoDeEntrega = new Endereco()
                {
                    Numero = 12,
                    Logradouro = "Rua A",
                    Complemento = "",
                    Bairro = "Centro",
                    Cidade = "Z"
                }
            };

            using (var contexto = new LojaContext())
            {
                contexto.Clientes.Add(luis);
                contexto.SaveChanges();

            }
        }

        private static void MuitosParaMuitos()
        {
            var p1 = new Produto() { Nome = "Suco de Laranja", Categoria = "Bebidas", PrecoUnitario = 8.89, Unidade = "Litros" };
            var p2 = new Produto() { Nome = "Suco de Uva", Categoria = "Bebidas", PrecoUnitario = 3.89, Unidade = "Litros" };
            var p3 = new Produto() { Nome = "Suco de Limão", Categoria = "Bebidas", PrecoUnitario = 5.89, Unidade = "Litros" };

            var promocaoDePascoa = new Promocao
            {
                Descricao = "Pascoa",
                DataInicio = DateTime.Now,
                DataTermino = DateTime.Now.AddMonths(3)
            };

            promocaoDePascoa.IncluirProduto(p1);
            promocaoDePascoa.IncluirProduto(p2);
            promocaoDePascoa.IncluirProduto(p3);



            using (var contexto = new LojaContext())
            {
                //log no console
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                //contexto.Promocoes.Add(promocaoDePascoa);
                var promocao = contexto.Promocoes.Find(1);
                contexto.Promocoes.Remove(promocao);
                contexto.SaveChanges();

                ExibeEntries(contexto.ChangeTracker.Entries());
            }
        }

        private static void GravarUsandoEntity()
        {
            Produto p = new Produto
            {
                Nome = "Harry Potter e a Ordem da Fênix",
                Categoria = "Livros",
                PrecoUnitario = 19.89
            };

            using (var contexto = new LojaContext())
            {
                contexto.Produtos.Add(p);
                contexto.SaveChanges();
            }

        }

    }
}
