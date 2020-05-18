using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics;
using System.Runtime.Serialization;
using System.Text;

namespace Trabalho
{
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

        public T Add(T valor1, T valor2) => Dicionario.ContainsKey(typeof(T)) ? Dicionario[typeof(T)].Invoke(valor1, valor2) : throw new FormatException("Não é possivel fazer essa operação com o formato " + typeof(T).Name + ".");
    }
}
