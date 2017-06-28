namespace InterchangeWFA
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.dGViewSenior = new System.Windows.Forms.DataGridView();
            this.idtpesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomfunDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numpisDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numfisDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tECCRADataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tEMBIODataGridViewImageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIPTEMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loademployedclockBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rondaGravataiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rondaGravatai = new InterchangeWFA.RondaGravatai();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.load_employe_dclockTableAdapter = new InterchangeWFA.RondaGravataiTableAdapters.load_employe_dclockTableAdapter();
            this.colaboradoresConsultaRemoteOutBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.colaboradoresConsultaRemoteOutBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnGravaBio = new System.Windows.Forms.Button();
            this.rBtnTemp1 = new System.Windows.Forms.RadioButton();
            this.lblQtdBio = new System.Windows.Forms.Label();
            this.rBtnTemp2 = new System.Windows.Forms.RadioButton();
            this.rBtnTemp3 = new System.Windows.Forms.RadioButton();
            this.gBoxGravaBio = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dGViewSenior)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loademployedclockBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rondaGravataiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rondaGravatai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colaboradoresConsultaRemoteOutBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colaboradoresConsultaRemoteOutBindingSource)).BeginInit();
            this.gBoxGravaBio.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnStart.Location = new System.Drawing.Point(95, 87);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(152, 37);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Inserir Colaborador";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStop.Location = new System.Drawing.Point(355, 87);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(152, 37);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(212, 38);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(163, 26);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Iniciar Conexão";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dGViewSenior
            // 
            this.dGViewSenior.AllowUserToDeleteRows = false;
            this.dGViewSenior.AllowUserToOrderColumns = true;
            this.dGViewSenior.AutoGenerateColumns = false;
            this.dGViewSenior.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGViewSenior.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idtpesDataGridViewTextBoxColumn,
            this.nomfunDataGridViewTextBoxColumn,
            this.numpisDataGridViewTextBoxColumn,
            this.numfisDataGridViewTextBoxColumn,
            this.tECCRADataGridViewTextBoxColumn,
            this.tEMBIODataGridViewImageColumn,
            this.tIPTEMDataGridViewTextBoxColumn});
            this.dGViewSenior.DataSource = this.loademployedclockBindingSource;
            this.dGViewSenior.Location = new System.Drawing.Point(64, 283);
            this.dGViewSenior.Name = "dGViewSenior";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGViewSenior.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dGViewSenior.Size = new System.Drawing.Size(443, 173);
            this.dGViewSenior.TabIndex = 5;
            this.dGViewSenior.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGViewSenior_CellContentClick);
            // 
            // idtpesDataGridViewTextBoxColumn
            // 
            this.idtpesDataGridViewTextBoxColumn.DataPropertyName = "idtpes";
            this.idtpesDataGridViewTextBoxColumn.HeaderText = "Matrícula";
            this.idtpesDataGridViewTextBoxColumn.Name = "idtpesDataGridViewTextBoxColumn";
            // 
            // nomfunDataGridViewTextBoxColumn
            // 
            this.nomfunDataGridViewTextBoxColumn.DataPropertyName = "nomfun";
            this.nomfunDataGridViewTextBoxColumn.HeaderText = "Nome do Funcionário";
            this.nomfunDataGridViewTextBoxColumn.Name = "nomfunDataGridViewTextBoxColumn";
            // 
            // numpisDataGridViewTextBoxColumn
            // 
            this.numpisDataGridViewTextBoxColumn.DataPropertyName = "numpis";
            this.numpisDataGridViewTextBoxColumn.HeaderText = "Número PIS";
            this.numpisDataGridViewTextBoxColumn.Name = "numpisDataGridViewTextBoxColumn";
            // 
            // numfisDataGridViewTextBoxColumn
            // 
            this.numfisDataGridViewTextBoxColumn.DataPropertyName = "numfis";
            this.numfisDataGridViewTextBoxColumn.HeaderText = "Cartão";
            this.numfisDataGridViewTextBoxColumn.Name = "numfisDataGridViewTextBoxColumn";
            this.numfisDataGridViewTextBoxColumn.ToolTipText = "Número Físico";
            // 
            // tECCRADataGridViewTextBoxColumn
            // 
            this.tECCRADataGridViewTextBoxColumn.DataPropertyName = "TECCRA";
            this.tECCRADataGridViewTextBoxColumn.HeaderText = "Tecnologia Cartão";
            this.tECCRADataGridViewTextBoxColumn.Name = "tECCRADataGridViewTextBoxColumn";
            // 
            // tEMBIODataGridViewImageColumn
            // 
            this.tEMBIODataGridViewImageColumn.DataPropertyName = "TEMBIO";
            this.tEMBIODataGridViewImageColumn.HeaderText = "Template Biométrico";
            this.tEMBIODataGridViewImageColumn.Name = "tEMBIODataGridViewImageColumn";
            this.tEMBIODataGridViewImageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.tEMBIODataGridViewImageColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tIPTEMDataGridViewTextBoxColumn
            // 
            this.tIPTEMDataGridViewTextBoxColumn.DataPropertyName = "TIPTEM";
            this.tIPTEMDataGridViewTextBoxColumn.HeaderText = "Quantidade Template";
            this.tIPTEMDataGridViewTextBoxColumn.Name = "tIPTEMDataGridViewTextBoxColumn";
            // 
            // loademployedclockBindingSource
            // 
            this.loademployedclockBindingSource.DataMember = "load_employe_dclock";
            this.loademployedclockBindingSource.DataSource = this.rondaGravataiBindingSource;
            // 
            // rondaGravataiBindingSource
            // 
            this.rondaGravataiBindingSource.DataSource = this.rondaGravatai;
            this.rondaGravataiBindingSource.Position = 0;
            // 
            // rondaGravatai
            // 
            this.rondaGravatai.DataSetName = "RondaGravatai";
            this.rondaGravatai.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(355, 138);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(152, 41);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // load_employe_dclockTableAdapter
            // 
            this.load_employe_dclockTableAdapter.ClearBeforeFill = true;
            // 
            // colaboradoresConsultaRemoteOutBindingSource1
            // 
            this.colaboradoresConsultaRemoteOutBindingSource1.DataSource = typeof(InterchangeWFA.DiginetWebServiceRef.colaboradoresConsultaRemoteOut);
            // 
            // colaboradoresConsultaRemoteOutBindingSource
            // 
            this.colaboradoresConsultaRemoteOutBindingSource.DataSource = typeof(InterchangeWFA.DiginetWebServiceRef.colaboradoresConsultaRemoteOut);
            // 
            // btnGravaBio
            // 
            this.btnGravaBio.Location = new System.Drawing.Point(31, 12);
            this.btnGravaBio.Name = "btnGravaBio";
            this.btnGravaBio.Size = new System.Drawing.Size(152, 37);
            this.btnGravaBio.TabIndex = 7;
            this.btnGravaBio.Text = "Grava Biometria";
            this.btnGravaBio.UseVisualStyleBackColor = true;
            this.btnGravaBio.Click += new System.EventHandler(this.btnGravaBio_Click_1);
            // 
            // rBtnTemp1
            // 
            this.rBtnTemp1.AutoSize = true;
            this.rBtnTemp1.Location = new System.Drawing.Point(31, 92);
            this.rBtnTemp1.Name = "rBtnTemp1";
            this.rBtnTemp1.Size = new System.Drawing.Size(31, 17);
            this.rBtnTemp1.TabIndex = 8;
            this.rBtnTemp1.TabStop = true;
            this.rBtnTemp1.Text = "0";
            this.rBtnTemp1.UseVisualStyleBackColor = true;
            // 
            // lblQtdBio
            // 
            this.lblQtdBio.AutoSize = true;
            this.lblQtdBio.Location = new System.Drawing.Point(28, 57);
            this.lblQtdBio.Name = "lblQtdBio";
            this.lblQtdBio.Size = new System.Drawing.Size(132, 13);
            this.lblQtdBio.TabIndex = 9;
            this.lblQtdBio.Text = "Quantidade de Templates:";
            // 
            // rBtnTemp2
            // 
            this.rBtnTemp2.AutoSize = true;
            this.rBtnTemp2.Location = new System.Drawing.Point(80, 92);
            this.rBtnTemp2.Name = "rBtnTemp2";
            this.rBtnTemp2.Size = new System.Drawing.Size(31, 17);
            this.rBtnTemp2.TabIndex = 10;
            this.rBtnTemp2.TabStop = true;
            this.rBtnTemp2.Text = "1";
            this.rBtnTemp2.UseVisualStyleBackColor = true;
            // 
            // rBtnTemp3
            // 
            this.rBtnTemp3.AutoSize = true;
            this.rBtnTemp3.Location = new System.Drawing.Point(129, 92);
            this.rBtnTemp3.Name = "rBtnTemp3";
            this.rBtnTemp3.Size = new System.Drawing.Size(31, 17);
            this.rBtnTemp3.TabIndex = 11;
            this.rBtnTemp3.TabStop = true;
            this.rBtnTemp3.Text = "2";
            this.rBtnTemp3.UseVisualStyleBackColor = true;
            // 
            // gBoxGravaBio
            // 
            this.gBoxGravaBio.Controls.Add(this.rBtnTemp3);
            this.gBoxGravaBio.Controls.Add(this.rBtnTemp2);
            this.gBoxGravaBio.Controls.Add(this.lblQtdBio);
            this.gBoxGravaBio.Controls.Add(this.rBtnTemp1);
            this.gBoxGravaBio.Controls.Add(this.btnGravaBio);
            this.gBoxGravaBio.Location = new System.Drawing.Point(64, 130);
            this.gBoxGravaBio.Name = "gBoxGravaBio";
            this.gBoxGravaBio.Size = new System.Drawing.Size(211, 126);
            this.gBoxGravaBio.TabIndex = 12;
            this.gBoxGravaBio.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 525);
            this.Controls.Add(this.gBoxGravaBio);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.dGViewSenior);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interchange";
            this.TransparencyKey = System.Drawing.Color.WhiteSmoke;
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGViewSenior)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loademployedclockBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rondaGravataiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rondaGravatai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colaboradoresConsultaRemoteOutBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colaboradoresConsultaRemoteOutBindingSource)).EndInit();
            this.gBoxGravaBio.ResumeLayout(false);
            this.gBoxGravaBio.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.DataGridView dGViewSenior;
        private System.Windows.Forms.BindingSource rondaGravataiBindingSource;
        private RondaGravatai rondaGravatai;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.BindingSource loademployedclockBindingSource;
        private RondaGravataiTableAdapters.load_employe_dclockTableAdapter load_employe_dclockTableAdapter;
        private System.Windows.Forms.BindingSource colaboradoresConsultaRemoteOutBindingSource1;
        private System.Windows.Forms.BindingSource colaboradoresConsultaRemoteOutBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idtpesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomfunDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numpisDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numfisDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tECCRADataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tEMBIODataGridViewImageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIPTEMDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnGravaBio;
        private System.Windows.Forms.RadioButton rBtnTemp1;
        private System.Windows.Forms.Label lblQtdBio;
        private System.Windows.Forms.RadioButton rBtnTemp2;
        private System.Windows.Forms.RadioButton rBtnTemp3;
        private System.Windows.Forms.GroupBox gBoxGravaBio;
    }
}

