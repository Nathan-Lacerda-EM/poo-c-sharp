using System;
using System.Collections.Generic;

namespace Calculadora.Domain
{
    public class Calculadora<T>
    {
        delegate T Calcular(T valor1, T valor2);

        Dictionary<Type, Calcular> DicionarioSoma = new Dictionary<Type, Calcular>();
        Dictionary<Type, Calcular> DicionarioSubtracao = new Dictionary<Type, Calcular>();
        Dictionary<Type, Calcular> DicionarioDivisao = new Dictionary<Type, Calcular>();
        Dictionary<Type, Calcular> DicionarioMultiplicacao = new Dictionary<Type, Calcular>();

        public Calculadora()
        {
            DicionarioSoma.Add(typeof(int), (v1, v2) => (dynamic)v1 + v2);
            DicionarioSoma.Add(typeof(double), (v1, v2) => (dynamic)v1 + v2);
            DicionarioSoma.Add(typeof(string), (v1, v2) => (dynamic)v1 + v2);
            DicionarioSoma.Add(typeof(decimal), (v1, v2) => (dynamic)v1 + v2);

            DicionarioSubtracao.Add(typeof(int), (v1, v2) => (dynamic)v1 - v2);
            DicionarioSubtracao.Add(typeof(double), (v1, v2) => (dynamic)v1 - v2);
            DicionarioSubtracao.Add(typeof(decimal), (v1, v2) => (dynamic)v1 - v2);

            DicionarioDivisao.Add(typeof(int), (v1, v2) => (dynamic)v1 / v2);
            DicionarioDivisao.Add(typeof(double), (v1, v2) => (dynamic)v1 / v2);
            DicionarioDivisao.Add(typeof(decimal), (v1, v2) => (dynamic)v1 / v2);

            DicionarioMultiplicacao.Add(typeof(int), (v1, v2) => (dynamic)v1 * v2);
            DicionarioMultiplicacao.Add(typeof(double), (v1, v2) => (dynamic)v1 * v2);
            DicionarioMultiplicacao.Add(typeof(decimal), (v1, v2) => (dynamic)v1 * v2);
        }

        public T Add(T valor1, T valor2) => DicionarioSoma.ContainsKey(typeof(T)) ?
            DicionarioSoma[typeof(T)].Invoke(valor1, valor2) :
            throw new FormatException("Não é possivel fazer essa operação com o formato " + typeof(T).Name + ".");

        public T Sub(T valor1, T valor2) => DicionarioSubtracao.ContainsKey(typeof(T)) ?
            DicionarioSubtracao[typeof(T)].Invoke(valor1, valor2) :
            throw new FormatException("Não é possivel fazer essa operação com o formato " + typeof(T).Name + ".");

        public T Div(T valor1, T valor2) => DicionarioDivisao.ContainsKey(typeof(T)) ?
            DicionarioDivisao[typeof(T)].Invoke(valor1, valor2) :
            throw new FormatException("Não é possivel fazer essa operação com o formato " + typeof(T).Name + ".");

        public T Mul(T valor1, T valor2) => DicionarioMultiplicacao.ContainsKey(typeof(T)) ?
            DicionarioMultiplicacao[typeof(T)].Invoke(valor1, valor2) :
            throw new FormatException("Não é possivel fazer essa operação com o formato " + typeof(T).Name + ".");
    }
}
