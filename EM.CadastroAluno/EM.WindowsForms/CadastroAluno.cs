using EM.Domain;
using static EM.Domain.Utils;
using EM.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

namespace EM.WindowsForms
{
    public partial class CadastroAluno : Form
    {
        RepositorioAluno repoAluno;
        BindingSource bs;

        public CadastroAluno()
        {
            InitializeComponent();

            /*
             * Aqui iniciamos os construtores do repositório, BindingSource, DataTable, ComboBox
             * além da DataGridView e algumas configurações.
             */
            IniciarControles();
        }

        private void IniciarControles()
        {
            repoAluno = new RepositorioAluno();
            bs = new BindingSource();

            cboSexo.Items.Add(EnumeradorDeSexo.Masculino);
            cboSexo.Items.Add(EnumeradorDeSexo.Feminino);

            SetupDGVAlunos();

            AtualizarDataGridView();
        }

        private void BtnAddModificar_Click(object sender, EventArgs e)
        {
            /*
             * Verificar se todos os campos respeitam os requisitos mínimos.
             */
            var strErroCampos = VerificarPreenchimentoCampos();
            if (strErroCampos == null)
            {
                /*
                 * Como estou utilizando o mesmo botão para adicionar e modificar os dados,
                 * faço a verificação do nome do botão por if mesmo e 
                 */
                if (btnAddModificar.Text.Equals("Adicionar"))
                {
                    Aluno aluno = new Aluno();
                    aluno.Matricula = int.Parse(txtMatricula.Text);
                    aluno.Nome = txtNome.Text;
                    if (ValidaCpf(txtCPF.Text) && txtCPF.TextLength > 0)
                        aluno.CPF = txtCPF.Text;
                    else if (txtCPF.TextLength == 0)
                        aluno.CPF = "";
                    else
                    {
                        MessageBox.Show("CPF Inválido!", "Cadastro de aluno",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    aluno.Nascimento = DateTime.Parse(mtbNascimento.Text);
                    aluno.Sexo = (EnumeradorDeSexo)cboSexo.SelectedItem;

                    try
                    {
                        repoAluno.Add(aluno);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Equals("Aluno ou CPF já registrado!"))
                        {
                            MessageBox.Show("Aluno ou CPF já registrado!", "Cadastro de aluno",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    AtualizarDataGridView();
                    LimparFormulario();

                    MessageBox.Show("Aluno adicionado com sucesso!", "Cadastro de aluno",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMatricula.Focus();
                }

                /*
                 * Botão de modificar, deixei else já que não é possível ser outro além de
                 * adicionar ou modificar, o código faz apenas essas duas alterações.
                 */
                else
                {
                    Aluno aluno = new Aluno();
                    aluno.Matricula = int.Parse(txtMatricula.Text);
                    aluno.Nome = txtNome.Text;
                    if (ValidaCpf(txtCPF.Text) && txtCPF.TextLength > 0)
                        aluno.CPF = txtCPF.Text;
                    else if (txtCPF.TextLength == 0)
                        aluno.CPF = "";
                    else
                    {
                        MessageBox.Show("CPF Inválido!", "Modificação de aluno",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    aluno.Nascimento = DateTime.Parse(mtbNascimento.Text);
                    aluno.Sexo = (EnumeradorDeSexo)cboSexo.SelectedItem;

                    repoAluno.Update(aluno);

                    AtualizarDataGridView();
                    AlterarEstadoControlesEmEdicao(false);

                    MessageBox.Show("Aluno modificado com sucesso!", "Modificação de aluno",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPesquisa.Focus();
                }
            }
            else
            {
                MessageBox.Show(strErroCampos, "Validação do cadastro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvAlunos.CurrentRow == null)
            {
                MessageBox.Show("Nenhum aluno foi selecionado.", "Edição de aluno",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPesquisa.Focus();
                return;
            }

            string matricula = Convert.ToString(dgvAlunos.CurrentRow.Cells["Matrícula"].Value);
            string nome = (string)dgvAlunos.CurrentRow.Cells["Nome"].Value;
            string cpf = (string)dgvAlunos.CurrentRow.Cells["CPF"].Value;
            int sexo = Convert.ToInt32(dgvAlunos.CurrentRow.Cells["Sexo"].Value);
            string nascimento = ((DateTime)dgvAlunos.CurrentRow.Cells["Nascimento"].Value).ToShortDateString();

            SetarCampos(matricula, nome, cpf, sexo, nascimento);
            AlterarEstadoControlesEmEdicao(true);
            txtPesquisa.Focus();
        }

        private void btnLimpaCancela_Click(object sender, EventArgs e)
        {
            if (btnLimpaCancela.Text.Equals("Limpar"))
                LimparFormulario();
            else
                AlterarEstadoControlesEmEdicao(false);
        }

        /* 
         * Fiz seguindo o protótipo, porém acredito que utilizando o TextChanged
         * ficaria melhor e mais confortável, já que não teria que clicar toda vez
         * no botão Pesquisar para atualizar a Grid.
         */
        private void txtPesquisar_TextChanged(object sender, EventArgs e)
        {
            if (txtPesquisa.TextLength > 0)
            {
                try
                {
                    if (int.TryParse(txtPesquisa.Text, out int inteiro))
                        bs.DataSource = repoAluno.GetByMatricula(inteiro);
                    else
                        bs.DataSource = repoAluno.GetByContendoNoNome(txtPesquisa.Text);

                }
                catch (Exception exc)
                {
                    if (exc.Message.Equals("Não existe nenhum aluno com esse nome!") ||
                        exc.Message.Equals("Não existe nenhum aluno com essa matrícula!") ||
                        exc.Message.Equals("Esse aluno não existe!"))
                        bs.DataSource = null;
                    else
                    {
                        var result = MessageBox.Show("Ver erro completo?", "Erro desconhecido",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                        /* 
                         * Aqui pode ser implementado um relatório de erro
                         * a ser enviado a equipe de desenvolvimento.
                         */
                        if (result == DialogResult.Yes)
                        {
                            new TelaErro(exc);
                        }
                    }
                }
            }
            else
                bs.DataSource = repoAluno.GetAll();
        }

        private void txtCadastro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnAddModificar_Click(this, new EventArgs());
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
                int matricula = Convert.ToInt32(dgvAlunos.CurrentRow.Cells["Matrícula"].Value);

                repoAluno.Remove(repoAluno.GetByMatricula(matricula));

                AtualizarDataGridView();

                MessageBox.Show("Aluno excluído!", "Exclusão de aluno",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPesquisa.Focus();
            }
        }

        private void mtbNascimento_Click(object sender, EventArgs e)
        {
            if (mtbNascimento.Text.Equals("  /  /"))
                mtbNascimento.Select(0, 0);
        }

        private void mtbNascimento_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"((0[1-9]|1[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");
            if (mtbNascimento.Text.Length == 10)
            {
                if (!regex.IsMatch(mtbNascimento.Text))
                {
                    MessageBox.Show("Data de nascimento não é válida!", "Validação de data de nascimento",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mtbNascimento.ResetText();
                    mtbNascimento.Focus();
                    mtbNascimento_Click(sender, e);
                    return;
                }

                if (DateTime.Parse(mtbNascimento.Text).CompareTo(DateTime.Now) > 0)
                {
                    MessageBox.Show("Data de nascimento não pode ser uma data futura!", "Validação de data de nascimento",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mtbNascimento.ResetText();
                    mtbNascimento.Focus();
                    mtbNascimento_Click(sender, e);
                }
            }
        }

        private void mtbNascimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (mtbNascimento.Text.Equals("  /  /"))
                mtbNascimento.Select(0, 0);

            if (char.IsControl(e.KeyChar) || (e.KeyChar >= 48 && e.KeyChar <= 57))
                return;

            e.Handled = true;
        }

        private void txtMatricula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || (e.KeyChar >= 48 && e.KeyChar <= 57))
                return;
            e.Handled = true;
        }

        private void txtCPF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || (e.KeyChar >= 48 && e.KeyChar <= 57) ||
                e.KeyChar == '.' || e.KeyChar == '-')
                return;
            e.Handled = true;
        }

        /*                                   MÉTODOS ÚTEIS
         * Os métodos abaixo são para melhorar a legibilidade e diminuir o tamanho do código.
         * São métodos utilizados em vários locais e separados melhoram a legibilidade,
         * facilitam em mudanças e deixa o código mais organizado.
         */

        public void AtualizarDataGridView()
        {
            try
            {
                bs.DataSource = repoAluno.GetAll();
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Não existe nenhum aluno no repositório!"))
                    bs.DataSource = null;
                else
                {
                    var result = MessageBox.Show("Ver erro completo?", "Erro desconhecido",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    /* 
                     * Aqui pode ser implementado um relatório de erro
                     * a ser enviado a equipe de desenvolvimento.
                     */
                    if (result == DialogResult.Yes)
                        new TelaErro(e);
                }
            }
        }

        public void SetupDGVAlunos()
        {
            dgvAlunos.AutoGenerateColumns = false;
            dgvAlunos.DataSource = bs;

            DataGridViewColumn clmMatricula = new DataGridViewTextBoxColumn();
            clmMatricula.DataPropertyName = "Matricula";
            clmMatricula.Name = "Matrícula";
            dgvAlunos.Columns.Add(clmMatricula);

            DataGridViewColumn clmNome = new DataGridViewTextBoxColumn();
            clmNome.DataPropertyName = "Nome";
            clmNome.Name = "Nome";
            dgvAlunos.Columns.Add(clmNome);

            DataGridViewColumn clmSexo = new DataGridViewTextBoxColumn();
            clmSexo.DataPropertyName = "Sexo";
            clmSexo.Name = "Sexo";
            dgvAlunos.Columns.Add(clmSexo);

            DataGridViewColumn clmNascimento = new DataGridViewTextBoxColumn();
            clmNascimento.DataPropertyName = "Nascimento";
            clmNascimento.Name = "Nascimento";
            dgvAlunos.Columns.Add(clmNascimento);

            DataGridViewColumn clmCPF = new DataGridViewTextBoxColumn();
            clmCPF.DataPropertyName = "CPF";
            clmCPF.Name = "CPF";
            dgvAlunos.Columns.Add(clmCPF);

            dgvAlunos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvAlunos.Columns["Nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void SetarCampos(string matricula, string nome, string cpf, int sexo, string nascimento)
        {
            txtMatricula.Text = matricula;
            txtNome.Text = nome;
            txtCPF.Text = LimparCPF(cpf);
            cboSexo.SelectedIndex = sexo;
            mtbNascimento.Text = nascimento;
        }

        private void AlterarEstadoControlesEmEdicao(bool estado)
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
                LimparFormulario();
            }
        }

        private void LimparFormulario()
        {
            txtMatricula.ResetText();
            txtNome.ResetText();
            txtCPF.ResetText();
            cboSexo.SelectedIndex = -1;
            mtbNascimento.ResetText();
        }

        public string VerificarPreenchimentoCampos()
        {
            if (!(txtMatricula.TextLength > 0))
            {
                txtMatricula.Focus();
                return "Matrícula deve ser maior que 0!";
            }
            else if (!(txtNome.TextLength > 0))
            {
                txtNome.Focus();
                return "Nome não pode estar vazio!";
            }
            else if (cboSexo.SelectedIndex == -1)
            {
                cboSexo.Focus();
                return "Selecione um sexo!";
            }
            else if (mtbNascimento.Text.Replace(" ", "").Length != 10)
            {
                mtbNascimento.Focus();
                return "Digite a data de nascimento completa!";
            }
            else
                return null;
        }
    }
}
