namespace Lab4
{
    partial class SearchForm
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
            LabelForSearchName = new Label();
            LabelForSearchSurname = new Label();
            LabelForSearchProfession = new Label();
            TextBoxForSearchName = new TextBox();
            TextBoxForSearchSurname = new TextBox();
            TextBoxForSearchProfession = new TextBox();
            ButtonForFind = new Button();
            ButtonForReset = new Button();
            ButtonForCancel = new Button();
            SuspendLayout();
            // 
            // LabelForSearchName
            // 
            LabelForSearchName.AutoSize = true;
            LabelForSearchName.Location = new Point(36, 45);
            LabelForSearchName.Name = "LabelForSearchName";
            LabelForSearchName.Size = new Size(51, 25);
            LabelForSearchName.TabIndex = 0;
            LabelForSearchName.Text = "Имя:";
            // 
            // LabelForSearchSurname
            // 
            LabelForSearchSurname.AutoSize = true;
            LabelForSearchSurname.Location = new Point(36, 97);
            LabelForSearchSurname.Name = "LabelForSearchSurname";
            LabelForSearchSurname.Size = new Size(89, 25);
            LabelForSearchSurname.TabIndex = 1;
            LabelForSearchSurname.Text = "Фамилия:";
            // 
            // LabelForSearchProfession
            // 
            LabelForSearchProfession.AutoSize = true;
            LabelForSearchProfession.Location = new Point(36, 151);
            LabelForSearchProfession.Name = "LabelForSearchProfession";
            LabelForSearchProfession.Size = new Size(107, 25);
            LabelForSearchProfession.TabIndex = 2;
            LabelForSearchProfession.Text = "Профессия:";
            // 
            // TextBoxForSearchName
            // 
            TextBoxForSearchName.Location = new Point(210, 45);
            TextBoxForSearchName.Name = "TextBoxForSearchName";
            TextBoxForSearchName.Size = new Size(150, 31);
            TextBoxForSearchName.TabIndex = 4;
            // 
            // TextBoxForSearchSurname
            // 
            TextBoxForSearchSurname.Location = new Point(210, 97);
            TextBoxForSearchSurname.Name = "TextBoxForSearchSurname";
            TextBoxForSearchSurname.Size = new Size(150, 31);
            TextBoxForSearchSurname.TabIndex = 5;
            // 
            // TextBoxForSearchProfession
            // 
            TextBoxForSearchProfession.Location = new Point(210, 151);
            TextBoxForSearchProfession.Name = "TextBoxForSearchProfession";
            TextBoxForSearchProfession.Size = new Size(150, 31);
            TextBoxForSearchProfession.TabIndex = 6;
            // 
            // ButtonForFind
            // 
            ButtonForFind.Location = new Point(12, 204);
            ButtonForFind.Name = "ButtonForFind";
            ButtonForFind.Size = new Size(112, 34);
            ButtonForFind.TabIndex = 8;
            ButtonForFind.Text = "Найти";
            ButtonForFind.UseVisualStyleBackColor = true;
            ButtonForFind.Click += ButtonForFind_Click;
            // 
            // ButtonForReset
            // 
            ButtonForReset.Location = new Point(130, 204);
            ButtonForReset.Name = "ButtonForReset";
            ButtonForReset.Size = new Size(112, 34);
            ButtonForReset.TabIndex = 9;
            ButtonForReset.Text = "Сброс";
            ButtonForReset.UseVisualStyleBackColor = true;
            ButtonForReset.Click += ButtonForReset_Click;
            // 
            // ButtonForCancel
            // 
            ButtonForCancel.Location = new Point(248, 204);
            ButtonForCancel.Name = "ButtonForCancel";
            ButtonForCancel.Size = new Size(112, 34);
            ButtonForCancel.TabIndex = 10;
            ButtonForCancel.Text = "Отмена";
            ButtonForCancel.UseVisualStyleBackColor = true;
            ButtonForCancel.Click += ButtonForCancel_Click;
            // 
            // SearchForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(378, 254);
            Controls.Add(ButtonForCancel);
            Controls.Add(ButtonForReset);
            Controls.Add(ButtonForFind);
            Controls.Add(TextBoxForSearchProfession);
            Controls.Add(TextBoxForSearchSurname);
            Controls.Add(TextBoxForSearchName);
            Controls.Add(LabelForSearchProfession);
            Controls.Add(LabelForSearchSurname);
            Controls.Add(LabelForSearchName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimumSize = new Size(400, 310);
            Name = "SearchForm";
            Text = "Поиск сотрудника";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LabelForSearchName;
        private Label LabelForSearchSurname;
        private Label LabelForSearchProfession;
        private TextBox TextBoxForSearchName;
        private TextBox TextBoxForSearchSurname;
        private TextBox TextBoxForSearchProfession;
        private Button ButtonForFind;
        private Button ButtonForReset;
        private Button ButtonForCancel;
    }
}