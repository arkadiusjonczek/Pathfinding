namespace Pathfinding.Demo.Gui
{
    partial class DemoForm
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
            this.pictureBoxGrid = new System.Windows.Forms.PictureBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.comboBoxAlgorithm = new System.Windows.Forms.ComboBox();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.checkBoxAllowDiagonal = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowCrossCorners = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxGrid
            // 
            this.pictureBoxGrid.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxGrid.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxGrid.Name = "pictureBoxGrid";
            this.pictureBoxGrid.Size = new System.Drawing.Size(525, 420);
            this.pictureBoxGrid.TabIndex = 0;
            this.pictureBoxGrid.TabStop = false;
            this.pictureBoxGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxGrid_Paint);
            this.pictureBoxGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxGrid_MouseClick);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(543, 39);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(623, 39);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 2;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // comboBoxAlgorithm
            // 
            this.comboBoxAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAlgorithm.FormattingEnabled = true;
            this.comboBoxAlgorithm.Items.AddRange(new object[] {
            "Dijkstra",
            "A* Manhattan",
            "A* Euclidean",
            "A* Chebyshev"});
            this.comboBoxAlgorithm.Location = new System.Drawing.Point(543, 12);
            this.comboBoxAlgorithm.Name = "comboBoxAlgorithm";
            this.comboBoxAlgorithm.Size = new System.Drawing.Size(156, 21);
            this.comboBoxAlgorithm.TabIndex = 4;
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Location = new System.Drawing.Point(543, 143);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.ReadOnly = true;
            this.textBoxInfo.Size = new System.Drawing.Size(155, 287);
            this.textBoxInfo.TabIndex = 5;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(543, 68);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 6;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // checkBoxAllowDiagonal
            // 
            this.checkBoxAllowDiagonal.AutoSize = true;
            this.checkBoxAllowDiagonal.Checked = true;
            this.checkBoxAllowDiagonal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAllowDiagonal.Location = new System.Drawing.Point(543, 97);
            this.checkBoxAllowDiagonal.Name = "checkBoxAllowDiagonal";
            this.checkBoxAllowDiagonal.Size = new System.Drawing.Size(96, 17);
            this.checkBoxAllowDiagonal.TabIndex = 7;
            this.checkBoxAllowDiagonal.Text = "Allow Diagonal";
            this.checkBoxAllowDiagonal.UseVisualStyleBackColor = true;
            // 
            // checkBoxAllowCrossCorners
            // 
            this.checkBoxAllowCrossCorners.AutoSize = true;
            this.checkBoxAllowCrossCorners.Checked = true;
            this.checkBoxAllowCrossCorners.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAllowCrossCorners.Location = new System.Drawing.Point(543, 120);
            this.checkBoxAllowCrossCorners.Name = "checkBoxAllowCrossCorners";
            this.checkBoxAllowCrossCorners.Size = new System.Drawing.Size(119, 17);
            this.checkBoxAllowCrossCorners.TabIndex = 8;
            this.checkBoxAllowCrossCorners.Text = "Allow Cross Corners";
            this.checkBoxAllowCrossCorners.UseVisualStyleBackColor = true;
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 442);
            this.Controls.Add(this.checkBoxAllowCrossCorners);
            this.Controls.Add(this.checkBoxAllowDiagonal);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.textBoxInfo);
            this.Controls.Add(this.comboBoxAlgorithm);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.pictureBoxGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "DemoForm";
            this.Text = "DemoForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxGrid;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.ComboBox comboBoxAlgorithm;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.CheckBox checkBoxAllowDiagonal;
        private System.Windows.Forms.CheckBox checkBoxAllowCrossCorners;
    }
}

