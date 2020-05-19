using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics;
using System.Runtime.Serialization;
using System.Text;

namespace Calculadora
{
    /*
     * Nesta classe estou usando o generics para permitir que a classe tenha variados Value_Type's, permitindo
     * assim várias operações com apenas um método. 
     * Implementei o delegate apenas para fim de aprendizado, dessa forma já sei como implementar e qual o in_
     * tuito do mesmo, além de saber como funciona o Dictionary. Neste caso, o delegate não tem tanta utilidade,
     * pois os métodos que ele está executando são os mesmos (Apenas somar).
     * 
     * Aprendizados: delegate, Dictionary, Type, typeof e Value_Type, dynamic, throw e Exception, generics,
     * limpeza, simplificação e legibilidade de código e operação ternária.
     */

    public class Calculadora<T>
    {
        delegate T Calcular(T valor1, T valor2);

        Dictionary<Type, Calcular> Dicionario = new Dictionary<Type, Calcular>();

        public Calculadora()
        {
            Dicionario.Add(typeof(int), (v1, v2) => (dynamic)v1 + v2);
            Dicionario.Add(typeof(double), (v1, v2) => (dynamic)v1 + v2);
            Dicionario.Add(typeof(string), (v1, v2) => (dynamic)v1 + v2);
            Dicionario.Add(typeof(decimal), (v1, v2) => (dynamic)v1 + v2);
        }

        public T Add(T valor1, T valor2) => Dicionario.ContainsKey(typeof(T)) ?
            Dicionario[typeof(T)].Invoke(valor1, valor2) :
            throw new FormatException("Não é possivel fazer essa operação com o formato " + typeof(T).Name + ".");
    }
}
