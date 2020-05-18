using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics;
using System.Runtime.Serialization;
using System.Text;

namespace Trabalho
{
    public class Calculadora<T> : IConvertible
    {
        public delegate T Calcular(T valor1, T valor2);

        private Dictionary<Type, Calcular> dicionario = new Dictionary<Type, Calcular>();

        object Resultado = null;

        public Calculadora()
        {
            dicionario.Add(typeof(int), (v1, v2) => (dynamic)v1 + v2);
            dicionario.Add(typeof(double), (v1, v2) => (dynamic)v1 + v2);
            dicionario.Add(typeof(string), (v1, v2) => (dynamic)v1 + v2);
            dicionario.Add(typeof(decimal), (v1, v2) => (dynamic)v1 + v2);
        }

        public T Add(T valor1, T valor2) => dicionario.ContainsKey(typeof(T)) ? GetResultado(dicionario[typeof(T)].Invoke(valor1, valor2)) : throw new FormatException("Não é possivel fazer essa operação com o formato " + typeof(T).Name + ".");

        public T GetResultado(T valor)
        {
            Resultado = valor;
            return (T)Resultado;
        }

        public TypeCode GetTypeCode() => TypeCode.Object;

        bool IConvertible.ToBoolean(IFormatProvider provider) => throw new System.FormatException("Não é possível converter para o formato Boolean.");

        byte IConvertible.ToByte(IFormatProvider provider) => Convert.ToByte(Resultado);

        char IConvertible.ToChar(IFormatProvider provider) => Convert.ToChar(Resultado);

        DateTime IConvertible.ToDateTime(IFormatProvider provider) => throw new System.FormatException("Não é possível converter para o formato DateTime.");

        decimal IConvertible.ToDecimal(IFormatProvider provider) => Convert.ToDecimal(Resultado);

        double IConvertible.ToDouble(IFormatProvider provider) => Convert.ToDouble(Resultado);

        short IConvertible.ToInt16(IFormatProvider provider) => Convert.ToInt16(Resultado);

        int IConvertible.ToInt32(IFormatProvider provider) =>  Convert.ToInt32(Resultado);

        long IConvertible.ToInt64(IFormatProvider provider) => Convert.ToInt64(Resultado);

        sbyte IConvertible.ToSByte(IFormatProvider provider) => Convert.ToSByte(Resultado);

        float IConvertible.ToSingle(IFormatProvider provider) => Convert.ToSingle(Resultado);

        string IConvertible.ToString(IFormatProvider provider) => Convert.ToString(Resultado);

        object IConvertible.ToType(Type conversionType, IFormatProvider provider) => Convert.ChangeType(Resultado, conversionType);

        ushort IConvertible.ToUInt16(IFormatProvider provider) => Convert.ToUInt16(Resultado);

        uint IConvertible.ToUInt32(IFormatProvider provider) => Convert.ToUInt32(Resultado);

        ulong IConvertible.ToUInt64(IFormatProvider provider) => Convert.ToUInt64(Resultado);
    }
}
