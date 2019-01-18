using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class ProdutoDaoEntity : IProdutoDAO, IDisposable
    {
        public LojaContext context;

        public ProdutoDaoEntity()
        {
            context = new LojaContext();
        }

        public void Adicionar(Produto p)
        {
            context.Produtos.Add(p);
            context.SaveChanges();
        }

        public void Atualizar(Produto p)
        {
            context.Produtos.Update(p);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IList<Produto> SelecionarTodos()
        {
            return context.Produtos.ToList();
        }

        public void Remover(Produto p)
        {
            context.Remove(p);
            context.SaveChanges();
        }

        public Produto Selecionar(int id)
        {
            return context.Produtos.Where(q => q.Id == id).First();
        }
    }
}
