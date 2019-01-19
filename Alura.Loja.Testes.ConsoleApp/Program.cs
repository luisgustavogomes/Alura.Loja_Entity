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


            Console.WriteLine("Aplicação finalizada, click enter para fechar!");
            Console.ReadLine();
        }

    }


}
