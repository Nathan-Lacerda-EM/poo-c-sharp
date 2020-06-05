using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;
using System.IO;

namespace EM.Repository
{
    public class GerenciadorBancoDeDados
    {
        private FbConnectionStringBuilder _fbConnectionStringBuilder;
        private string _arquivoBancoDeDados;
        private static GerenciadorBancoDeDados _instancia;
        public static bool bancoDeTeste = false;

        public static GerenciadorBancoDeDados GetInstancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new GerenciadorBancoDeDados();
                }
                return _instancia;
            }
        }

        public FbConnection GetConexao { get; } = new FbConnection();

        private GerenciadorBancoDeDados()
        {
            IniciarBancoDeDados();
        }

        private void IniciarBancoDeDados()
        {
            _arquivoBancoDeDados = bancoDeTeste ? @"E:\Bancos\CADASTROALUNOTESTE.fdb" : @"E:\Bancos\CADASTROALUNO.fdb";

            _fbConnectionStringBuilder = new FbConnectionStringBuilder
            {
                UserID = "SYSDBA",
                Password = "masterkey",
                DataSource = "localhost",
                Database = _arquivoBancoDeDados,
                Charset = "ISO8859_1"
            };

            GetConexao.ConnectionString = _fbConnectionStringBuilder.ToString();
            CrieBancoETabelaAlunoSeNaoExistir();
        }

        private void CrieBancoETabelaAlunoSeNaoExistir()
        {
            if (File.Exists(_arquivoBancoDeDados))
            {
                return;
            }

            FbConnection.CreateDatabase(_fbConnectionStringBuilder.ToString(), false);

            var criarTabela =
                "CREATE TABLE TBALUNO(MATRIC INTEGER NOT NULL, " +
                "NOME VARCHAR(100) NOT NULL, " +
                "CPF VARCHAR(14), " +
                "NASC TIMESTAMP NOT NULL, " +
                "SEXO INTEGER NOT NULL, " +
                "CONSTRAINT PK_ALUNO PRIMARY KEY(MATRIC));";

            ExecuteComando(criarTabela);
        }

        public void ExecuteComando(string comando)
        {
            GetConexao.Open();
            using (var fbTransaction = GetConexao.BeginTransaction())
            {
                var fbCommand = new FbCommand(comando, GetConexao, fbTransaction)
                {
                    CommandText = comando
                };
                fbCommand.ExecuteNonQuery();
                fbTransaction.Commit();
            }
            GetConexao.Close();
        }

        public void ExecuteComandoComParametro(string comando, Dictionary<string, object> fbParameters)
        {
            GetConexao.Open();
            using (var fbTransaction = GetConexao.BeginTransaction())
            {
                var fbCommand = new FbCommand(comando, GetConexao, fbTransaction)
                {
                    CommandText = comando
                };

                foreach (var param in fbParameters)
                {
                    fbCommand.Parameters.AddWithValue(param.Key, param.Value);
                }
                fbCommand.ExecuteNonQuery();
                fbTransaction.Commit();
            }
            GetConexao.Close();
        }

        public FbDataReader ExecuteComandoRetornandoReader(string comando)
        {
            GetConexao.Open();
            var fbTransaction = GetConexao.BeginTransaction();
            var fbCommand = new FbCommand(comando, GetConexao, fbTransaction)
            {
                CommandText = comando
            };

            var fbDataReader = fbCommand.ExecuteReader();

            return fbDataReader;
        }

        public void RemovaBancoDeTestes() => File.Delete(_arquivoBancoDeDados);

        public void EsvaziarBancoDeTestes() => ExecuteComando("DELETE FROM TBALUNO;");
    }
}
