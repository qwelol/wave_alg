namespace wave_alg
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.generateBtn = new System.Windows.Forms.Button();
            this.settings_panel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.heightTB = new System.Windows.Forms.TextBox();
            this.widthTB = new System.Windows.Forms.TextBox();
            this.againBtn = new System.Windows.Forms.Button();
            this.calculateBtn = new System.Windows.Forms.Button();
            this.workplacePanel = new System.Windows.Forms.Panel();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settings_panel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // generateBtn
            // 
            this.generateBtn.Location = new System.Drawing.Point(396, 7);
            this.generateBtn.Name = "generateBtn";
            this.generateBtn.Size = new System.Drawing.Size(115, 39);
            this.generateBtn.TabIndex = 0;
            this.generateBtn.Text = "Сгенерировать";
            this.generateBtn.UseVisualStyleBackColor = true;
            this.generateBtn.Click += new System.EventHandler(this.generateBtn_Click);
            // 
            // settings_panel
            // 
            this.settings_panel.Controls.Add(this.label1);
            this.settings_panel.Controls.Add(this.heightTB);
            this.settings_panel.Controls.Add(this.widthTB);
            this.settings_panel.Controls.Add(this.againBtn);
            this.settings_panel.Controls.Add(this.calculateBtn);
            this.settings_panel.Controls.Add(this.generateBtn);
            this.settings_panel.Location = new System.Drawing.Point(12, 27);
            this.settings_panel.Name = "settings_panel";
            this.settings_panel.Size = new System.Drawing.Size(700, 53);
            this.settings_panel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "X";
            // 
            // heightTB
            // 
            this.heightTB.Location = new System.Drawing.Point(185, 17);
            this.heightTB.MaxLength = 2;
            this.heightTB.Name = "heightTB";
            this.heightTB.Size = new System.Drawing.Size(100, 20);
            this.heightTB.TabIndex = 2;
            this.heightTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.heightTB_KeyPress);
            // 
            // widthTB
            // 
            this.widthTB.Location = new System.Drawing.Point(56, 17);
            this.widthTB.MaxLength = 2;
            this.widthTB.Name = "widthTB";
            this.widthTB.Size = new System.Drawing.Size(100, 20);
            this.widthTB.TabIndex = 1;
            this.widthTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.widthTB_KeyPress);
            // 
            // againBtn
            // 
            this.againBtn.Location = new System.Drawing.Point(396, 7);
            this.againBtn.Name = "againBtn";
            this.againBtn.Size = new System.Drawing.Size(115, 39);
            this.againBtn.TabIndex = 5;
            this.againBtn.Text = "Заново";
            this.againBtn.UseVisualStyleBackColor = true;
            this.againBtn.Visible = false;
            this.againBtn.Click += new System.EventHandler(this.againBtn_Click);
            // 
            // calculateBtn
            // 
            this.calculateBtn.Location = new System.Drawing.Point(396, 7);
            this.calculateBtn.Name = "calculateBtn";
            this.calculateBtn.Size = new System.Drawing.Size(115, 39);
            this.calculateBtn.TabIndex = 4;
            this.calculateBtn.Text = "Расчитать";
            this.calculateBtn.UseVisualStyleBackColor = true;
            this.calculateBtn.Visible = false;
            this.calculateBtn.Click += new System.EventHandler(this.calculateBtn_Click);
            // 
            // workplacePanel
            // 
            this.workplacePanel.AutoScroll = true;
            this.workplacePanel.Location = new System.Drawing.Point(12, 86);
            this.workplacePanel.Name = "workplacePanel";
            this.workplacePanel.Size = new System.Drawing.Size(700, 520);
            this.workplacePanel.TabIndex = 2;
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 619);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(724, 22);
            this.statusBar.TabIndex = 3;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(535, 621);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(160, 20);
            this.progressBar.TabIndex = 4;
            this.progressBar.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(724, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.saveToolStripMenuItem.Text = "Сохранить в файл";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.quitToolStripMenuItem.Text = "Выход";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 641);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.workplacePanel);
            this.Controls.Add(this.settings_panel);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wave alg";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.settings_panel.ResumeLayout(false);
            this.settings_panel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button generateBtn;
        private System.Windows.Forms.Panel settings_panel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox heightTB;
        private System.Windows.Forms.TextBox widthTB;
        private System.Windows.Forms.Panel workplacePanel;
        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button calculateBtn;
        private System.Windows.Forms.Button againBtn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

