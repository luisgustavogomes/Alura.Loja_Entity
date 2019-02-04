using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string  Nome { get; set; }
        public Endereco EnderecoDeEntrega { get; set; }



        public override string ToString()
        {
            return $"Id: {ClienteId}; Nome: {Nome}; Endereco: {EnderecoDeEntrega}";

        }

    }

}
