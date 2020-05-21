using Microsoft.VisualBasic.CompilerServices;
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

        public Aluno(int Matricula, string Nome, string CPF, DateTime Nascimento, EnumeradorDeSexo Sexo)
        {
            this.Matricula = Matricula;
            this.Nome = Nome;
            this.CPF = CPF;
            this.Nascimento = Nascimento;
            this.Sexo = Sexo;
        }

        public Aluno() { }

        public int Matricula
        {
            get => _matricula;
            set
            {
                if (value.ToString().Length > 9)
                    throw new ValidationException("Tamanho de matrícula excede o máximo (9).");
                else
                    _matricula = value;
            }
        }

        public string Nome
        {
            get => _nome;
            set
            {
                if(value == null)
                    throw new ValidationException("O nome deve ter pelo menos um caractere.");
                else if (value.Length > 100)
                    throw new ValidationException("Tamanho de nome deve ser menor ou igual a 100 caracteres.");
                else if (value.Length < 1)
                    throw new ValidationException("Tamanho de nome deve ser maior ou igual a 1.");
                else
                    _nome = value;
            }
        }

        public string CPF
        {
            get => FormatarCPF(_cpf);
            set
            {
                if (ValidaCpf(value))
                    _cpf = LimparCPF(value);
                else
                    throw new ValidationException("CPF inválido!");
            }
        }

        public DateTime Nascimento
        {
            get => _nascimento;
            set
            {
                if (value.CompareTo(DateTime.Today) < 0)
                    _nascimento = value;
                else
                    throw new ValidationException("Data deve ser anterior ao dia de hoje!");
            }
        }
        public EnumeradorDeSexo Sexo
        {
            get => _sexo;
            set => _sexo = value;
        }

        public override bool Equals(Object obj) => (obj is Aluno aluno && this.Matricula == aluno.Matricula);

        public override int GetHashCode() => this.Matricula;

        public override string ToString() => $"[Matrícula: {Matricula}], [Nome: \"{Nome}\"], " +
            $"[CPF: {FormatarCPF(CPF)}], [Nascimento: {Nascimento.ToShortDateString()}],  [Sexo: {Sexo}]";
    }
}
