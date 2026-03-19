namespace Lab4
{
    partial class AddEmployeeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEmployeeForm));
            TextBoxForName = new TextBox();
            LabelForName = new Label();
            TextBoxForSurname = new TextBox();
            LabelForSurname = new Label();
            ComboBoxForProfession = new ComboBox();
            LabelForProfession = new Label();
            ComboBoxForGender = new ComboBox();
            LabelForGender = new Label();
            LabelForAge = new Label();
            TextBoxForAge = new TextBox();
            GroupBoxType = new GroupBox();
            RadioButtonSalary = new RadioButton();
            RadioButtonWage = new RadioButton();
            GroupBoxParameters = new GroupBox();
            TextBoxForParameter2 = new TextBox();
            LabelForParameter2 = new Label();
            TextBoxForParameter1 = new TextBox();
            LabelForParameter1 = new Label();
            ButtonOk = new Button();
            ButtonCancel = new Button();
            ButtonForGeneration = new Button();
            GroupBoxType.SuspendLayout();
            GroupBoxParameters.SuspendLayout();
            SuspendLayout();
            // 
            // TextBoxForName
            // 
            TextBoxForName.Location = new Point(280, 45);
            TextBoxForName.Name = "TextBoxForName";
            TextBoxForName.Size = new Size(378, 31);
            TextBoxForName.TabIndex = 0;
            // 
            // LabelForName
            // 
            LabelForName.AutoSize = true;
            LabelForName.Location = new Point(45, 48);
            LabelForName.Name = "LabelForName";
            LabelForName.Size = new Size(146, 25);
            LabelForName.TabIndex = 1;
            LabelForName.Text = "Имя сотрудника";
            // 
            // TextBoxForSurname
            // 
            TextBoxForSurname.Location = new Point(280, 95);
            TextBoxForSurname.Name = "TextBoxForSurname";
            TextBoxForSurname.Size = new Size(378, 31);
            TextBoxForSurname.TabIndex = 1;
            // 
            // LabelForSurname
            // 
            LabelForSurname.AutoSize = true;
            LabelForSurname.Location = new Point(45, 98);
            LabelForSurname.Name = "LabelForSurname";
            LabelForSurname.Size = new Size(184, 25);
            LabelForSurname.TabIndex = 3;
            LabelForSurname.Text = "Фамилия сотрудника";
            // 
            // ComboBoxForProfession
            // 
            ComboBoxForProfession.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForProfession.FormattingEnabled = true;
            ComboBoxForProfession.Location = new Point(280, 145);
            ComboBoxForProfession.MaximumSize = new Size(500, 0);
            ComboBoxForProfession.MinimumSize = new Size(100, 0);
            ComboBoxForProfession.Name = "ComboBoxForProfession";
            ComboBoxForProfession.Size = new Size(378, 33);
            ComboBoxForProfession.TabIndex = 2;
            // 
            // LabelForProfession
            // 
            LabelForProfession.AutoSize = true;
            LabelForProfession.Location = new Point(44, 148);
            LabelForProfession.Name = "LabelForProfession";
            LabelForProfession.Size = new Size(202, 25);
            LabelForProfession.TabIndex = 5;
            LabelForProfession.Text = "Профессия сотрудника";
            // 
            // ComboBoxForGender
            // 
            ComboBoxForGender.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxForGender.FormattingEnabled = true;
            ComboBoxForGender.Location = new Point(280, 195);
            ComboBoxForGender.Name = "ComboBoxForGender";
            ComboBoxForGender.Size = new Size(378, 33);
            ComboBoxForGender.TabIndex = 3;
            // 
            // LabelForGender
            // 
            LabelForGender.AutoSize = true;
            LabelForGender.Location = new Point(45, 198);
            LabelForGender.Name = "LabelForGender";
            LabelForGender.Size = new Size(144, 25);
            LabelForGender.TabIndex = 7;
            LabelForGender.Text = "Пол сотрудника";
            // 
            // LabelForAge
            // 
            LabelForAge.AutoSize = true;
            LabelForAge.Location = new Point(45, 248);
            LabelForAge.Name = "LabelForAge";
            LabelForAge.Size = new Size(175, 25);
            LabelForAge.TabIndex = 8;
            LabelForAge.Text = "Возраст сотрудника";
            // 
            // TextBoxForAge
            // 
            TextBoxForAge.Location = new Point(280, 245);
            TextBoxForAge.Name = "TextBoxForAge";
            TextBoxForAge.Size = new Size(378, 31);
            TextBoxForAge.TabIndex = 4;
            // 
            // GroupBoxType
            // 
            GroupBoxType.Controls.Add(RadioButtonSalary);
            GroupBoxType.Controls.Add(RadioButtonWage);
            GroupBoxType.Location = new Point(45, 312);
            GroupBoxType.Name = "GroupBoxType";
            GroupBoxType.Size = new Size(618, 72);
            GroupBoxType.TabIndex = 10;
            GroupBoxType.TabStop = false;
            GroupBoxType.Text = "Тип оплаты";
            // 
            // RadioButtonSalary
            // 
            RadioButtonSalary.AutoSize = true;
            RadioButtonSalary.Location = new Point(305, 30);
            RadioButtonSalary.Name = "RadioButtonSalary";
            RadioButtonSalary.Size = new Size(255, 29);
            RadioButtonSalary.TabIndex = 6;
            RadioButtonSalary.Text = "Оплата по окладу и ставке";
            RadioButtonSalary.UseVisualStyleBackColor = true;
            // 
            // RadioButtonWage
            // 
            RadioButtonWage.Location = new Point(59, 22);
            RadioButtonWage.Name = "RadioButtonWage";
            RadioButtonWage.Size = new Size(212, 44);
            RadioButtonWage.TabIndex = 5;
            RadioButtonWage.TabStop = true;
            RadioButtonWage.Text = "Почасовая оплата";
            RadioButtonWage.UseVisualStyleBackColor = true;
            // 
            // GroupBoxParameters
            // 
            GroupBoxParameters.Controls.Add(TextBoxForParameter2);
            GroupBoxParameters.Controls.Add(LabelForParameter2);
            GroupBoxParameters.Controls.Add(TextBoxForParameter1);
            GroupBoxParameters.Controls.Add(LabelForParameter1);
            GroupBoxParameters.Location = new Point(46, 390);
            GroupBoxParameters.Name = "GroupBoxParameters";
            GroupBoxParameters.Size = new Size(618, 145);
            GroupBoxParameters.TabIndex = 11;
            GroupBoxParameters.TabStop = false;
            GroupBoxParameters.Text = "Параметры";
            // 
            // TextBoxForParameter2
            // 
            TextBoxForParameter2.AcceptsTab = true;
            TextBoxForParameter2.Location = new Point(234, 84);
            TextBoxForParameter2.Name = "TextBoxForParameter2";
            TextBoxForParameter2.Size = new Size(150, 31);
            TextBoxForParameter2.TabIndex = 8;
            // 
            // LabelForParameter2
            // 
            LabelForParameter2.AutoSize = true;
            LabelForParameter2.Location = new Point(15, 87);
            LabelForParameter2.Name = "LabelForParameter2";
            LabelForParameter2.Size = new Size(113, 25);
            LabelForParameter2.TabIndex = 2;
            LabelForParameter2.Text = "Параметр 2:";
            // 
            // TextBoxForParameter1
            // 
            TextBoxForParameter1.Location = new Point(234, 38);
            TextBoxForParameter1.Name = "TextBoxForParameter1";
            TextBoxForParameter1.Size = new Size(150, 31);
            TextBoxForParameter1.TabIndex = 7;
            // 
            // LabelForParameter1
            // 
            LabelForParameter1.AutoSize = true;
            LabelForParameter1.Location = new Point(15, 41);
            LabelForParameter1.Name = "LabelForParameter1";
            LabelForParameter1.Size = new Size(113, 25);
            LabelForParameter1.TabIndex = 0;
            LabelForParameter1.Text = "Параметр 1:";
            // 
            // ButtonOk
            // 
            ButtonOk.Location = new Point(44, 569);
            ButtonOk.Name = "ButtonOk";
            ButtonOk.Size = new Size(130, 50);
            ButtonOk.TabIndex = 9;
            ButtonOk.Text = "ОК";
            ButtonOk.UseVisualStyleBackColor = true;
            ButtonOk.Click += ButtonOk_Click;
            // 
            // ButtonCancel
            // 
            ButtonCancel.Location = new Point(197, 569);
            ButtonCancel.Name = "ButtonCancel";
            ButtonCancel.Size = new Size(130, 50);
            ButtonCancel.TabIndex = 10;
            ButtonCancel.Text = "Отмена";
            ButtonCancel.UseVisualStyleBackColor = true;
            ButtonCancel.Click += ButtonCancel_Click;
            // 
            // ButtonForGeneration
            // 
            ButtonForGeneration.Location = new Point(350, 569);
            ButtonForGeneration.Name = "ButtonForGeneration";
            ButtonForGeneration.Size = new Size(320, 50);
            ButtonForGeneration.TabIndex = 12;
            ButtonForGeneration.Text = "Создать случайного сотрудника";
            ButtonForGeneration.UseVisualStyleBackColor = true;
            ButtonForGeneration.Click += ButtonForGeneration_Click;
            // 
            // AddEmployeeForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(678, 644);
            Controls.Add(ButtonForGeneration);
            Controls.Add(ButtonCancel);
            Controls.Add(ButtonOk);
            Controls.Add(GroupBoxParameters);
            Controls.Add(GroupBoxType);
            Controls.Add(TextBoxForAge);
            Controls.Add(LabelForAge);
            Controls.Add(LabelForGender);
            Controls.Add(ComboBoxForGender);
            Controls.Add(LabelForProfession);
            Controls.Add(ComboBoxForProfession);
            Controls.Add(LabelForSurname);
            Controls.Add(TextBoxForSurname);
            Controls.Add(LabelForName);
            Controls.Add(TextBoxForName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(700, 700);
            Name = "AddEmployeeForm";
            Text = "Добавить сотрудника";
            GroupBoxType.ResumeLayout(false);
            GroupBoxType.PerformLayout();
            GroupBoxParameters.ResumeLayout(false);
            GroupBoxParameters.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TextBoxForName;
        private Label LabelForName;
        private TextBox TextBoxForSurname;
        private Label LabelForSurname;
        private ComboBox ComboBoxForProfession;
        private Label LabelForProfession;
        private ComboBox ComboBoxForGender;
        private Label LabelForGender;
        private Label LabelForAge;
        private TextBox TextBoxForAge;
        private GroupBox GroupBoxType;
        private RadioButton RadioButtonWage;
        private RadioButton RadioButtonSalary;
        private GroupBox GroupBoxParameters;
        private Label LabelForParameter1;
        private TextBox TextBoxForParameter1;
        private TextBox TextBoxForParameter2;
        private Label LabelForParameter2;
        private Button ButtonOk;
        private Button ButtonCancel;
        private Button ButtonForGeneration;
    }
}