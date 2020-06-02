using System;
using System.Globalization;
using System.Text;

namespace EM.Domain
{
    public class Utils
    {
        public static bool EhCPFValido(string cpf) //Source: https://gabrielrb.net/2011/10/11/validar-cpf-em-csharp/
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;

            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
            {
                return false;
            }
            tempCpf = cpf.Substring(0, 9);

            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * (multiplicador1[i]);
            }
            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            tempCpf += digito;
            int soma2 = 0;

            for (int i = 0; i < 10; i++)
            {
                soma2 += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma2 % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito += resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static string FormateCPF(string cpf)
        {
            if (cpf != null && cpf.Length > 0)
            {
                return Convert.ToUInt64(LimpeCPF(cpf)).ToString(@"000\.000\.000\-00");
            }
            else if (cpf == "Sem CPF.")
            {
                return "";
            }

            return "Sem CPF.";
        }

        public static string LimpeCPF(string cpf)
        {
            if (cpf == "Sem CPF.")
            {
                return "";
            }

            return cpf.Trim().Replace(".", "").Replace("-", "");
        }

        public static string RemovaAcentosEUppercase(string texto)
        {
            StringBuilder stringBuilder = new StringBuilder();
            char[] arrayDeTexto = texto.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letra in arrayDeTexto)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letra) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(letra);
                }
            }

            return stringBuilder.ToString().ToLower();
        }
    }
}
