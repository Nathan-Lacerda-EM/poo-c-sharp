using System;
using System.Globalization;
using System.Threading;
using Calculadora.Domain;

namespace Calculadora.Console
{
    public class Trabalho
    {
        public static void Main(string[] args)
        {
            object[] obj = EscolherTipo();
            string methodName = (string)obj[0];
            Type[] typeArgs = { (Type)obj[1] };

            object valor1, valor2;
            System.Console.Write("\nDigite o primeiro valor: ");
            valor1 = System.Console.ReadLine();
            System.Console.Write("\nDigite o segundo valor: ");
            valor2 = System.Console.ReadLine();

            var typeCalc = typeof(Calculadora<>);
            var typeConstructed = typeCalc.MakeGenericType(typeArgs);
            var method = typeConstructed.GetMethod(methodName);
            var calculadora = Activator.CreateInstance(typeConstructed);

            var cultureInfo = new CultureInfo(CultureInfo.InvariantCulture.LCID);
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";

            var resultado = method.Invoke(calculadora, ConverterValores(typeArgs, valor1, valor2, cultureInfo));

            System.Console.WriteLine("\nResultado: " + resultado);
        }

        private static object[] ConverterValores(Type[] typeArgs, object valor1, object valor2, CultureInfo cultureInfo)
        {
            return new object[] {
                Convert.ChangeType(valor1.ToString(), typeArgs[0], cultureInfo),
                Convert.ChangeType(valor2.ToString(), typeArgs[0], cultureInfo) };
        }

        public static object[] EscolherTipo()
        {
            int i = 0;
            string operacao, tipo;
            object[] obj = new object[2];

            do
            {
                System.Console.Clear();

                if (i != 1)
                    System.Console.WriteLine("Olá! Seja bem vindo a Calculadora!");

                System.Console.Write("\nOperações disponíveis:" +
                    "\n1 - Soma" +
                    "\n2 - Subtração" +
                    "\n3 - Multiplicação" +
                    "\n4 - Divisão" +
                    "\nDigite a operação que deseja utilizar: ");
                operacao = System.Console.ReadLine();

                if (operacao.Equals("1"))
                    obj.SetValue("Add", 0);
                else if (operacao.Equals("2"))
                    obj.SetValue("Sub", 0);
                else if (operacao.Equals("3"))
                    obj.SetValue("Mul", 0);
                else if (operacao.Equals("4"))
                    obj.SetValue("Div", 0);
                else
                    obj.SetValue(null, 0);

                if (obj[0] == null)
                {
                    System.Console.WriteLine("\nA operação digitada está incorreta!");
                    i = 1;
                    Thread.Sleep(3000);
                    System.Console.Clear();
                    continue;
                }
                else
                    i = 0;

            } while (i == 1);

            do
            {
                System.Console.Clear();
                if (operacao.Equals("1"))
                    System.Console.Write("\nEscolha o tipo" +
                        "\nTipos disponíveis:" +
                        "\n1 - Inteiro" +
                        "\n2 - Double" +
                        "\n3 - Decimal" +
                        "\n4 - String" +
                        "\nDigite um tipo que deseja calcular: ");
                else
                    System.Console.Write("\nEscolha o tipo" +
                        "\nTipos disponíveis:" +
                        "\n1 - Inteiro" +
                        "\n2 - Double" +
                        "\n3 - Decimal" +
                        "\nDigite um tipo que deseja calcular: ");
                tipo = System.Console.ReadLine();

                if (tipo.Equals("1"))
                    obj.SetValue(typeof(int), 1);
                else if (tipo.Equals("2"))
                    obj.SetValue(typeof(double), 1);
                else if (tipo.Equals("3"))
                    obj.SetValue(typeof(decimal), 1);
                else if (tipo.Equals("4"))
                    if (operacao.Equals("1"))
                        obj.SetValue(typeof(string), 1);
                    else
                        obj.SetValue(null, 1);
                else
                    obj.SetValue(null, 1);

                if (obj[1] == null)
                {
                    System.Console.WriteLine("\nO tipo digitado está incorreto!");
                    i = 1;
                    Thread.Sleep(3000);
                    continue;
                }
                else
                    i = 0;

            } while (i == 1);

            System.Console.Clear();

            return obj;
        }
    }
}
