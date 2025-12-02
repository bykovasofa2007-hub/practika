using System;
using System.Drawing;
using System.IO;
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
        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Label label3;

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
            comboBoxClass.Items.AddRange(new object[] { "Маг", "Целитель", "Высший Маг", "Заклинатель Духов", "Убийца Драконов", "Убийца Демонов" });
            comboBoxClass.Location = new Point(34, 185);
            comboBoxClass.Name = "comboBoxClass";
            comboBoxClass.Size = new Size(186, 28);
            comboBoxClass.TabIndex = 3;
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

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string nickname = textBoxNickname.Text.Trim();
            string classChoice = comboBoxClass.SelectedItem?.ToString() ?? "Маг";
            string backstory = textBoxBackstory.Text.Trim();

            // Условия по заданию
            if (string.IsNullOrEmpty(nickname))
                nickname = "Безымянный раб";

            if (string.IsNullOrEmpty(backstory))
                backstory = "Просто путник";

            // Создаём красивую карточку персонажа
            Form cardForm = new Form();
            cardForm.Text = "Карточка персонажа";
            cardForm.Size = new Size(600, 400);
            cardForm.StartPosition = FormStartPosition.CenterScreen;
            cardForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            cardForm.MaximizeBox = false;
            cardForm.MinimizeBox = false;
            cardForm.BackColor = Color.LightGoldenrodYellow;

            // Фото Макарова
            PictureBox picmakarov = new PictureBox
            {
                Location = new Point(20, 20),
                Size = new Size(150, 180),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // Приветствие
            Label lblGreeting = new Label
            {
                Text = "Тебя приветствует мастер Гильдии\n\"Хвост Феи\" Макаров!\nДобро пожаловать, согильдиец!",
                Location = new Point(190, 20),
                Size = new Size(380, 80),
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Данные персонажа
            Label lblNick = new Label
            {
                Text = $"Ник: {nickname}",
                Location = new Point(190, 110),
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Black
            };

            Label lblClass = new Label
            {
                Text = $"Класс: {classChoice}",
                Location = new Point(190, 140),
                AutoSize = true,
                Font = new Font("Arial", 12),
                ForeColor = Color.Black
            };

            Label lblBack = new Label
            {
                Text = $"Предыстория: {backstory}",
                Location = new Point(190, 170),
                Size = new Size(380, 60),
                Font = new Font("Arial", 11),
                ForeColor = Color.Black,
                BackColor = Color.Transparent
            };

            // Кнопка ПРОДОЛЖИТЬ
            Button btnContinue = new Button
            {
                Text = "ПРОДОЛЖИТЬ",
                Location = new Point(450, 320),
                Size = new Size(120, 35),
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.LightGreen,
                ForeColor = Color.DarkGreen
            };
            btnContinue.Click += (s, ev) =>
            {
                cardForm.Close();
                // Переходим к характеристикам
                Form2 form2 = new Form2();
                form2.Show();
                this.Hide();
            };

            // Добавляем всё на форму
            cardForm.Controls.AddRange(new Control[]
            {
                picmakarov, lblGreeting, lblNick, lblClass, lblBack, btnContinue
            });

            // Загружаем фото Макарова
            LoadmakarovImage(picmakarov);

            // Показываем карточку
            cardForm.ShowDialog();
        }

        private void LoadmakarovImage(PictureBox pictureBox)
        {
            try
            {
                // Способ 1: Пробуем загрузить из файлов в папке с проектом
                string[] possibleFiles = {
                    "makarov.jpg", "makarov.png", "макаров.jpg", "макаров.png",
                    "master.jpg", "master.png", "makaroв.jpg", "makaroв.png"
                };

                foreach (string fileName in possibleFiles)
                {
                    if (File.Exists(fileName))
                    {
                        pictureBox.Image = Image.FromFile(fileName);
                        return;
                    }
                }

                // Способ 2: Пробуем загрузить из папки Resources
                string projectPath = Directory.GetCurrentDirectory();
                string resourcesPath = Path.Combine(projectPath, "Resources");

                if (Directory.Exists(resourcesPath))
                {
                    foreach (string fileName in possibleFiles)
                    {
                        string fullPath = Path.Combine(resourcesPath, fileName);
                        if (File.Exists(fullPath))
                        {
                            pictureBox.Image = Image.FromFile(fullPath);
                            return;
                        }
                    }
                }

                // Способ 3: Создаем профессиональную заглушку
                CreateProfessionalPlaceholder(pictureBox);
            }
            catch (Exception ex)
            {
                // Если ошибка - создаем заглушку
                CreateProfessionalPlaceholder(pictureBox);
            }
        }

        private void CreateProfessionalPlaceholder(PictureBox pictureBox)
        {
            try
            {
                Bitmap placeholder = new Bitmap(150, 180);
                using (Graphics g = Graphics.FromImage(placeholder))
                {
                    // Фон
                    g.Clear(Color.SteelBlue);

                    // Рамка
                    g.DrawRectangle(new Pen(Color.Gold, 3), 5, 5, 140, 170);

                    // Символика гильдии
                    g.FillEllipse(Brushes.Gold, 50, 40, 50, 50); // Солнце
                    g.FillEllipse(Brushes.White, 55, 45, 40, 40); // Внутренний круг

                    // Текст
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    g.DrawString("Гильдия", new Font("Arial", 10, FontStyle.Bold), Brushes.DarkBlue,
                               new Rectangle(10, 100, 130, 30), format);
                    g.DrawString("Хвост Феи", new Font("Arial", 9, FontStyle.Bold), Brushes.DarkRed,
                               new Rectangle(10, 125, 130, 25), format);
                    g.DrawString("Макаров", new Font("Arial", 8, FontStyle.Italic), Brushes.Black,
                               new Rectangle(10, 150, 130, 20), format);
                }

                pictureBox.Image = placeholder;
            }
            catch
            {
                // Если даже заглушка не работает - просто белый фон
                pictureBox.BackColor = Color.White;
            }
        }
    }
}