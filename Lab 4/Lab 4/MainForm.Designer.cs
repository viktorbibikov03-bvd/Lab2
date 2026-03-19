namespace Lab4
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            ButtonForAdd = new Button();
            DataGridViewWithEmployees = new DataGridView();
            ButtonForDelete = new Button();
            ButtonForSearch = new Button();
            menuStrip1 = new MenuStrip();
            FileToolStripMenuItem = new ToolStripMenuItem();
            OpenToolStripMenuItem = new ToolStripMenuItem();
            SaveToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)DataGridViewWithEmployees).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // ButtonForAdd
            // 
            ButtonForAdd.Anchor = AnchorStyles.None;
            ButtonForAdd.BackColor = Color.White;
            ButtonForAdd.Cursor = Cursors.Hand;
            ButtonForAdd.ForeColor = SystemColors.ActiveCaptionText;
            ButtonForAdd.Location = new Point(30, 570);
            ButtonForAdd.Name = "ButtonForAdd";
            ButtonForAdd.Size = new Size(250, 50);
            ButtonForAdd.TabIndex = 0;
            ButtonForAdd.Text = "Добавить сотрудника";
            ButtonForAdd.UseVisualStyleBackColor = false;
            ButtonForAdd.Click += AddButton_Click;
            // 
            // DataGridViewWithEmployees
            // 
            DataGridViewWithEmployees.AllowUserToAddRows = false;
            DataGridViewWithEmployees.AllowUserToDeleteRows = false;
            DataGridViewWithEmployees.AllowUserToResizeRows = false;
            DataGridViewWithEmployees.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DataGridViewWithEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewWithEmployees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewWithEmployees.Location = new Point(3, 38);
            DataGridViewWithEmployees.Name = "DataGridViewWithEmployees";
            DataGridViewWithEmployees.ReadOnly = true;
            DataGridViewWithEmployees.RowHeadersWidth = 62;
            DataGridViewWithEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewWithEmployees.Size = new Size(1073, 508);
            DataGridViewWithEmployees.TabIndex = 0;
            // 
            // ButtonForDelete
            // 
            ButtonForDelete.BackColor = Color.White;
            ButtonForDelete.Location = new Point(330, 570);
            ButtonForDelete.Name = "ButtonForDelete";
            ButtonForDelete.Size = new Size(250, 50);
            ButtonForDelete.TabIndex = 2;
            ButtonForDelete.Text = "Удалить сотрудника";
            ButtonForDelete.UseVisualStyleBackColor = false;
            ButtonForDelete.Click += ButtonForDelete_Click;
            // 
            // ButtonForSearch
            // 
            ButtonForSearch.BackColor = Color.White;
            ButtonForSearch.Location = new Point(630, 570);
            ButtonForSearch.Name = "ButtonForSearch";
            ButtonForSearch.Size = new Size(250, 50);
            ButtonForSearch.TabIndex = 3;
            ButtonForSearch.Text = "Поиск сотрудника";
            ButtonForSearch.UseVisualStyleBackColor = false;
            ButtonForSearch.Click += ButtonForSearch_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { FileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1078, 33);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { OpenToolStripMenuItem, SaveToolStripMenuItem });
            FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            FileToolStripMenuItem.Size = new Size(69, 29);
            FileToolStripMenuItem.Text = "Файл";
            // 
            // OpenToolStripMenuItem
            // 
            OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            OpenToolStripMenuItem.Size = new Size(244, 34);
            OpenToolStripMenuItem.Text = "Открыть";
            OpenToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            // 
            // SaveToolStripMenuItem
            // 
            SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            SaveToolStripMenuItem.Size = new Size(244, 34);
            SaveToolStripMenuItem.Text = "Сохранить как...";
            SaveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1078, 644);
            Controls.Add(ButtonForSearch);
            Controls.Add(ButtonForDelete);
            Controls.Add(DataGridViewWithEmployees);
            Controls.Add(ButtonForAdd);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(1100, 700);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Список сотрудников";
            ((System.ComponentModel.ISupportInitialize)DataGridViewWithEmployees).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ButtonForAdd;
        private DataGridView DataGridViewWithEmployees;
        private Button ButtonForDelete;
        private Button ButtonForSearch;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem FileToolStripMenuItem;
        private ToolStripMenuItem OpenToolStripMenuItem;
        private ToolStripMenuItem SaveToolStripMenuItem;
    }
}
