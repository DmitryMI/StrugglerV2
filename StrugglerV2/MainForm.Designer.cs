namespace StrugglerV2
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ToggleKeyBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TargetKeyBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ListeningIndicator = new System.Windows.Forms.Panel();
            this.ActuatingIndicator = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.SetTargetKeyButton = new StrugglerV2.NotSelectableButton();
            this.SetToggleKeyButton = new StrugglerV2.NotSelectableButton();
            this.StartListeningButton = new StrugglerV2.NotSelectableButton();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToggleKeyBox
            // 
            this.ToggleKeyBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ToggleKeyBox.Location = new System.Drawing.Point(184, 6);
            this.ToggleKeyBox.Name = "ToggleKeyBox";
            this.ToggleKeyBox.ReadOnly = true;
            this.ToggleKeyBox.Size = new System.Drawing.Size(363, 35);
            this.ToggleKeyBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Switching key:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(45, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "Target key:";
            // 
            // TargetKeyBox
            // 
            this.TargetKeyBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TargetKeyBox.Location = new System.Drawing.Point(184, 47);
            this.TargetKeyBox.Name = "TargetKeyBox";
            this.TargetKeyBox.ReadOnly = true;
            this.TargetKeyBox.Size = new System.Drawing.Size(363, 35);
            this.TargetKeyBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(225, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 29);
            this.label3.TabIndex = 7;
            this.label3.Text = "Listening:";
            // 
            // ListeningIndicator
            // 
            this.ListeningIndicator.BackColor = System.Drawing.Color.Red;
            this.ListeningIndicator.Location = new System.Drawing.Point(347, 172);
            this.ListeningIndicator.Name = "ListeningIndicator";
            this.ListeningIndicator.Size = new System.Drawing.Size(50, 50);
            this.ListeningIndicator.TabIndex = 8;
            // 
            // ActuatingIndicator
            // 
            this.ActuatingIndicator.BackColor = System.Drawing.Color.Red;
            this.ActuatingIndicator.Location = new System.Drawing.Point(347, 228);
            this.ActuatingIndicator.Name = "ActuatingIndicator";
            this.ActuatingIndicator.Size = new System.Drawing.Size(50, 50);
            this.ActuatingIndicator.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(225, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 29);
            this.label4.TabIndex = 9;
            this.label4.Text = "Actuating:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 452);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(648, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 15);
            // 
            // SetTargetKeyButton
            // 
            this.SetTargetKeyButton.Location = new System.Drawing.Point(553, 47);
            this.SetTargetKeyButton.Name = "SetTargetKeyButton";
            this.SetTargetKeyButton.Size = new System.Drawing.Size(83, 35);
            this.SetTargetKeyButton.TabIndex = 6;
            this.SetTargetKeyButton.Text = "Set";
            this.SetTargetKeyButton.UseVisualStyleBackColor = true;
            this.SetTargetKeyButton.Click += new System.EventHandler(this.SetKeyButton_Click);
            // 
            // SetToggleKeyButton
            // 
            this.SetToggleKeyButton.Location = new System.Drawing.Point(553, 6);
            this.SetToggleKeyButton.Name = "SetToggleKeyButton";
            this.SetToggleKeyButton.Size = new System.Drawing.Size(83, 35);
            this.SetToggleKeyButton.TabIndex = 5;
            this.SetToggleKeyButton.Text = "Set";
            this.SetToggleKeyButton.UseVisualStyleBackColor = true;
            this.SetToggleKeyButton.Click += new System.EventHandler(this.SetKeyButton_Click);
            // 
            // StartListeningButton
            // 
            this.StartListeningButton.Location = new System.Drawing.Point(7, 397);
            this.StartListeningButton.Name = "StartListeningButton";
            this.StartListeningButton.Size = new System.Drawing.Size(629, 41);
            this.StartListeningButton.TabIndex = 0;
            this.StartListeningButton.Text = "Toggle listening";
            this.StartListeningButton.UseVisualStyleBackColor = true;
            this.StartListeningButton.Click += new System.EventHandler(this.StartListeningButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 474);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ActuatingIndicator);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ListeningIndicator);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SetTargetKeyButton);
            this.Controls.Add(this.SetToggleKeyButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TargetKeyBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ToggleKeyBox);
            this.Controls.Add(this.StartListeningButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.Text = "Struggler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StrugglerV2.NotSelectableButton StartListeningButton;
        private System.Windows.Forms.TextBox ToggleKeyBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TargetKeyBox;
        private NotSelectableButton SetToggleKeyButton;
        private NotSelectableButton SetTargetKeyButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel ListeningIndicator;
        private System.Windows.Forms.Panel ActuatingIndicator;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
    }
}

