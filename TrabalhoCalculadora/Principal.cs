using System;
using System.Collections.Generic;

namespace Trabalho
{
    public class Trabalho
    {
        public static void Main(string[] args)
        {
            Calculadora<double> calc = new Calculadora<double>();
            var v1 = calc.Add(20.50, 12.40);

            Calculadora<int> calc2 = new Calculadora<int>();
            var v2 = calc2.Add(100, 200);

            Calculadora<string> calc3 = new Calculadora<string>();
            var v3 = calc3.Add("ab", "cd");

            Console.WriteLine("Classe Calculadora" +
                "\nResultado do primeiro valor: " + v1 +
                "\nResultado do primeiro valor convertido para int: " + Convert.ToInt32(v1) +
                "\nResultado do segundo valor: " + v2 +
                "\nResultado do terceiro valor: " + v3);

            Console.WriteLine();

            Pessoa p = new Pessoa();
            Pessoa p1 = new Pessoa(1, 10, "Nathan");
            Pessoa p2 = p1;

            Pessoa p3 = new Pessoa(1, 10, "Joãoa");
            Pessoa p4 = new Pessoa(1, 10, "João");

            Console.WriteLine("Comparação p1 com p2: " + p1.Equals(p2) +
                "\nComparação p2 com p1: " + p2.Equals(p1) +
                "\nComparação p2 com p: " + p2.Equals(p) +
                "\nComparação p3 com p: " + p3.Equals(p) +
                "\nComparação p com p3: " + p.Equals(p3) +
                "\nComparação p3 com p4: " + p3.Equals(p4));


            /* Downcast & Upcast */

            Pessoa Pessoa = new Funcionario();
            /* 
             * Downcast & Conversão explícita - Aqui é necessário fazer o cast do funcionário, já que a Pessoa não herda de Funcionário e sim ao contrário.
             * Nesse caso Pessoa pegou o que Funcionario herda da classe Pessoa, porém nenhum método de funcionário pode instanciar Pessoa.
             */
            Funcionario f = (Funcionario)Pessoa;
            Funcionario f2 = new Funcionario();
            /*
             * Upcast & Conversão implícita - Aqui já não precisamos do cast do funcionário, já que dentro de Funcionário temos o objeto Pessoa e podemos, sem
             * a utilização de cast, instanciar diretamente, porém não teremos acesso aos atributos de Funcionário, apenas os de Pessoa.
             * PS: Também é aceito uma conversão explícita, porém não é necessário.
             */
            Pessoa Pessoa2 = f2;
            Pessoa Pessoa3 = (Pessoa)f2;


            /* Exemplos de usos de List<T> */

            List<Funcionario> lista = new List<Funcionario>();
            lista.Add(f);
            lista.Add(f2);
            lista.Add((Funcionario)Pessoa);
            //lista.Add(Pessoa2); // Aqui não é possível adicionar uma Pessoa, pois ela não tem nenhuma herança de Funcionário.

            List<Pessoa> lista2 = new List<Pessoa>();
            lista2.Add(f); // Aqui já podemos adicionar um Funcionário, pois este herda uma Pessoa.

            /* Fazer uma leitura de toda a lista usando lambda, código limpo e fácil de ler */
            lista.ForEach(x => x.ToString());

            /* Criando lista a partir de array pronta */
            Pessoa[] array = new Pessoa[] { Pessoa, f2 };
            List<Pessoa> pessoas = new List<Pessoa>(array);


            /* Operadores de conversão */

            var d = new Digit(7);

            byte number = d;
            Console.WriteLine(number);  // output: 7

            Digit digit = (Digit)number;
            Console.WriteLine(digit);  // output: 7

        }
        public struct Digit
        {
            byte valor;

            public Digit(byte valor)
            {
                if (valor < 0 || valor > 9) throw new ArgumentException();
                this.valor = valor;
            }

            public static implicit operator byte(Digit d) => d.valor;

            public static explicit operator Digit(byte b)
            {
                return new Digit(b);
            }
        }
    }
}
