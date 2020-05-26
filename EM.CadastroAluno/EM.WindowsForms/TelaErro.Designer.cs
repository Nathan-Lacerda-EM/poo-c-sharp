using System.Windows.Forms;

namespace EM.WindowsForms
{
    partial class TelaErro
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaErro));
            this.rtxtErro = new System.Windows.Forms.RichTextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rtxtErro
            // 
            this.rtxtErro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtxtErro.Location = new System.Drawing.Point(103, 70);
            this.rtxtErro.Name = "rtxtErro";
            this.rtxtErro.ReadOnly = true;
            this.rtxtErro.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtxtErro.Size = new System.Drawing.Size(594, 329);
            this.rtxtErro.TabIndex = 0;
            this.rtxtErro.Text = "";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.Location = new System.Drawing.Point(359, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(94, 25);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Exception";
            // 
            // TelaErro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 437);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.rtxtErro);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TelaErro";
            this.Padding = new System.Windows.Forms.Padding(100, 20, 100, 35);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tela de Erro";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.RichTextBox rtxtErro;
        private System.Windows.Forms.Label lblTitulo;
    }
}