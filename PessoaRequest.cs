using System;

namespace WebAPIPessoa
{
    public class PessoaRequest
    {
        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public decimal Altura { get; set; }

        public decimal Peso { get; set; }

        public double salario { get; set; }     

        public decimal Saldo { get; set; }  
    }
}
