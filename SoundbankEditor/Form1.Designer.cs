namespace MaddenMixer
{
    partial class MaddenMixer
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
            this.components = new System.ComponentModel.Container();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.offsetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReplaceSong = new System.Windows.Forms.DataGridViewButtonColumn();
            this.PlaySong = new System.Windows.Forms.DataGridViewButtonColumn();
            this.soundbankEntryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.openSoundFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenSbr = new System.Windows.Forms.Button();
            this.lbl_FileName = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.logOutput = new System.Windows.Forms.RichTextBox();
            this.btnOpenSbs = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.openMp3Dialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.soundbankEntryBindingSource)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbl_Title.Location = new System.Drawing.Point(12, 9);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(170, 32);
            this.lbl_Title.TabIndex = 0;
            this.lbl_Title.Text = "Madden Mixer";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(188, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "v0.3";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.offsetDataGridViewTextBoxColumn,
            this.ReplaceSong,
            this.PlaySong});
            this.dataGridView1.DataSource = this.soundbankEntryBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(776, 261);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 64;
            // 
            // offsetDataGridViewTextBoxColumn
            // 
            this.offsetDataGridViewTextBoxColumn.DataPropertyName = "Offset";
            this.offsetDataGridViewTextBoxColumn.HeaderText = "Offset";
            this.offsetDataGridViewTextBoxColumn.Name = "offsetDataGridViewTextBoxColumn";
            this.offsetDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ReplaceSong
            // 
            this.ReplaceSong.HeaderText = "Replace Song";
            this.ReplaceSong.Name = "ReplaceSong";
            this.ReplaceSong.ReadOnly = true;
            this.ReplaceSong.Text = "Replace...";
            this.ReplaceSong.UseColumnTextForButtonValue = true;
            this.ReplaceSong.Visible = false;
            // 
            // PlaySong
            // 
            this.PlaySong.HeaderText = "Play Song";
            this.PlaySong.Name = "PlaySong";
            this.PlaySong.ReadOnly = true;
            this.PlaySong.Text = "Play";
            this.PlaySong.UseColumnTextForButtonValue = true;
            this.PlaySong.Visible = false;
            // 
            // soundbankEntryBindingSource
            // 
            this.soundbankEntryBindingSource.DataSource = typeof(EASoundbankTools.Model.SoundbankEntry);
            // 
            // openSoundFileDialog
            // 
            this.openSoundFileDialog.FileName = "openSoundFileDialog";
            this.openSoundFileDialog.Title = "Open Sound File...";
            // 
            // btnOpenSbr
            // 
            this.btnOpenSbr.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnOpenSbr.FlatAppearance.BorderSize = 0;
            this.btnOpenSbr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenSbr.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOpenSbr.Location = new System.Drawing.Point(3, 3);
            this.btnOpenSbr.Name = "btnOpenSbr";
            this.btnOpenSbr.Size = new System.Drawing.Size(121, 23);
            this.btnOpenSbr.TabIndex = 3;
            this.btnOpenSbr.Text = "Open SBR File...";
            this.btnOpenSbr.UseVisualStyleBackColor = false;
            this.btnOpenSbr.Click += new System.EventHandler(this.btnOpenSbr_Click);
            // 
            // lbl_FileName
            // 
            this.lbl_FileName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_FileName.AutoEllipsis = true;
            this.lbl_FileName.AutoSize = true;
            this.lbl_FileName.Location = new System.Drawing.Point(257, 7);
            this.lbl_FileName.Name = "lbl_FileName";
            this.lbl_FileName.Size = new System.Drawing.Size(0, 15);
            this.lbl_FileName.TabIndex = 4;
            this.lbl_FileName.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 73);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.logOutput);
            this.splitContainer1.Size = new System.Drawing.Size(776, 352);
            this.splitContainer1.SplitterDistance = 261;
            this.splitContainer1.TabIndex = 8;
            // 
            // logOutput
            // 
            this.logOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logOutput.Location = new System.Drawing.Point(0, 0);
            this.logOutput.Name = "logOutput";
            this.logOutput.ReadOnly = true;
            this.logOutput.Size = new System.Drawing.Size(776, 87);
            this.logOutput.TabIndex = 0;
            this.logOutput.Text = "";
            // 
            // btnOpenSbs
            // 
            this.btnOpenSbs.BackColor = System.Drawing.SystemColors.Control;
            this.btnOpenSbs.FlatAppearance.BorderSize = 0;
            this.btnOpenSbs.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpenSbs.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnOpenSbs.Location = new System.Drawing.Point(130, 3);
            this.btnOpenSbs.Name = "btnOpenSbs";
            this.btnOpenSbs.Size = new System.Drawing.Size(121, 23);
            this.btnOpenSbs.TabIndex = 9;
            this.btnOpenSbs.Text = "Open SBS File...";
            this.btnOpenSbs.UseVisualStyleBackColor = false;
            this.btnOpenSbs.Visible = false;
            this.btnOpenSbs.Click += new System.EventHandler(this.btnOpenSbs_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnOpenSbr);
            this.flowLayoutPanel1.Controls.Add(this.btnOpenSbs);
            this.flowLayoutPanel1.Controls.Add(this.lbl_FileName);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(9, 41);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(776, 26);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // openMp3Dialog
            // 
            this.openMp3Dialog.Filter = "mp3 files|*.mp3|All files|*.*";
            this.openMp3Dialog.Title = "Open MP3 File";
            // 
            // MaddenMixer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "MaddenMixer";
            this.Text = "Madden Mixer";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.soundbankEntryBindingSource)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lbl_Title;
        private Label label1;
        private DataGridView dataGridView1;
        private BindingSource soundbankEntryBindingSource;
        private OpenFileDialog openSoundFileDialog;
        private Button btnOpenSbr;
        private Label lbl_FileName;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private SplitContainer splitContainer1;
        private RichTextBox logOutput;
        private Button btnOpenSbs;
        private FlowLayoutPanel flowLayoutPanel1;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn offsetDataGridViewTextBoxColumn;
        private DataGridViewButtonColumn ReplaceSong;
        private DataGridViewButtonColumn PlaySong;
        private OpenFileDialog openMp3Dialog;
    }
}