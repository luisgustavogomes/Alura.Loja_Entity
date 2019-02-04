﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class Endereco
    {
        public int Numero { get; internal set; }
        public string Logradouro { get; internal set; }
        public string Complemento { get; internal set; }
        public string Bairro { get; internal set; }
        public string Cidade { get; internal set; }
        public Cliente Cliente { get; set; }


        public override string ToString()
        {
            return $"Numero: {Numero}; Logradouro: {Logradouro}; Complento: {Complemento} ; Bairro: {Bairro} ; Cidade: {Cidade} ";
        }
    }
}
