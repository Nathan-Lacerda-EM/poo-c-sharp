using EM.Domain;
using EM.Repository;
using System;
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
            // Can be used too: cboSexo.Items.AddRange(Enum.GetValues(typeof(EnumeradorDeSexo)));

            SetupDGVAlunos();
            AtualizeDataGridView();
        }

        private void btnAddModificar_Click(object sender, EventArgs e)
        {
            var erroCampos = EstaCorretoPreenchimentoFormulario();
            if (!erroCampos)
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

        private void ModifiqueAluno()
        {
            Aluno aluno = new Aluno
            {
                Matricula = int.Parse(txtMatricula.Text),
                Nome = txtNome.Text,
                Nascimento = DateTime.Parse(mtbNascimento.Text),
                Sexo = (EnumeradorDeSexo)cboSexo.SelectedItem
            };

            if (!ValideCpf(txtCPF.Text) && txtCPF.TextLength > 0)
            {
                MostreErroNaTelaDoUsuario("CPF Inválido!", "Modificação de aluno");
                return;
            }
            else if (txtCPF.TextLength == 0)
            {
                aluno.CPF = txtCPF.Text;
            }

            _repoAluno.Update(aluno);

            AtualizeDataGridView();
            AjusteEstadoControlesEmEdicao(false);

            MostreInformacaoNaTelaDoUsuario("Aluno modificado com sucesso!", "Modificação de aluno");
            txtPesquisa.Focus();
        }

        private void GetCPF(string CPF)
        {

        }

        private void MostreInformacaoNaTelaDoUsuario(string informacao, string tituloBox)
        {
            MessageBox.Show(informacao, tituloBox,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AdicioneAluno()
        {
            Aluno aluno = new Aluno
            {
                Matricula = int.Parse(txtMatricula.Text),
                Nome = txtNome.Text,
                Nascimento = DateTime.Parse(mtbNascimento.Text),
                Sexo = (EnumeradorDeSexo)cboSexo.SelectedItem
            };

            if (ValideCpf(txtCPF.Text) && txtCPF.TextLength > 0)
            {
                aluno.CPF = txtCPF.Text;
            }
            else if (txtCPF.TextLength == 0)
            {
                aluno.CPF = "";
            }
            else
            {
                MostreErroNaTelaDoUsuario("CPF Inválido!", "Cadastro de aluno");
                return;
            }

            try
            {
                _repoAluno.Add(aluno);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Aluno ou CPF já registrado!")
                {
                    MostreErroNaTelaDoUsuario("Aluno ou CPF já registrado!", "Cadastro de aluno");
                    return;
                }
            }

            AtualizeDataGridView();
            LimpeFormulario();

            MessageBox.Show("Aluno adicionado com sucesso!", "Cadastro de aluno",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtMatricula.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvAlunos.CurrentRow == null)
            {
                MostreErroNaTelaDoUsuario("Nenhum aluno foi selecionado.", "Edição de aluno");
                txtPesquisa.Focus();
                return;
            }

            AjusteCampos(
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

            DialogResult result = MessageBox.Show("Tem certeza que quer excluir este aluno?", "Exclusão de Aluno",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int matricula = Convert.ToInt32(dgvAlunos.CurrentRow.Cells["Matrícula"].Value);

                _repoAluno.Remove(_repoAluno.GetByMatricula(matricula));

                AtualizeDataGridView();

                MessageBox.Show("Aluno excluído!", "Exclusão de aluno",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPesquisa.Focus();
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
            if (DateTime.TryParse(mtbNascimento.Text, out DateTime dataDeNascimento) && mtbNascimento.TextLength == 10)
            {
                if (dataDeNascimento.CompareTo(DateTime.Now) > 0)
                {
                    MostreErroNaTelaDoUsuario("Data de nascimento não pode ser uma data futura!",
                        "Validação de data de nascimento");
                    mtbNascimento.ResetText();
                    mtbNascimento.Focus();
                }
                return;
            }
            MostreErroNaTelaDoUsuario("Data de nascimento inválida!", "Validação de data de nascimento");
            mtbNascimento.ResetText();
            mtbNascimento.Focus();
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
            catch (Exception exc)
            {
                if (exc.Message == "Não existe nenhum aluno no repositório!")
                {
                    _bs.DataSource = null;
                    return;
                }

                DialogResult result = MessageBox.Show(exc.Message + "\n\nVer erro completo?", "Erro desconhecido",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (result == DialogResult.Yes)
                {
                    new TelaErro(exc);
                }
            }
        }

        private void SetupDGVAlunos()
        {
            dgvAlunos.AutoGenerateColumns = false;
            dgvAlunos.DataSource = _bs;

            DataGridViewColumn clmMatricula = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Matricula",
                Name = "Matrícula"
            };
            dgvAlunos.Columns.Add(clmMatricula);

            DataGridViewColumn clmNome = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nome",
                Name = "Nome"
            };
            dgvAlunos.Columns.Add(clmNome);

            DataGridViewColumn clmSexo = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Sexo",
                Name = "Sexo"
            };
            dgvAlunos.Columns.Add(clmSexo);

            DataGridViewColumn clmNascimento = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nascimento",
                Name = "Nascimento"
            };
            dgvAlunos.Columns.Add(clmNascimento);

            DataGridViewColumn clmCPF = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CPF",
                Name = "CPF"
            };
            dgvAlunos.Columns.Add(clmCPF);

            dgvAlunos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvAlunos.Columns["Nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void AjusteCampos(string matricula, string nome, string cpf, int sexo, string nascimento)
        {
            txtMatricula.Text = matricula;
            txtNome.Text = nome;
            txtCPF.Text = LimparCPF(cpf);
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
            }
            else
            {
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

        private void MostreErroNaTelaDoUsuario(string erro, string tituloBox) => MessageBox.Show(erro, tituloBox,
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
