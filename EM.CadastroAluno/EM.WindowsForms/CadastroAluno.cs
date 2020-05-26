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

namespace EM.WindowsForms
{
    public partial class CadastroAluno : Form
    {
        RepositorioAluno repoAluno;
        BindingSource bs;
        DataTable dt;

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
            // DataGridView, Repository, Binding Source & DataTable.
            repoAluno = new RepositorioAluno();
            bs = new BindingSource();
            dt = new DataTable();
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;

            // Adicionar os dois sexos na ComboBox.
            cboSexo.Items.Add(EnumeradorDeSexo.Masculino);
            cboSexo.Items.Add(EnumeradorDeSexo.Feminino);

            /* Gerar as colunas vazias de acordo com os atributos do objeto Aluno.
             * E já deixar o método preparado caso for colocar um banco de dados.
             */
            AtualizarDataGridView();

            // Setar a DataSource da DGV para receber dados da Binding Source.
            dgvAlunos.DataSource = bs;
        }

        private void BtnAddModificar_Click(object sender, EventArgs e)
        {
            /*
             * Verificar se todos os campos respeitam os requisitos mínimos.
             */
            if (VerificarTodosOsCampos())
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
                }
            }
            else
            {
                MessageBox.Show("Algum dado está incorreto ou não foi preenchido!", "Cadastro de aluno",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvAlunos.CurrentRow == null)
            {
                MessageBox.Show("Nenhum aluno foi selecionado.", "Edição de aluno",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string matricula = Convert.ToString(dgvAlunos.CurrentRow.Cells["Matrícula"].Value);
            string nome = (string)dgvAlunos.CurrentRow.Cells["Nome"].Value;
            string cpf = (string)dgvAlunos.CurrentRow.Cells["CPF"].Value;
            int sexo = Convert.ToInt32(dgvAlunos.CurrentRow.Cells["Sexo"].Value);
            string nascimento = ((DateTime)dgvAlunos.CurrentRow.Cells["Nascimento"].Value).ToShortDateString();

            SetarCampos(matricula, nome, cpf, sexo, nascimento);
            AlterarEstadoControlesEmEdicao(true);
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
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtPesquisa.TextLength > 0)
            {
                bs.Filter = "Nome LIKE '%" + txtPesquisa.Text + "%'";
            }
            else
            {
                bs.RemoveFilter();
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
                int matricula = Convert.ToInt32(dgvAlunos.CurrentRow.Cells["Matrícula"].Value);

                repoAluno.Remove(repoAluno.GetByMatricula(matricula));

                AtualizarDataGridView();

                MessageBox.Show("Aluno excluído!", "Exclusão de aluno",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    mtbNascimento_Click(sender, e);
                    return;
                }

                if (DateTime.Parse(mtbNascimento.Text).CompareTo(DateTime.Now) > 0)
                {
                    MessageBox.Show("Data de nascimento não pode ser uma data futura!", "Validação de data de nascimento",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mtbNascimento.ResetText();
                    mtbNascimento_Click(sender, e);
                }
            }
        }

        private void mtbNascimento_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            MessageBox.Show("Digite apenas números na data de nascimento!", "Validação de data de nascimento",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            mtbNascimento_Click(sender, e);
        }

        /* Restringir apenas números quando o usuário estiver digitando a matrícula */
        private void txtMatricula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || (e.KeyChar >= 48 && e.KeyChar <= 57))
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
                dt = ConvertListToDataTable<Aluno>(repoAluno.GetAll().ToList());

                dt.Columns["Matricula"].ColumnName = "Matrícula";

                bs.RemoveFilter();
                bs.DataSource = dt;

                dgvAlunos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgvAlunos.Columns["Nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Não existe nenhum aluno no repositório!"))
                {
                    dt = CreateTable<Aluno>();
                    bs.DataSource = dt;
                    dt.Columns["Matricula"].ColumnName = "Matrícula";
                }
                else
                {
                    var result = MessageBox.Show(e.Message, "Erro desconhecido",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    /* 
                     * Aqui pode ser implementado um relatório de erro
                     * a ser enviado a equipe de desenvolvimento.
                     */
                    if (result == DialogResult.OK)
                    {
                        new TelaErro(e);
                    }
                }
            }
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
                btnPesquisa.Enabled = false;
            } else
            {
                estadoCadastro.Text = "Novo aluno";
                btnLimpaCancela.Text = "Limpar";
                btnAddModificar.Text = "Adicionar";
                txtMatricula.Enabled = true;
                txtPesquisa.Enabled = true;
                btnExcluir.Enabled = true;
                btnEditar.Enabled = true;
                btnPesquisa.Enabled = true;
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

        public bool VerificarTodosOsCampos()
        {
            if (txtNome.TextLength > 0 && txtMatricula.TextLength > 0 &&
                (cboSexo.SelectedIndex != -1) &&
                mtbNascimento.TextLength == 10)
                return true;
            else
                return false;
        }

        public static DataTable ConvertListToDataTable<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            return table;
        }
    }
}
