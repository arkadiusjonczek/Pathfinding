namespace Pathfinding.Game
{
    partial class GameForm
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
            this.pictureBoxGrid = new System.Windows.Forms.PictureBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.radioButtonLaserTower = new System.Windows.Forms.RadioButton();
            this.radioButtonRocketTower = new System.Windows.Forms.RadioButton();
            this.labelTowers = new System.Windows.Forms.Label();
            this.labelLifes = new System.Windows.Forms.Label();
            this.labelLifesValue = new System.Windows.Forms.Label();
            this.labelKilled = new System.Windows.Forms.Label();
            this.labelKilledValue = new System.Windows.Forms.Label();
            this.labelCash = new System.Windows.Forms.Label();
            this.labelCashValue = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxGrid
            // 
            this.pictureBoxGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.buttonStart.Location = new System.Drawing.Point(543, 12);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(98, 23);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // radioButtonLaserTower
            // 
            this.radioButtonLaserTower.AutoSize = true;
            this.radioButtonLaserTower.Checked = true;
            this.radioButtonLaserTower.Location = new System.Drawing.Point(546, 141);
            this.radioButtonLaserTower.Name = "radioButtonLaserTower";
            this.radioButtonLaserTower.Size = new System.Drawing.Size(84, 17);
            this.radioButtonLaserTower.TabIndex = 2;
            this.radioButtonLaserTower.TabStop = true;
            this.radioButtonLaserTower.Text = "Laser Tower";
            this.radioButtonLaserTower.UseVisualStyleBackColor = true;
            // 
            // radioButtonRocketTower
            // 
            this.radioButtonRocketTower.AutoSize = true;
            this.radioButtonRocketTower.Location = new System.Drawing.Point(546, 161);
            this.radioButtonRocketTower.Name = "radioButtonRocketTower";
            this.radioButtonRocketTower.Size = new System.Drawing.Size(93, 17);
            this.radioButtonRocketTower.TabIndex = 3;
            this.radioButtonRocketTower.Text = "Rocket Tower";
            this.radioButtonRocketTower.UseVisualStyleBackColor = true;
            // 
            // labelTowers
            // 
            this.labelTowers.AutoSize = true;
            this.labelTowers.Location = new System.Drawing.Point(543, 122);
            this.labelTowers.Name = "labelTowers";
            this.labelTowers.Size = new System.Drawing.Size(45, 13);
            this.labelTowers.TabIndex = 4;
            this.labelTowers.Text = "Towers:";
            // 
            // labelLifes
            // 
            this.labelLifes.AutoSize = true;
            this.labelLifes.Location = new System.Drawing.Point(543, 51);
            this.labelLifes.Name = "labelLifes";
            this.labelLifes.Size = new System.Drawing.Size(35, 13);
            this.labelLifes.TabIndex = 5;
            this.labelLifes.Text = "Lifes: ";
            // 
            // labelLifesValue
            // 
            this.labelLifesValue.AutoSize = true;
            this.labelLifesValue.Location = new System.Drawing.Point(576, 51);
            this.labelLifesValue.Name = "labelLifesValue";
            this.labelLifesValue.Size = new System.Drawing.Size(13, 13);
            this.labelLifesValue.TabIndex = 6;
            this.labelLifesValue.Text = "0";
            // 
            // labelKilled
            // 
            this.labelKilled.AutoSize = true;
            this.labelKilled.Location = new System.Drawing.Point(543, 67);
            this.labelKilled.Name = "labelKilled";
            this.labelKilled.Size = new System.Drawing.Size(38, 13);
            this.labelKilled.TabIndex = 7;
            this.labelKilled.Text = "Killed: ";
            // 
            // labelKilledValue
            // 
            this.labelKilledValue.AutoSize = true;
            this.labelKilledValue.Location = new System.Drawing.Point(576, 67);
            this.labelKilledValue.Name = "labelKilledValue";
            this.labelKilledValue.Size = new System.Drawing.Size(13, 13);
            this.labelKilledValue.TabIndex = 8;
            this.labelKilledValue.Text = "0";
            // 
            // labelCash
            // 
            this.labelCash.AutoSize = true;
            this.labelCash.Location = new System.Drawing.Point(543, 83);
            this.labelCash.Name = "labelCash";
            this.labelCash.Size = new System.Drawing.Size(37, 13);
            this.labelCash.TabIndex = 9;
            this.labelCash.Text = "Cash: ";
            // 
            // labelCashValue
            // 
            this.labelCashValue.AutoSize = true;
            this.labelCashValue.Location = new System.Drawing.Point(576, 83);
            this.labelCashValue.Name = "labelCashValue";
            this.labelCashValue.Size = new System.Drawing.Size(13, 13);
            this.labelCashValue.TabIndex = 10;
            this.labelCashValue.Text = "0";
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(543, 409);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(98, 23);
            this.buttonExit.TabIndex = 11;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 446);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.labelCashValue);
            this.Controls.Add(this.labelCash);
            this.Controls.Add(this.labelKilledValue);
            this.Controls.Add(this.labelKilled);
            this.Controls.Add(this.labelLifesValue);
            this.Controls.Add(this.labelLifes);
            this.Controls.Add(this.labelTowers);
            this.Controls.Add(this.radioButtonRocketTower);
            this.Controls.Add(this.radioButtonLaserTower);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.pictureBoxGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.Text = "Pathfinding Game";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GameForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxGrid;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.RadioButton radioButtonLaserTower;
        private System.Windows.Forms.RadioButton radioButtonRocketTower;
        private System.Windows.Forms.Label labelTowers;
        private System.Windows.Forms.Label labelLifes;
        private System.Windows.Forms.Label labelLifesValue;
        private System.Windows.Forms.Label labelKilled;
        private System.Windows.Forms.Label labelKilledValue;
        private System.Windows.Forms.Label labelCash;
        private System.Windows.Forms.Label labelCashValue;
        private System.Windows.Forms.Button buttonExit;
    }
}

