using System;
using System.Collections.Generic;

namespace Trabalho
{
    public class Trabalho
    {
        public static void Main(string[] args)
        {
            Calculadora<decimal>.Calcular CalcTeste = new Calculadora<decimal>.Calcular(new Calculadora<decimal>().Add);
            var v = CalcTeste(79228162M, 1);

            var convertido = Convert.ToUInt32(CalcTeste.Target);

            Calculadora<double> calc = new Calculadora<double>();
            var v1 = calc.Add(20.50, 12.40);

            Calculadora<int> calc2 = new Calculadora<int>();
            var v2 = calc2.Add(100, 200);

            Calculadora<string> calc3 = new Calculadora<string>();
            var v3 = calc3.Add("ab", "cd");

            Pessoa p = new Pessoa();
            Pessoa p1 = new Pessoa(1, 10, "Nathan");
            Pessoa p2 = p1;

            Pessoa p3 = new Pessoa(1, 10, "João");
            Pessoa p4 = new Pessoa(1, 10, "João");



            /* Downcast & Upcast */

            Pessoa Pessoa = new Funcionario(); //Upcast
            /* 
             * Downcast & Conversão explícita - Aqui é necessário fazer o cast do funcionário, já que a Pessoa não herda de Funcionário e sim ao contrário.
             * Nesse caso Pessoa pegou o que Funcionario herda da classe Pessoa, porém nenhum método de funcionário pode instanciar Pessoa.
             */
            Funcionario f = (Funcionario)Pessoa; //Downcast
            Funcionario f2 = new Funcionario();
            /*
             * Upcast & Conversão implícita - Aqui já não precisamos do cast do funcionário, já que dentro de Funcionário temos o objeto Pessoa e podemos, sem
             * a utilização de cast, instanciar diretamente, porém não teremos acesso aos atributos de Funcionário, apenas os de Pessoa.
             * PS: Também é aceito uma conversão explícita, porém não é necessário.
             */
            Pessoa Pessoa2 = f2;
            Pessoa Pessoa3 = (Pessoa)f2;

            Pessoa.Cep = 1;
            var Name = Pessoa.Nome;


            /* Exemplos de usos de List<T> */
            List<Funcionario> funcionarios = new List<Funcionario>();
            funcionarios.Add(f);
            funcionarios.Add(f2);
            funcionarios.Add((Funcionario)Pessoa);

            //lista.Add(Pessoa2); // Aqui não é possível adicionar uma Pessoa, pois ela não tem nenhuma herança de Funcionário.

            List<Pessoa> pessoas = new List<Pessoa>();
            pessoas.Add(Pessoa2);
            pessoas.Add(f);



            List<Pessoa> lista2 = new List<Pessoa>();
            lista2.Add(f); // Aqui já podemos adicionar um Funcionário, pois este herda uma Pessoa.

            /* Fazer uma leitura de toda a lista usando lambda, código limpo e fácil de ler */
            funcionarios.ForEach(x => x.ToString());

            /* Criando lista a partir de array pronta */
            Pessoa[] array = new Pessoa[] { Pessoa, f2 };
            //List<Pessoa> pessoas = new List<Pessoa>(array);
        }
    }
}
