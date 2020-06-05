using System;
using System.ComponentModel.DataAnnotations;
using static EM.Domain.Utils;

namespace EM.Domain
{
    public class Aluno : IEntidade
    {
        private int _matricula;
        private string _nome;
        private string _cpf;
        private DateTime _nascimento;
        private EnumeradorDeSexo _sexo;

        public Aluno(int matricula, string nome, string cpf, DateTime nascimento, EnumeradorDeSexo sexo)
        {
            Matricula = matricula;
            Nome = nome;
            CPF = cpf;
            Nascimento = nascimento;
            Sexo = sexo;
        }

        public Aluno() { }

        public int Matricula
        {
            get => _matricula;
            set
            {
                const int tamanhoMaximoMatricula = 9;
                if (value.ToString().Length > tamanhoMaximoMatricula)
                {
                    throw new ValidationException("Tamanho de matrícula excede o máximo (9).");
                }
                _matricula = value;
            }
        }

        public string Nome
        {
            get => _nome;
            set
            {
                const int tamanhoMinimoNome = 1;
                const int tamanhoMaximoNome = 100;
                if (value == null)
                {
                    throw new ValidationException("O nome deve ter pelo menos um caractere.");
                }
                else if (value.Length > tamanhoMaximoNome)
                {
                    throw new ValidationException("Tamanho de nome deve ser menor ou igual a 100 caracteres.");
                }
                else if (value.Length < tamanhoMinimoNome)
                {
                    throw new ValidationException("Tamanho de nome deve ser maior ou igual a 1.");
                }
                _nome = value;
            }
        }

        public DateTime Nascimento
        {
            get => _nascimento;
            set
            {
                const int anoMinimo = 1900;
                if (!(value.CompareTo(DateTime.Today) <= 0))
                {
                    throw new ValidationException("Data deve ser igual ou anterior ao dia de hoje!");
                }
                else if (value.Year < anoMinimo)
                {
                    throw new ValidationException("Ano deve ser maior que 1900!");
                }
                _nascimento = value;
            }
        }
        public EnumeradorDeSexo Sexo
        {
            get => _sexo;
            set => _sexo = value;
        }

        public string CPF
        {
            get => FormateCPF(_cpf);
            set
            {
                if (EhCPFValido(value))
                {
                    _cpf = LimpeCPF(value);
                    return;
                }
                else if (value.Length > 0)
                {
                    throw new ValidationException("CPF inválido!");
                }
                _cpf = "";
            }
        }

        public override bool Equals(object objeto) => objeto is Aluno aluno && Matricula == aluno.Matricula;

        public override int GetHashCode() => Matricula;

        public override string ToString()
        {
            return $"[Matrícula: {Matricula}], [Nome: \"{Nome}\"], " +
                $"[Sexo: {Sexo}], [Nascimento: {Nascimento.ToShortDateString()}], [CPF: {CPF}]";
        }
    }
}
