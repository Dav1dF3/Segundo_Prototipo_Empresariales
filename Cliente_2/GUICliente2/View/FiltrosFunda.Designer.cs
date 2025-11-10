namespace GUICliente2.View
{
    partial class FiltrosFunda
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
            this.cbNombre = new System.Windows.Forms.CheckBox();
            this.cbCGuitarra = new System.Windows.Forms.CheckBox();
            this.cbPrecioMin = new System.Windows.Forms.CheckBox();
            this.cbPrecioMax = new System.Windows.Forms.CheckBox();
            this.btnFiltros = new System.Windows.Forms.Button();
            this.textCGuitarra = new System.Windows.Forms.TextBox();
            this.textNombre = new System.Windows.Forms.TextBox();
            this.numPrecioMin = new System.Windows.Forms.NumericUpDown();
            this.numPrecioMax = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioMax)).BeginInit();
            this.SuspendLayout();
            // 
            // cbNombre
            // 
            this.cbNombre.AutoSize = true;
            this.cbNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNombre.Location = new System.Drawing.Point(13, 65);
            this.cbNombre.Name = "cbNombre";
            this.cbNombre.Size = new System.Drawing.Size(90, 24);
            this.cbNombre.TabIndex = 0;
            this.cbNombre.Text = "Nombre";
            this.cbNombre.UseVisualStyleBackColor = true;
            this.cbNombre.CheckedChanged += new System.EventHandler(this.cbNombre_CheckedChanged);
            // 
            // cbCGuitarra
            // 
            this.cbCGuitarra.AutoSize = true;
            this.cbCGuitarra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCGuitarra.Location = new System.Drawing.Point(13, 26);
            this.cbCGuitarra.Name = "cbCGuitarra";
            this.cbCGuitarra.Size = new System.Drawing.Size(149, 24);
            this.cbCGuitarra.TabIndex = 1;
            this.cbCGuitarra.Text = "Código Guitarra";
            this.cbCGuitarra.UseVisualStyleBackColor = true;
            this.cbCGuitarra.CheckedChanged += new System.EventHandler(this.cbCodigo_CheckedChanged);
            // 
            // cbPrecioMin
            // 
            this.cbPrecioMin.AutoSize = true;
            this.cbPrecioMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPrecioMin.Location = new System.Drawing.Point(13, 106);
            this.cbPrecioMin.Name = "cbPrecioMin";
            this.cbPrecioMin.Size = new System.Drawing.Size(111, 24);
            this.cbPrecioMin.TabIndex = 2;
            this.cbPrecioMin.Text = "Precio min";
            this.cbPrecioMin.UseVisualStyleBackColor = true;
            this.cbPrecioMin.CheckedChanged += new System.EventHandler(this.cbPrecioMin_CheckedChanged);
            // 
            // cbPrecioMax
            // 
            this.cbPrecioMax.AutoSize = true;
            this.cbPrecioMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPrecioMax.Location = new System.Drawing.Point(13, 147);
            this.cbPrecioMax.Name = "cbPrecioMax";
            this.cbPrecioMax.Size = new System.Drawing.Size(115, 24);
            this.cbPrecioMax.TabIndex = 3;
            this.cbPrecioMax.Text = "Precio max";
            this.cbPrecioMax.UseVisualStyleBackColor = true;
            this.cbPrecioMax.CheckedChanged += new System.EventHandler(this.cbPrecioMax_CheckedChanged);
            // 
            // btnFiltros
            // 
            this.btnFiltros.Location = new System.Drawing.Point(113, 194);
            this.btnFiltros.Name = "btnFiltros";
            this.btnFiltros.Size = new System.Drawing.Size(110, 37);
            this.btnFiltros.TabIndex = 6;
            this.btnFiltros.Text = "Aplicar filtros";
            this.btnFiltros.UseVisualStyleBackColor = true;
            this.btnFiltros.Click += new System.EventHandler(this.btnFiltros_Click);
            // 
            // textCGuitarra
            // 
            this.textCGuitarra.Enabled = false;
            this.textCGuitarra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCGuitarra.Location = new System.Drawing.Point(211, 24);
            this.textCGuitarra.Name = "textCGuitarra";
            this.textCGuitarra.Size = new System.Drawing.Size(159, 27);
            this.textCGuitarra.TabIndex = 7;
            // 
            // textNombre
            // 
            this.textNombre.Enabled = false;
            this.textNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNombre.Location = new System.Drawing.Point(211, 62);
            this.textNombre.Name = "textNombre";
            this.textNombre.Size = new System.Drawing.Size(159, 27);
            this.textNombre.TabIndex = 8;
            // 
            // numPrecioMin
            // 
            this.numPrecioMin.Enabled = false;
            this.numPrecioMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPrecioMin.Location = new System.Drawing.Point(211, 103);
            this.numPrecioMin.Name = "numPrecioMin";
            this.numPrecioMin.Size = new System.Drawing.Size(159, 27);
            this.numPrecioMin.TabIndex = 9;
            // 
            // numPrecioMax
            // 
            this.numPrecioMax.Enabled = false;
            this.numPrecioMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPrecioMax.Location = new System.Drawing.Point(211, 144);
            this.numPrecioMax.Name = "numPrecioMax";
            this.numPrecioMax.Size = new System.Drawing.Size(159, 27);
            this.numPrecioMax.TabIndex = 10;
            // 
            // FiltrosFunda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 244);
            this.Controls.Add(this.numPrecioMax);
            this.Controls.Add(this.numPrecioMin);
            this.Controls.Add(this.textNombre);
            this.Controls.Add(this.textCGuitarra);
            this.Controls.Add(this.btnFiltros);
            this.Controls.Add(this.cbPrecioMax);
            this.Controls.Add(this.cbPrecioMin);
            this.Controls.Add(this.cbCGuitarra);
            this.Controls.Add(this.cbNombre);
            this.Name = "FiltrosFunda";
            this.Text = "Filtros";
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbNombre;
        private System.Windows.Forms.CheckBox cbCGuitarra;
        private System.Windows.Forms.CheckBox cbPrecioMin;
        private System.Windows.Forms.CheckBox cbPrecioMax;
        private System.Windows.Forms.Button btnFiltros;
        private System.Windows.Forms.TextBox textCGuitarra;
        private System.Windows.Forms.TextBox textNombre;
        private System.Windows.Forms.NumericUpDown numPrecioMin;
        private System.Windows.Forms.NumericUpDown numPrecioMax;
    }
}