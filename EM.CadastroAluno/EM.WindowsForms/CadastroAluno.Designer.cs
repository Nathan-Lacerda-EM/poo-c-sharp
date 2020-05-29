using EM.Domain;
using System;
using System.Windows.Forms;

namespace EM.WindowsForms
{
    partial class CadastroAluno
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CadastroAluno));
            this.estadoCadastro = new System.Windows.Forms.Label();
            this.pnlCadastro = new System.Windows.Forms.Panel();
            this.lblSexo = new System.Windows.Forms.Label();
            this.btnAddModificar = new System.Windows.Forms.Button();
            this.btnLimpaCancela = new System.Windows.Forms.Button();
            this.lblCPF = new System.Windows.Forms.Label();
            this.cboSexo = new System.Windows.Forms.ComboBox();
            this.txtCPF = new System.Windows.Forms.TextBox();
            this.lblNascimento = new System.Windows.Forms.Label();
            this.mtbNascimento = new System.Windows.Forms.MaskedTextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.txtMatricula = new System.Windows.Forms.TextBox();
            this.lblMatricula = new System.Windows.Forms.Label();
            this.dgvAlunos = new System.Windows.Forms.DataGridView();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.txtPesquisa = new System.Windows.Forms.TextBox();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.pnlCadastro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlunos)).BeginInit();
            this.SuspendLayout();
            // 
            // estadoCadastro
            // 
            this.estadoCadastro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.estadoCadastro.Location = new System.Drawing.Point(10, 10);
            this.estadoCadastro.Margin = new System.Windows.Forms.Padding(0);
            this.estadoCadastro.Name = "estadoCadastro";
            this.estadoCadastro.Size = new System.Drawing.Size(90, 15);
            this.estadoCadastro.TabIndex = 1;
            this.estadoCadastro.Text = "Novo aluno";
            // 
            // pnlCadastro
            // 
            this.pnlCadastro.AutoScroll = true;
            this.pnlCadastro.AutoSize = true;
            this.pnlCadastro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCadastro.Controls.Add(this.lblSexo);
            this.pnlCadastro.Controls.Add(this.btnAddModificar);
            this.pnlCadastro.Controls.Add(this.btnLimpaCancela);
            this.pnlCadastro.Controls.Add(this.lblCPF);
            this.pnlCadastro.Controls.Add(this.cboSexo);
            this.pnlCadastro.Controls.Add(this.txtCPF);
            this.pnlCadastro.Controls.Add(this.lblNascimento);
            this.pnlCadastro.Controls.Add(this.mtbNascimento);
            this.pnlCadastro.Controls.Add(this.lblNome);
            this.pnlCadastro.Controls.Add(this.txtNome);
            this.pnlCadastro.Controls.Add(this.txtMatricula);
            this.pnlCadastro.Controls.Add(this.lblMatricula);
            this.pnlCadastro.Location = new System.Drawing.Point(13, 29);
            this.pnlCadastro.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.pnlCadastro.Name = "pnlCadastro";
            this.pnlCadastro.Padding = new System.Windows.Forms.Padding(9, 5, 5, 5);
            this.pnlCadastro.Size = new System.Drawing.Size(485, 110);
            this.pnlCadastro.TabIndex = 1;
            // 
            // lblSexo
            // 
            this.lblSexo.AutoSize = true;
            this.lblSexo.Location = new System.Drawing.Point(8, 59);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Size = new System.Drawing.Size(32, 15);
            this.lblSexo.TabIndex = 10;
            this.lblSexo.Text = "Sexo";
            // 
            // btnAddModificar
            // 
            this.btnAddModificar.Location = new System.Drawing.Point(400, 77);
            this.btnAddModificar.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.btnAddModificar.Name = "btnAddModificar";
            this.btnAddModificar.Size = new System.Drawing.Size(75, 23);
            this.btnAddModificar.TabIndex = 7;
            this.btnAddModificar.Text = "Adicionar";
            this.btnAddModificar.UseVisualStyleBackColor = true;
            this.btnAddModificar.Click += new System.EventHandler(this.BtnAddModificar_Click);
            // 
            // btnLimpaCancela
            // 
            this.btnLimpaCancela.Location = new System.Drawing.Point(323, 77);
            this.btnLimpaCancela.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.btnLimpaCancela.Name = "btnLimpaCancela";
            this.btnLimpaCancela.Size = new System.Drawing.Size(75, 23);
            this.btnLimpaCancela.TabIndex = 6;
            this.btnLimpaCancela.Text = "Limpar";
            this.btnLimpaCancela.UseVisualStyleBackColor = true;
            this.btnLimpaCancela.Click += new System.EventHandler(this.btnLimpaCancela_Click);
            // 
            // lblCPF
            // 
            this.lblCPF.AutoSize = true;
            this.lblCPF.Location = new System.Drawing.Point(200, 59);
            this.lblCPF.Name = "lblCPF";
            this.lblCPF.Size = new System.Drawing.Size(28, 15);
            this.lblCPF.TabIndex = 7;
            this.lblCPF.Text = "CPF";
            // 
            // cboSexo
            // 
            this.cboSexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSexo.FormattingEnabled = true;
            this.cboSexo.Location = new System.Drawing.Point(8, 77);
            this.cboSexo.Name = "cboSexo";
            this.cboSexo.Size = new System.Drawing.Size(100, 23);
            this.cboSexo.TabIndex = 3;
            this.cboSexo.KeyDown += new KeyEventHandler(this.txtCadastro_KeyDown);
            // 
            // txtCPF
            // 
            this.txtCPF.Location = new System.Drawing.Point(200, 77);
            this.txtCPF.Name = "txtCPF";
            this.txtCPF.Size = new System.Drawing.Size(117, 23);
            this.txtCPF.TabIndex = 5;
            this.txtCPF.KeyDown += new KeyEventHandler(this.txtCadastro_KeyDown);
            // 
            // lblNascimento
            // 
            this.lblNascimento.AutoSize = true;
            this.lblNascimento.Location = new System.Drawing.Point(114, 59);
            this.lblNascimento.Name = "lblNascimento";
            this.lblNascimento.Size = new System.Drawing.Size(71, 15);
            this.lblNascimento.TabIndex = 5;
            this.lblNascimento.Text = "Nascimento";
            // 
            // mtbNascimento
            // 
            this.mtbNascimento.Location = new System.Drawing.Point(114, 77);
            this.mtbNascimento.Mask = "00/00/0000";
            this.mtbNascimento.Name = "mtbNascimento";
            this.mtbNascimento.Size = new System.Drawing.Size(80, 23);
            this.mtbNascimento.TabIndex = 4;
            this.mtbNascimento.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.mtbNascimento_MaskInputRejected);
            this.mtbNascimento.Click += new System.EventHandler(this.mtbNascimento_Click);
            this.mtbNascimento.TextChanged += new System.EventHandler(this.mtbNascimento_TextChanged);
            this.mtbNascimento.KeyDown += new KeyEventHandler(this.txtCadastro_KeyDown);
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(114, 4);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(40, 15);
            this.lblNome.TabIndex = 3;
            this.lblNome.Text = "Nome";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(114, 23);
            this.txtNome.MaxLength = 100;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(361, 23);
            this.txtNome.TabIndex = 2;
            this.txtNome.KeyDown += new KeyEventHandler(this.txtCadastro_KeyDown);
            // 
            // txtMatricula
            // 
            this.txtMatricula.Location = new System.Drawing.Point(8, 23);
            this.txtMatricula.MaxLength = 9;
            this.txtMatricula.Name = "txtMatricula";
            this.txtMatricula.Size = new System.Drawing.Size(100, 23);
            this.txtMatricula.TabIndex = 1;
            this.txtMatricula.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMatricula.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMatricula_KeyPress);
            this.txtMatricula.KeyDown += new KeyEventHandler(this.txtCadastro_KeyDown);
            // 
            // lblMatricula
            // 
            this.lblMatricula.AutoSize = true;
            this.lblMatricula.Location = new System.Drawing.Point(8, 4);
            this.lblMatricula.Name = "lblMatricula";
            this.lblMatricula.Size = new System.Drawing.Size(57, 15);
            this.lblMatricula.TabIndex = 0;
            this.lblMatricula.Text = "Matrícula";
            // 
            // dgvAlunos
            // 
            this.dgvAlunos.AllowUserToAddRows = false;
            this.dgvAlunos.AllowUserToDeleteRows = false;
            this.dgvAlunos.AllowUserToResizeColumns = false;
            this.dgvAlunos.AllowUserToResizeRows = false;
            this.dgvAlunos.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvAlunos.Location = new System.Drawing.Point(13, 174);
            this.dgvAlunos.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.dgvAlunos.MultiSelect = false;
            this.dgvAlunos.Name = "dgvAlunos";
            this.dgvAlunos.ReadOnly = true;
            this.dgvAlunos.RowHeadersVisible = false;
            this.dgvAlunos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvAlunos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlunos.ShowCellToolTips = false;
            this.dgvAlunos.ShowEditingIcon = false;
            this.dgvAlunos.Size = new System.Drawing.Size(485, 125);
            this.dgvAlunos.TabIndex = 4;
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Location = new System.Drawing.Point(423, 148);
            this.btnPesquisa.Margin = new System.Windows.Forms.Padding(1, 3, 3, 0);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 3;
            this.btnPesquisa.Text = "Pesquisar";
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Location = new System.Drawing.Point(13, 148);
            this.txtPesquisa.Margin = new System.Windows.Forms.Padding(3, 3, 1, 2);
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.Size = new System.Drawing.Size(408, 23);
            this.txtPesquisa.TabIndex = 2;
            this.txtPesquisa.KeyDown += new KeyEventHandler(this.txtPesquisar_KeyDown);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Location = new System.Drawing.Point(423, 312);
            this.btnExcluir.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(75, 23);
            this.btnExcluir.TabIndex = 6;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(346, 312);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(75, 23);
            this.btnEditar.TabIndex = 5;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // CadastroAluno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 344);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.txtPesquisa);
            this.Controls.Add(this.btnPesquisa);
            this.Controls.Add(this.dgvAlunos);
            this.Controls.Add(this.pnlCadastro);
            this.Controls.Add(this.estadoCadastro);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(527, 383);
            this.Name = "CadastroAluno";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 10, 6);
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Alunos";
            this.pnlCadastro.ResumeLayout(false);
            this.pnlCadastro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlunos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label estadoCadastro;
        private System.Windows.Forms.Label lblMatricula;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.TextBox txtMatricula;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.MaskedTextBox mtbNascimento;
        private System.Windows.Forms.Panel pnlCadastro;
        private System.Windows.Forms.Label lblNascimento;
        private System.Windows.Forms.TextBox txtCPF;
        private System.Windows.Forms.ComboBox cboSexo;
        private System.Windows.Forms.Label lblCPF;
        private System.Windows.Forms.Button btnAddModificar;
        private System.Windows.Forms.Button btnLimpaCancela;
        private System.Windows.Forms.Label lblSexo;
        private System.Windows.Forms.DataGridView dgvAlunos;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.TextBox txtPesquisa;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnEditar;
    }
}

