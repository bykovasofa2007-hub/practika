using System;
using System.Windows.Forms;

namespace _24_26_
{
    public partial class Form1 : Form
    {
        // Элементы формы (объявляем явно)
        private TextBox textBoxNickname;
        private ComboBox comboBoxClass;
        private TextBox textBoxBackstory;
        private Button buttonRegister;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            textBoxNickname = new TextBox();
            comboBoxClass = new ComboBox();
            textBoxBackstory = new TextBox();
            buttonRegister = new Button();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // textBoxNickname
            // 
            textBoxNickname.Location = new Point(34, 127);
            textBoxNickname.Name = "textBoxNickname";
            textBoxNickname.Size = new Size(186, 27);
            textBoxNickname.TabIndex = 1;
            // 
            // comboBoxClass
            // 
            comboBoxClass.Items.AddRange(new object[] { "Маг", "Целитель", "Высший Маг", "Закленатель Духов", "Убийца Драконов", "Убийца Демонов" });
            comboBoxClass.Location = new Point(34, 185);
            comboBoxClass.Name = "comboBoxClass";
            comboBoxClass.Size = new Size(186, 28);
            comboBoxClass.TabIndex = 3;
            comboBoxClass.SelectedIndexChanged += comboBoxClass_SelectedIndexChanged;
            // 
            // textBoxBackstory
            // 
            textBoxBackstory.Location = new Point(34, 244);
            textBoxBackstory.Multiline = true;
            textBoxBackstory.Name = "textBoxBackstory";
            textBoxBackstory.Size = new Size(300, 80);
            textBoxBackstory.TabIndex = 5;
            // 
            // buttonRegister
            // 
            buttonRegister.Font = new Font("Nirmala Text", 10.8F);
            buttonRegister.Location = new Point(34, 330);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(220, 53);
            buttonRegister.TabIndex = 6;
            buttonRegister.Text = "Зарегистрироваться";
            buttonRegister.Click += buttonRegister_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1, -1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(509, 81);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Nirmala Text", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Firebrick;
            label1.Location = new Point(34, 99);
            label1.Name = "label1";
            label1.Size = new Size(160, 25);
            label1.TabIndex = 10;
            label1.Text = "Введите никнейм";
            label1.Click += label1_Click_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Nirmala Text", 10.8F);
            label2.Location = new Point(34, 157);
            label2.Name = "label2";
            label2.Size = new Size(147, 25);
            label2.TabIndex = 11;
            label2.Text = "Выберите класс";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Nirmala Text", 10.8F);
            label3.Location = new Point(35, 216);
            label3.Name = "label3";
            label3.Size = new Size(194, 25);
            label3.TabIndex = 12;
            label3.Text = "Краткая информация";
            // 
            // Form1
            // 
            BackgroundImage = Properties.Resources.гиодия;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(746, 531);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(textBoxNickname);
            Controls.Add(comboBoxClass);
            Controls.Add(textBoxBackstory);
            Controls.Add(buttonRegister);
            ForeColor = Color.Firebrick;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Регистрация в Гильдии";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private string GetPlayerData()
        {
            string nickname = textBoxNickname.Text.Trim();
            string classChoice = comboBoxClass.SelectedItem?.ToString() ?? "Воин";
            string backstory = textBoxBackstory.Text.Trim();

            if (string.IsNullOrEmpty(nickname))
                nickname = "Безымянный раб";

            if (string.IsNullOrEmpty(backstory))
                backstory = "Просто путник";

            return $"Ник: {nickname}\nКласс: {classChoice}\nПредыстория: {backstory}";
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string card = GetPlayerData();
            MessageBox.Show(card, "Карточка персонажа", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void comboBoxClass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private System.ComponentModel.IContainer components;
        private PictureBox pictureBox1;

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private Label label1;

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
        private Label label2;

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private Label label3;
    }
}