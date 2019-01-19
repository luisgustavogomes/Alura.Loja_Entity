using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class Compra : IEquatable<Compra>
    {
        public int Id { get; set; }
        public double Quantidade { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public double PrecoTotal { get; set; }

        public Compra()
        {
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Compra);
        }

        public bool Equals(Compra other)
        {
            return other != null &&
                   Id == other.Id &&
                   Quantidade == other.Quantidade &&
                   ProdutoId == other.ProdutoId &&
                   EqualityComparer<Produto>.Default.Equals(Produto, other.Produto) &&
                   PrecoTotal == other.PrecoTotal;
        }

        public override int GetHashCode()
        {
            var hashCode = -1736603010;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + Quantidade.GetHashCode();
            hashCode = hashCode * -1521134295 + ProdutoId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Produto>.Default.GetHashCode(Produto);
            hashCode = hashCode * -1521134295 + PrecoTotal.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Quantidade: {Quantidade}, Preço total: {PrecoTotal}";
        }
    }
}
