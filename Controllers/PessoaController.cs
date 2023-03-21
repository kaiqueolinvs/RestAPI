

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIPessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    { 
    

        public PessoaController(ILogger<PessoaController> logger)
        {

        }



        [HttpPost]
        [Authorize]
       
        public classpessoa ProcessarinformacoesPessoa([FromBody] PessoaRequest request)
        {
            var idade = CalcularIdade(request.DataNascimento);
            var imc = CalcularIMC(request.Peso, request.Altura);
            var classificacao = calcularClassificacao(imc);
            var desconto = calcularDesconto(request.salario);
            var inss = calcularInss(request.salario, desconto);
            var salarioLiquido = Liquido(request.salario, inss);
            var saldodolar = Dolar(request.Saldo);


            var resposta = new classpessoa();
             resposta.saldodolar = saldodolar;
            resposta.aliquota = desconto;
            resposta.classificacao = classificacao;
            resposta.salarioliquido = desconto;
            resposta.inss = inss;
            resposta.idade = idade;
            resposta.imc= imc;
            resposta.nome = request.Nome;
           
           
           
            return resposta;

        }

        private int CalcularIdade(DateTime dataNascimento)
        {
            var anoAtual = DateTime.Now.Year;
            var idade = anoAtual - dataNascimento.Year;

            var mesAtual = DateTime.Now.Month;
            if(mesAtual < dataNascimento.Month) {
                idade =  - 1;
            
            }
            return idade;
        }
        private decimal CalcularIMC(decimal peso, decimal altura)
        {
            return Math.Round(peso / (altura * altura), 2);

        }
        private string calcularClassificacao(decimal imc)
        {
            var classificacao = "";
            if (imc < (Decimal)18.50)
            {
                classificacao = "abaixo do peso";
            }
            else if (imc > (Decimal)18.50 && imc <= (Decimal)25.90)
            {
                classificacao = "peso normal";

            }
            else if (imc >= (Decimal)25.0 && imc <= (Decimal)29.90)
            {

                classificacao = " sobrepeso";
            }
            else if (imc >= (Decimal)30.0 && imc <= (Decimal)34.90)
            {
                classificacao = "obesidade nivel I";
            }

            else if (imc >= (Decimal)35.0 && imc <= (Decimal)39.0)
            {

                classificacao = " obesidade nivel II";
            }
            else
            {
                classificacao = "obesidade nivel III";



            }
           return classificacao;
        }

        private double calcularDesconto(double salario)
        {
            var desconto = 7.5;

            if (salario < 1.045)
            {
                desconto = 7.5;
            }
            else if (salario >= 1.405 && salario < 2089.60)
            {
                desconto = 9;

            }
            else if (salario >= 2089.61 && salario < 3134.40)
            {
                desconto = 12;
            }

            else
            {
                desconto = 14;
            }
             return desconto;
        }
        private double calcularInss(double salario, double desconto )
        {
            return (salario * desconto) / 100;
            
        }
        private double Liquido( double salario, double inss)
        {
            return salario - inss;
        }
        private decimal Dolar(decimal saldo)
        {

            var dolar = (decimal)5.14;
            var saldodolar = Math.Round( saldo / dolar, 2);
            return saldodolar;
        }

    }
}
