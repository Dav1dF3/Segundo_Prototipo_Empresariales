namespace GUICliente2.View
{
    partial class Filtros
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
            this.cbMarca = new System.Windows.Forms.CheckBox();
            this.cbPrecioMin = new System.Windows.Forms.CheckBox();
            this.cbPrecioMax = new System.Windows.Forms.CheckBox();
            this.cbStockMin = new System.Windows.Forms.CheckBox();
            this.cbStockMax = new System.Windows.Forms.CheckBox();
            this.btnFiltros = new System.Windows.Forms.Button();
            this.textNombre = new System.Windows.Forms.TextBox();
            this.textMarca = new System.Windows.Forms.TextBox();
            this.numPrecioMin = new System.Windows.Forms.NumericUpDown();
            this.numPrecioMax = new System.Windows.Forms.NumericUpDown();
            this.numStockMin = new System.Windows.Forms.NumericUpDown();
            this.numStockMax = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStockMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStockMax)).BeginInit();
            this.SuspendLayout();
            // 
            // cbNombre
            // 
            this.cbNombre.AutoSize = true;
            this.cbNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNombre.Location = new System.Drawing.Point(13, 26);
            this.cbNombre.Name = "cbNombre";
            this.cbNombre.Size = new System.Drawing.Size(90, 24);
            this.cbNombre.TabIndex = 0;
            this.cbNombre.Text = "Nombre";
            this.cbNombre.UseVisualStyleBackColor = true;
            this.cbNombre.CheckedChanged += new System.EventHandler(this.cbNombre_CheckedChanged);
            // 
            // cbMarca
            // 
            this.cbMarca.AutoSize = true;
            this.cbMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMarca.Location = new System.Drawing.Point(13, 66);
            this.cbMarca.Name = "cbMarca";
            this.cbMarca.Size = new System.Drawing.Size(78, 24);
            this.cbMarca.TabIndex = 1;
            this.cbMarca.Text = "Marca";
            this.cbMarca.UseVisualStyleBackColor = true;
            this.cbMarca.CheckedChanged += new System.EventHandler(this.cbMarca_CheckedChanged);
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
            // cbStockMin
            // 
            this.cbStockMin.AutoSize = true;
            this.cbStockMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStockMin.Location = new System.Drawing.Point(13, 187);
            this.cbStockMin.Name = "cbStockMin";
            this.cbStockMin.Size = new System.Drawing.Size(105, 24);
            this.cbStockMin.TabIndex = 4;
            this.cbStockMin.Text = "Stock min";
            this.cbStockMin.UseVisualStyleBackColor = true;
            this.cbStockMin.CheckedChanged += new System.EventHandler(this.cbStockMin_CheckedChanged);
            // 
            // cbStockMax
            // 
            this.cbStockMax.AutoSize = true;
            this.cbStockMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStockMax.Location = new System.Drawing.Point(13, 226);
            this.cbStockMax.Name = "cbStockMax";
            this.cbStockMax.Size = new System.Drawing.Size(109, 24);
            this.cbStockMax.TabIndex = 5;
            this.cbStockMax.Text = "Stock max";
            this.cbStockMax.UseVisualStyleBackColor = true;
            this.cbStockMax.CheckedChanged += new System.EventHandler(this.cbStockMax_CheckedChanged);
            // 
            // btnFiltros
            // 
            this.btnFiltros.Location = new System.Drawing.Point(112, 269);
            this.btnFiltros.Name = "btnFiltros";
            this.btnFiltros.Size = new System.Drawing.Size(110, 37);
            this.btnFiltros.TabIndex = 6;
            this.btnFiltros.Text = "Aplicar filtros";
            this.btnFiltros.UseVisualStyleBackColor = true;
            this.btnFiltros.Click += new System.EventHandler(this.btnFiltros_Click);
            // 
            // textNombre
            // 
            this.textNombre.Enabled = false;
            this.textNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNombre.Location = new System.Drawing.Point(164, 23);
            this.textNombre.Name = "textNombre";
            this.textNombre.Size = new System.Drawing.Size(159, 27);
            this.textNombre.TabIndex = 7;
            // 
            // textMarca
            // 
            this.textMarca.Enabled = false;
            this.textMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMarca.Location = new System.Drawing.Point(164, 63);
            this.textMarca.Name = "textMarca";
            this.textMarca.Size = new System.Drawing.Size(159, 27);
            this.textMarca.TabIndex = 8;
            // 
            // numPrecioMin
            // 
            this.numPrecioMin.Enabled = false;
            this.numPrecioMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPrecioMin.Location = new System.Drawing.Point(164, 103);
            this.numPrecioMin.Name = "numPrecioMin";
            this.numPrecioMin.Size = new System.Drawing.Size(159, 27);
            this.numPrecioMin.TabIndex = 9;
            // 
            // numPrecioMax
            // 
            this.numPrecioMax.Enabled = false;
            this.numPrecioMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPrecioMax.Location = new System.Drawing.Point(164, 144);
            this.numPrecioMax.Name = "numPrecioMax";
            this.numPrecioMax.Size = new System.Drawing.Size(159, 27);
            this.numPrecioMax.TabIndex = 10;
            // 
            // numStockMin
            // 
            this.numStockMin.Enabled = false;
            this.numStockMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numStockMin.Location = new System.Drawing.Point(164, 184);
            this.numStockMin.Name = "numStockMin";
            this.numStockMin.Size = new System.Drawing.Size(159, 27);
            this.numStockMin.TabIndex = 11;
            // 
            // numStockMax
            // 
            this.numStockMax.Enabled = false;
            this.numStockMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numStockMax.Location = new System.Drawing.Point(164, 223);
            this.numStockMax.Name = "numStockMax";
            this.numStockMax.Size = new System.Drawing.Size(159, 27);
            this.numStockMax.TabIndex = 12;
            // 
            // Filtros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 318);
            this.Controls.Add(this.numStockMax);
            this.Controls.Add(this.numStockMin);
            this.Controls.Add(this.numPrecioMax);
            this.Controls.Add(this.numPrecioMin);
            this.Controls.Add(this.textMarca);
            this.Controls.Add(this.textNombre);
            this.Controls.Add(this.btnFiltros);
            this.Controls.Add(this.cbStockMax);
            this.Controls.Add(this.cbStockMin);
            this.Controls.Add(this.cbPrecioMax);
            this.Controls.Add(this.cbPrecioMin);
            this.Controls.Add(this.cbMarca);
            this.Controls.Add(this.cbNombre);
            this.Name = "Filtros";
            this.Text = "Filtros";
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStockMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStockMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbNombre;
        private System.Windows.Forms.CheckBox cbMarca;
        private System.Windows.Forms.CheckBox cbPrecioMin;
        private System.Windows.Forms.CheckBox cbPrecioMax;
        private System.Windows.Forms.CheckBox cbStockMin;
        private System.Windows.Forms.CheckBox cbStockMax;
        private System.Windows.Forms.Button btnFiltros;
        private System.Windows.Forms.TextBox textNombre;
        private System.Windows.Forms.TextBox textMarca;
        private System.Windows.Forms.NumericUpDown numPrecioMin;
        private System.Windows.Forms.NumericUpDown numPrecioMax;
        private System.Windows.Forms.NumericUpDown numStockMin;
        private System.Windows.Forms.NumericUpDown numStockMax;
    }
}