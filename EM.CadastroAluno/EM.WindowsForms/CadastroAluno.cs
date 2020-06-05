using EM.Domain;
using EM.Repository;
using System;
using System.Data;
using System.Windows.Forms;
using static EM.Domain.Utils;

namespace EM.WindowsForms
{
    public partial class CadastroAluno : Form
    {
        private readonly RepositorioAluno _repoAluno = new RepositorioAluno();
        private readonly BindingSource _bs = new BindingSource();

        public CadastroAluno()
        {
            InitializeComponent();
            IniciarControles();
        }

        private void IniciarControles()
        {
            cboSexo.Items.Add(EnumeradorDeSexo.Masculino);
            cboSexo.Items.Add(EnumeradorDeSexo.Feminino);
            // Pode ser usado também: cboSexo.Items.AddRange(Enum.GetValues(typeof(EnumeradorDeSexo)));

            ConfigureDataGridView();
            AtualizeDataGridView();
        }

        private void btnAddModificar_Click(object sender, EventArgs e)
        {
            if (!EstaCorretoPreenchimentoFormulario())
            {
                return;
            }

            if (btnAddModificar.Text != "Adicionar")
            {
                ModifiqueAluno();
                return;
            }

            AdicioneAluno();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvAlunos.CurrentRow == null)
            {
                MostreErroNaTelaDoUsuario("Nenhum aluno foi selecionado.", "Edição de aluno");
                txtPesquisa.Focus();
                return;
            }

            AjusteCamposFormulario(
                Convert.ToString(dgvAlunos.CurrentRow.Cells["Matrícula"].Value),
                (string)dgvAlunos.CurrentRow.Cells["Nome"].Value,
                (string)dgvAlunos.CurrentRow.Cells["CPF"].Value,
                Convert.ToInt32(dgvAlunos.CurrentRow.Cells["Sexo"].Value),
                ((DateTime)dgvAlunos.CurrentRow.Cells["Nascimento"].Value).ToShortDateString()
            );

            AjusteEstadoControlesEmEdicao(true);
        }

        private void btnLimpaCancela_Click(object sender, EventArgs e)
        {
            if (btnLimpaCancela.Text != "Limpar")
            {
                AjusteEstadoControlesEmEdicao(false);
                return;
            }

            LimpeFormulario();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtPesquisa.TextLength == 0)
            {
                AtualizeDataGridView();
                return;
            }

            try
            {
                if (int.TryParse(txtPesquisa.Text, out int inteiro))
                {
                    _bs.DataSource = _repoAluno.GetByMatricula(inteiro);
                    return;
                }
                _bs.DataSource = _repoAluno.GetByContendoNoNome(txtPesquisa.Text);
            }
            catch (Exception exc)
            {
                if (exc.Message == "Não existe nenhum aluno com esse nome!" ||
                    exc.Message == "Não existe nenhum aluno com essa matrícula!" ||
                    exc.Message == "Não existe nenhum aluno no repositório!")
                {
                    _bs.DataSource = null;
                    return;
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvAlunos.CurrentRow == null)
            {
                MessageBox.Show("Nenhum aluno foi selecionado.", "Exclusão de Aluno",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Tem certeza que quer excluir este aluno?", "Exclusão de Aluno",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var matricula = Convert.ToInt32(dgvAlunos.CurrentRow.Cells["Matrícula"].Value);

                _repoAluno.Remove(_repoAluno.GetByMatricula(matricula));

                AtualizeDataGridView();

                MostreInformacaoNaTelaDoUsuario("Aluno excluído!", "Exclusão de aluno");
            }
        }

        private void dgvAlunos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar_Click(sender, e);
        }

        private void mtbNascimento_Click(object sender, EventArgs e)
        {
            if (mtbNascimento.Text == "  /  /")
            {
                mtbNascimento.Select(0, 0);
            }
        }

        private void mtbNascimento_TextChanged(object sender, EventArgs e)
        {
            if (mtbNascimento.Text.Replace(" ", "").Length == 10)
            {
                var data = DateTime.TryParse(mtbNascimento.Text, out DateTime dataDeNascimento);

                if (!data)
                {
                    MostreErroNaTelaDoUsuario("Data de nascimento inválida!", "Validação de data de nascimento");
                    mtbNascimento.ResetText();
                    mtbNascimento.Focus();
                    return;
                }

                if (dataDeNascimento.CompareTo(DateTime.Now) > 0)
                {
                    MostreErroNaTelaDoUsuario("Data de nascimento não pode ser uma data futura!",
                        "Validação de data de nascimento");
                    mtbNascimento.ResetText();
                    mtbNascimento.Focus();
                }
            }
        }

        private void txtPesquisar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPesquisar_Click(this, new EventArgs());
            }
        }

        private void txtCadastro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddModificar_Click(this, new EventArgs());
            }
        }

        private void dgvAlunos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                btnExcluir_Click(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnEditar_Click(this, new EventArgs());
            }
        }

        private void mtbNascimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (mtbNascimento.Text == "  /  /")
            {
                mtbNascimento.Select(0, 0);
            }

            if (char.IsControl(e.KeyChar) || (e.KeyChar >= 48 && e.KeyChar <= 57))
            {
                return;
            }

            e.Handled = true;
        }

        private void txtMatricula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || (e.KeyChar >= 48 && e.KeyChar <= 57))
            {
                return;
            }

            e.Handled = true;
        }

        private void txtCPF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || (e.KeyChar >= 48 && e.KeyChar <= 57) ||
                e.KeyChar == '.' || e.KeyChar == '-')
            {
                return;
            }

            e.Handled = true;
        }

        private void AtualizeDataGridView()
        {
            try
            {
                _bs.DataSource = _repoAluno.GetAll();
            }
            catch (Exception excecao)
            {
                if (excecao.Message == "Não existe nenhum aluno no repositório!")
                {
                    _bs.DataSource = null;

                    var conexao = GerenciadorBancoDeDados.GetInstancia.GetConexao;
                    if (conexao.State != ConnectionState.Closed)
                    {
                        conexao.Close();
                    }

                    return;
                }

                var resultadoMessageBox = MessageBox.Show(excecao.Message + "\n\nVer erro completo?", "Erro desconhecido",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (resultadoMessageBox == DialogResult.Yes)
                {
                    var telaErro = new TelaErro(excecao);
                    telaErro.Show();
                }
            }
        }

        private void ConfigureDataGridView()
        {
            dgvAlunos.AutoGenerateColumns = false;
            dgvAlunos.DataSource = _bs;

            CrieColunaNaDataGridView("Matrícula", nameof(Aluno.Matricula));
            CrieColunaNaDataGridView("Nome", nameof(Aluno.Nome));
            CrieColunaNaDataGridView("Sexo", nameof(Aluno.Sexo));
            CrieColunaNaDataGridView("Nascimento", nameof(Aluno.Nascimento));
            CrieColunaNaDataGridView("CPF", nameof(Aluno.CPF));

            dgvAlunos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvAlunos.Columns["Nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void CrieColunaNaDataGridView(string nomeDaColuna, string nomeDaPropriedade)
        {
            dgvAlunos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nomeDaColuna,
                DataPropertyName = nomeDaPropriedade
            });
        }

        private void AjusteCamposFormulario(string matricula, string nome, string cpf, int sexo, string nascimento)
        {
            txtMatricula.Text = matricula;
            txtNome.Text = nome;
            txtCPF.Text = LimpeCPF(cpf);
            cboSexo.SelectedIndex = sexo;
            mtbNascimento.Text = nascimento;
        }

        private void AjusteEstadoControlesEmEdicao(bool estado)
        {
            if (estado)
            {
                estadoCadastro.Text = "Editando aluno";
                btnLimpaCancela.Text = "Cancelar";
                btnAddModificar.Text = "Modificar";
                txtMatricula.Enabled = false;
                txtPesquisa.Enabled = false;
                btnExcluir.Enabled = false;
                btnEditar.Enabled = false;
                btnPesquisa.Enabled = false;
                return;
            }

            estadoCadastro.Text = "Novo aluno";
            btnLimpaCancela.Text = "Limpar";
            btnAddModificar.Text = "Adicionar";
            txtMatricula.Enabled = true;
            txtPesquisa.Enabled = true;
            btnExcluir.Enabled = true;
            btnEditar.Enabled = true;
            btnPesquisa.Enabled = true;
            LimpeFormulario();
        }

        private void LimpeFormulario()
        {
            txtMatricula.ResetText();
            txtNome.ResetText();
            txtCPF.ResetText();
            cboSexo.SelectedIndex = -1;
            mtbNascimento.ResetText();
        }

        public bool EstaCorretoPreenchimentoFormulario()
        {
            if (!(txtMatricula.TextLength > 0))
            {
                MostreErroNaTelaDoUsuario("Preencha o campo matrícula!", "Validação do cadastro");
                txtMatricula.Focus();
                return false;
            }
            else if (!(txtNome.TextLength > 0))
            {
                MostreErroNaTelaDoUsuario("Preencha o campo nome!", "Validação do cadastro");
                txtNome.Focus();
                return false;
            }
            else if (cboSexo.SelectedIndex == -1)
            {
                MostreErroNaTelaDoUsuario("Selecione um sexo!", "Validação do cadastro");
                cboSexo.Focus();
                return false;
            }
            else if (mtbNascimento.Text.Replace(" ", "").Length != 10)
            {
                MostreErroNaTelaDoUsuario("Digite uma data de nascimento completa!", "Validação do cadastro");
                mtbNascimento.Focus();
                return false;
            }

            return true;
        }

        private bool ValideCPF(string CPF)
        {
            if (EhCPFValido(txtCPF.Text) && txtCPF.TextLength > 0)
            {
                return true;
            }
            else if (txtCPF.TextLength == 0)
            {
                return true;
            }

            return false;
        }

        private Aluno CrieObjetoAluno(string matricula, string nome, string cpf, string dataDeNascimento, object sexo)
        {
            return new Aluno
            {
                Matricula = Convert.ToInt32(matricula),
                Nome = nome,
                CPF = cpf,
                Nascimento = Convert.ToDateTime(dataDeNascimento),
                Sexo = (EnumeradorDeSexo)sexo
            };
        }

        private void AdicioneAluno()
        {
            var cpf = txtCPF.Text;
            if (!ValideCPF(cpf))
            {
                MostreErroNaTelaDoUsuario("CPF Inválido!", "Cadastro de aluno");
                return;
            }

            var aluno = CrieObjetoAluno(txtMatricula.Text, txtNome.Text, cpf, mtbNascimento.Text, cboSexo.SelectedItem);

            try
            {
                _repoAluno.Add(aluno);
            }
            catch (Exception excecao)
            {
                if (excecao.Message == "Aluno ou CPF já registrado!")
                {
                    MostreErroNaTelaDoUsuario("Aluno ou CPF já registrado!", "Cadastro de aluno");
                    return;
                }
            }

            AtualizeDataGridView();
            LimpeFormulario();

            MostreInformacaoNaTelaDoUsuario("Aluno adicionado com sucesso!", "Cadastro de aluno");
            txtMatricula.Focus();
        }

        private void ModifiqueAluno()
        {
            var cpf = txtCPF.Text;
            if (!ValideCPF(cpf))
            {
                MostreErroNaTelaDoUsuario("CPF Inválido!", "Modificação de aluno");
                return;
            }

            var aluno = CrieObjetoAluno(txtMatricula.Text, txtNome.Text, cpf, mtbNascimento.Text, cboSexo.SelectedItem);

            _repoAluno.Update(aluno);

            AtualizeDataGridView();
            AjusteEstadoControlesEmEdicao(false);

            MostreInformacaoNaTelaDoUsuario("Aluno modificado com sucesso!", "Modificação de aluno");
        }

        private void MostreErroNaTelaDoUsuario(string erro, string tituloBox)
        {
            MessageBox.Show(erro, tituloBox, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MostreInformacaoNaTelaDoUsuario(string informacao, string tituloBox)
        {
            MessageBox.Show(informacao, tituloBox, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
