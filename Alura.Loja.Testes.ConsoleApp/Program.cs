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
            var promocaoDePascoa = new Promocao
            {
                Descricao = "Pascoa",
                DataInicio = DateTime.Now,
                DataTermino = DateTime.Now.AddMonths(3)
            };
            promocaoDePascoa.Produtos.Add(new Produto());
            promocaoDePascoa.Produtos.Add(new Produto());
            promocaoDePascoa.Produtos.Add(new Produto());



            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());


            }

            Console.WriteLine("Aplicação finalizada, click enter para fechar!");
            Console.ReadLine();
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
