using System;
using System.Collections.Generic;
using System.Text;

namespace Trabalho
{
    public class Funcionario : Pessoa
    {
        private double Salario { get; set; }
        private string Cargo { get; set; }

        public Funcionario() { }

        public Funcionario(double salario, string cargo) {
            Salario = salario;
            Cargo = cargo;
        }

        public Funcionario(int id, int idade, string nome, double salario, string cargo)
        {
            Id = id;
            Nome = nome;
            Idade = idade;
            Salario = salario;
            Cargo = cargo;
        }
    }
}
