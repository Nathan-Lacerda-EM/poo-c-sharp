using System;
using System.Collections.Generic;
using System.Text;

namespace Trabalho
{
    public class Pessoa
    {
        protected int Id { get; set; }
        protected string Nome { get; set; }
        protected int Idade { get; set; }

        public Pessoa() { }

        public Pessoa(int id, int idade, string nome)
        {
            this.Id = id;
            this.Nome = nome;
            this.Idade = idade;
        }

        /* 
         * É necessário mudar o método ToString(), já que o mesmo é Herança do Object.ToString(), 
         * que resultaria nesse caso Trabalho.Pessoa ao invés de escrever o estado do objeto como
         * está descrito abaixo.
         */
        public override string ToString() => $"[ID: {Id}], [Nome: {Nome}], [Idade: {Idade}]";

        /* 
         * Também é necessário mudar o Equals, já que o método de Herança do Object comparada o HashCode também.
         * Além disso, verificamos cada atributo para confirmar se realmente é igual.
         */
        public override bool Equals(Object o)
        {
            if (o == null)
                return false;

            /* 
             * Aqui foi utilizado uma checagem de tipo (as), para verificar se é possível fazer a conversão do
             * Objeto O para o Objeto Pessoa.
             */
            Pessoa p = o as Pessoa;
            if (p == null) //Se for null é porque alguma conversão ( não foi sucedida.
                return false;

            /* 
             * Aqui chamamos o Equals da classe base, ou seja o Object.Equals, para verificar se ambos os objetos
             * estão apontados para o mesmo espaço de memória (Que é definido pelo HashCode).
             */
            if (base.Equals(o))
                return true;

            /*
             * E aqui é verificado cada atributo do Objeto Pessoa.
             */
            return (this.Id == p.Id && this.Nome == p.Nome && this.Idade == p.Idade);
        }

        /*
         * Vamos dar "override" aqui também nos operadores de comparação, fazendo eles compararem com o Equals e 
         * caso dê false, ele vai comparar também todos os atributos do Objeto.
         */
        public static bool operator ==(Pessoa a, Pessoa b)
        {
            /* 
             * Aqui foi utilizado o is, para verificar se o Objeto Pessoa é um objeto, fazendo conversões de referência,
             * Boxing e Unboxing
             * if (((a is object) == null) || ((b is object) == null))
             */
            if (!(a is object) || !(b is object))
                return false;
            return object.Equals(a, b) || (a.Id == b.Id && a.Nome == b.Nome && a.Idade == b.Idade);
        }

        /*
         * O mesmo para comparar se não for os mesmos.
         */
        public static bool operator !=(Pessoa a, Pessoa b) => !(a == b);

        /* 
         * É necessário criar um HashCode novo, já que foi mudado o Equals também, uma sugestão é essa para que
         * nunca se repita e não tenha chance de conflitos com outros HashCodes, pegar os hashes de todos os
         * atributos do objeto e multiplicar por e multiplicar por um número primo. 
         */
        public override int GetHashCode() => this.Id.GetHashCode() + this.Nome.GetHashCode() + this.Idade.GetHashCode() * 11;
    }
}
