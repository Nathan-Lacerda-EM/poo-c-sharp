using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace Trabalho
{
    public class Old_Calculadora<T> : IConvertible
    {
        dynamic resultado;
        public T Add(T valor1, T valor2)
        {
            switch (Type.GetTypeCode(valor1.GetType()))
            {
                case TypeCode.String:
                    {
                        resultado = null;
                        return resultado;
                    }
                case TypeCode.Boolean:
                    {
                        resultado = null;
                        return resultado;
                    }
                case TypeCode.DateTime:
                    {
                        resultado = null;
                        return resultado;
                    }
                case TypeCode.Empty:
                    {
                        resultado = null;
                        return resultado;
                    }
                case TypeCode.Object:
                    {
                        resultado = null;
                        return resultado;
                    }
                default:
                    {
                        resultado = (dynamic)valor1 + valor2;
                        return resultado;
                    }
            }
        }

        public T Sub(T valor1, T valor2)
        {
            switch (Type.GetTypeCode(valor1.GetType()))
            {
                case TypeCode.String:
                    {
                        resultado = null;
                        return resultado;
                    }
                case TypeCode.Boolean:
                    {
                        resultado = null;
                        return resultado;
                    }
                case TypeCode.DateTime:
                    {
                        resultado = null;
                        return resultado;
                    }
                case TypeCode.Empty:
                    {
                        resultado = null;
                        return resultado;
                    }
                case TypeCode.Object:
                    {
                        resultado = null;
                        return resultado;
                    }
                default:
                    {
                        resultado = (dynamic)valor1 - valor2;
                        return resultado;
                    }
            }
        }

        public T Mul(T valor1, T valor2)
        {
            switch (Type.GetTypeCode(valor1.GetType()))
            {
                case TypeCode.String:
                    {
                        resultado = null;
                        return resultado;
                    }
                case TypeCode.Boolean:
                    {
                        resultado = null;
                        return resultado;
                    }
                case TypeCode.DateTime:
                    {
                        resultado = null;
                        return resultado;
                    }
                case TypeCode.Empty:
                    {
                        resultado = null;
                        return resultado;
                    }
                case TypeCode.Object:
                    {
                        resultado = null;
                        return resultado;
                    }
                default:
                    {
                        resultado = (dynamic)valor1 * valor2;
                        return resultado;
                    }
            }
        }

        public T Div(T valor1, T valor2)
        {
            switch (Type.GetTypeCode(valor1.GetType()))
            {
                case TypeCode.String:
                    {
                        resultado = null;
                        return resultado;
                    }
                case TypeCode.Boolean:
                    {
                        resultado = null;
                        return resultado;
                    }
                case TypeCode.DateTime:
                    {
                        resultado = null;
                        return resultado;
                    }
                case TypeCode.Empty:
                    {
                        resultado = null;
                        return resultado;
                    }
                case TypeCode.Object:
                    {
                        resultado = null;
                        return resultado;
                    }
                default:
                    {
                        resultado = (dynamic)valor1 / valor2;
                        return resultado;
                    }
            }
        }

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
            return (Convert.ToByte(resultado));
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(resultado);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new System.FormatException("Não é possível converter para o formato DateTime.");
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(resultado);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(resultado);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(resultado);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(resultado);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(resultado);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(resultado);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(resultado);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            throw new System.FormatException("Não é possível converter para o formato String.");
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(resultado, conversionType);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(resultado);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(resultado);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(resultado);
        }
    }
}
