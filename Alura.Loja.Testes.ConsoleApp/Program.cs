using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //GravarUsandoAdoNet();
            //GravarUsandoEntity();
            RecuperarProdutos();
            Console.WriteLine("Aplicação finalizada, click enter para fechar!");
            Console.ReadLine();
        }

        private static void RecuperarProdutos()
        {
            IList<Produto> produtos = null;
            using (var contexto = new LojaContext())
            {
                produtos = contexto.Produtos.ToList();
            }

            produtos.ToList().ForEach(p => Console.WriteLine(p));
            
        }

        private static void GravarUsandoEntity()
        {
            Produto p = new Produto
            {
                Nome = "Harry Potter e a Ordem da Fênix",
                Categoria = "Livros",
                Preco = 19.89
            };

            using (var contexto = new LojaContext())
            {
                contexto.Produtos.Add(p);
                contexto.SaveChanges();
            }

        }

        private static void GravarUsandoAdoNet()
        {
            Produto p = new Produto
            {
                Nome = "Harry Potter e a Ordem da Fênix",
                Categoria = "Livros",
                Preco = 19.89
            };

            using (var repo = new ProdutoDAO())
            {
                repo.Adicionar(p);
            }
        }
    }
}
