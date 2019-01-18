using System;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class Produto : IEquatable<Produto>
    {
        public int Id { get; internal set; }
        public string Nome { get; internal set; }
        public string Categoria { get; internal set; }
        public double PrecoUnitario { get; internal set; }
        public string Unidade { get; private set; }
        public string Observacoes { get; private set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Produto);
        }

        public bool Equals(Produto other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nome: {Nome}, Categoria: {Categoria}, Preço: {PrecoUnitario}.";
        }


    }
}