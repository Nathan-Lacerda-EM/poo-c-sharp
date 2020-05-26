using EM.Domain;
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
            setupControls();
        }

        private void setupControls()
        {
            // DataGridView, Repository & Binding Source
            repoAluno = new RepositorioAluno();
            bs = new BindingSource();
            dt = new DataTable();

            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dt.Columns.Add("Matricula", typeof(string));
            dt.Columns.Add("Nome", typeof(string));
            dt.Columns.Add("Sexo", typeof(string));
            dt.Columns.Add("Nascimento", typeof(string));
            dt.Columns.Add("CPF", typeof(string));

            dgvAlunos.DataSource = bs;

            // ComboBox
            cboSexo.Items.Add(EnumeradorDeSexo.Masculino);
            cboSexo.Items.Add(EnumeradorDeSexo.Feminino);

            // Gerar as colunas.
            atualizarDGV();

            dgvAlunos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            dgvAlunos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnAddModificar_Click(object sender, EventArgs e)
        {
            if (verificarSeEstaTudoPreenchido())
            {
                if (btnAddModificar.Text.Equals("Adicionar"))
                {
                    Aluno aluno = new Aluno();
                    aluno.Matricula = int.Parse(txtMatricula.Text);
                    aluno.Nome = txtNome.Text;
                    if (EM.Domain.Utils.ValidaCpf(txtCPF.Text) && txtCPF.TextLength > 0)
                        aluno.CPF = txtCPF.Text;
                    else if (txtCPF.TextLength == 0)
                        aluno.CPF = "";
                    else
                    {
                        MessageBox.Show("CPF Inválido!");
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
                            MessageBox.Show("Aluno ou CPF já registrado!");
                            return;
                        }
                    }

                    atualizarDGV();
                    MessageBox.Show("Aluno adicionado com sucesso!");
                }
                else
                {
                    Aluno aluno = new Aluno();
                    aluno.Matricula = int.Parse(txtMatricula.Text);
                    aluno.Nome = txtNome.Text;
                    if (EM.Domain.Utils.ValidaCpf(txtCPF.Text) && txtCPF.TextLength > 0)
                        aluno.CPF = txtCPF.Text;
                    else if (txtCPF.TextLength == 0)
                        aluno.CPF = "";
                    else
                    {
                        MessageBox.Show("CPF Inválido!");
                        return;
                    }

                    aluno.Nascimento = DateTime.Parse(mtbNascimento.Text);
                    aluno.Sexo = (EnumeradorDeSexo)cboSexo.SelectedItem;

                    repoAluno.Update(aluno);

                    atualizarDGV();
                    MessageBox.Show("Aluno modificado com sucesso!");

                    txtMatricula.ResetText();
                    txtNome.ResetText();
                    txtCPF.ResetText();
                    cboSexo.ResetText();
                    mtbNascimento.ResetText();
                    txtMatricula.ReadOnly = false;
                    txtMatricula.Enabled = true;
                    estadoCadastro.Text = "Novo aluno";
                    btnLimpaCancela.Text = "Limpar";
                    btnAddModificar.Text = "Adicionar";
                }
            }
            else
            {
                MessageBox.Show("Algum dado está incorreto ou não foi preenchido!");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvAlunos.CurrentRow == null)
            {
                MessageBox.Show("Nenhum aluno foi selecionado.");
                return;
            }

            estadoCadastro.Text = "Editando aluno";
            btnLimpaCancela.Text = "Cancelar";
            btnAddModificar.Text = "Modificar";

            string matricula = Convert.ToString(dgvAlunos.CurrentRow.Cells["Matrícula"].Value);
            string nome = (string)dgvAlunos.CurrentRow.Cells["Nome"].Value;
            string cpf = (string)dgvAlunos.CurrentRow.Cells["CPF"].Value;
            int sexo = Convert.ToInt32(dgvAlunos.CurrentRow.Cells["Sexo"].Value);
            string nascimento = ((DateTime)dgvAlunos.CurrentRow.Cells["Nascimento"].Value).ToShortDateString();

            txtMatricula.Text = matricula;
            txtNome.Text = nome;
            txtCPF.Text = EM.Domain.Utils.LimparCPF(cpf);
            cboSexo.SelectedIndex = sexo;
            mtbNascimento.Text = nascimento;

            txtMatricula.ReadOnly = true;
            txtMatricula.Enabled = false;
        }

        private void btnLimpaCancela_Click(object sender, EventArgs e)
        {
            if (btnLimpaCancela.Text.Equals("Limpar"))
            {
                txtMatricula.ResetText();
                txtNome.ResetText();
                txtCPF.ResetText();
                cboSexo.ResetText();
                mtbNascimento.ResetText();

                MessageBox.Show("Formulário limpo!");
            }
            else
            {
                txtMatricula.ResetText();
                txtNome.ResetText();
                txtCPF.ResetText();
                cboSexo.ResetText();
                mtbNascimento.ResetText();
                txtMatricula.ReadOnly = false;
                txtMatricula.Enabled = true;
                estadoCadastro.Text = "Novo aluno";
                btnLimpaCancela.Text = "Limpar";
                btnAddModificar.Text = "Adicionar";

                MessageBox.Show("Edição cancelada!");
            }
        }

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
                MessageBox.Show("Nenhum aluno foi selecionado.");
                return;
            }

            int matricula = Convert.ToInt32(dgvAlunos.CurrentRow.Cells["Matrícula"].Value);
            repoAluno.Remove(repoAluno.GetByMatricula(matricula));

            atualizarDGV();
            MessageBox.Show("Aluno excluído!");
        }

        private void mtbNascimento_Click(object sender, EventArgs e)
        {
            if (mtbNascimento.Text.Equals("  /  /"))
                mtbNascimento.Select(0, 0);
        }

        private void mtbNascimento_Validate(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"((0[1-9]|1[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");
            if (mtbNascimento.Text.Length == 10)
            {
                if (!regex.IsMatch(mtbNascimento.Text))
                {
                    MessageBox.Show("Data de nascimento não é válida!");
                    mtbNascimento.ResetText();
                    return;
                }

                if (DateTime.Parse(mtbNascimento.Text).CompareTo(DateTime.Now) > 0)
                {
                    MessageBox.Show("Data de nascimento não pode ser uma data futura!");
                    mtbNascimento.ResetText();
                }
            }
        }

        private void mtbNascimento_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            MessageBox.Show("Digite apenas números na data de nascimento!");
        }

        public bool verificarSeEstaTudoPreenchido()
        {
            if (txtNome.TextLength > 0 && txtMatricula.TextLength > 0 &&
                (cboSexo.SelectedIndex != -1) &&
                mtbNascimento.TextLength == 10)
                return true;
            else
                return false;
        }

        public void atualizarDGV()
        {
            try
            {
                DataTable dataTable = ConvertListToDataTable<Aluno>(repoAluno.GetAll().ToList());

                if (repoAluno.GetAll().Count() < 4)
                    for (int i = 0; i < 4 - repoAluno.GetAll().Count(); i++)
                        dataTable.NewRow();

                bs.RemoveFilter();

                bs.DataSource = dataTable;

                dataTable.Columns["Matricula"].ColumnName = "Matrícula";

                dgvAlunos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            }
            catch (Exception e)
            {
                if (e.Message.Equals("Não existe nenhum aluno no repositório!"))
                {
                    dt = CreateTable<Aluno>();
                    bs.DataSource = dt;
                    dt.Columns["Matricula"].ColumnName = "Matrícula";
                }
            }
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
