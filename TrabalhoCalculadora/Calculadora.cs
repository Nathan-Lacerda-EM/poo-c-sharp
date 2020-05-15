using System;
using System.Collections.Generic;
using System.Text;

namespace Trabalho
{
    public class Calculadora<T> : IConvertible
    {
        /* 
         * O dynamic foi colocado por estamos manipulando uma classe genérica, ou seja, ainda não sabemos
         * o tipo de dados que vamos utilizar, já que será atribuído em tempo de execução e ele dá suporte
         * para qualquer tipo de operação.
         */
        dynamic Resultado { get; set; }
        public T Add(T valor1, T valor2)
        {
            switch (Type.GetTypeCode(valor1.GetType()))
            {
                case TypeCode.String:
                    {
                        Resultado = (dynamic)valor1 + valor2;
                        return Resultado;
                    }

                case TypeCode.Int32:
                    {
                        Resultado = (dynamic)valor1 + valor2;
                        return Resultado;
                    }

                case TypeCode.Double:
                    {
                        Resultado = (dynamic)valor1 + valor2;
                        return Resultado;
                    }

                case TypeCode.Decimal:
                    {
                        Resultado = (dynamic)valor1 + valor2;
                        return Resultado;
                    }

                default:
                    {
                        return default; // Esse default quer dizer o valor padrão da variável que T for. [Bool: false], [Valores de referência: null], [Numéricos: 0], etc...
                    }
            }
        }

        /* 
         * As classes abaixo são da Interface IConvertible, usamos ela para fazer formas de conversão
         * personalizadas, ou seja, converter da forma que bem entender os tipos de váriaveis.
         * PS: Temos obrigação de colocar todos os métodos, já que estamos implementando uma Interface.
         */

        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            throw new System.FormatException("Não é possível converter para o formato Boolean.");
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return (Convert.ToByte(Resultado));
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(Resultado);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new System.FormatException("Não é possível converter para o formato DateTime.");
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(Resultado);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(Resultado);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(Resultado);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(Resultado);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(Resultado);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(Resultado);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(Resultado);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            throw new System.FormatException("Não é possível converter para o formato String.");
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(Resultado, conversionType);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(Resultado);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(Resultado);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(Resultado);
        }
    }
}
