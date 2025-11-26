using System;
using System.Drawing;
using System.Windows.Forms;

namespace _24_26_
{
    public partial class Form2 : Form
    {
        private int totalPoints = 20;
        private int strength = 0, agility = 0, intelligence = 0;

        // Элементы формы
        private NumericUpDown numericUpDownStrength;
        private NumericUpDown numericUpDownAgility;
        private NumericUpDown numericUpDownIntelligence;
        private Label labelRemaining;
        private Button buttonConfirm;

        public Form2()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            numericUpDownStrength = new NumericUpDown();
            numericUpDownAgility = new NumericUpDown();
            numericUpDownIntelligence = new NumericUpDown();
            labelRemaining = new Label();
            buttonConfirm = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStrength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAgility).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownIntelligence).BeginInit();
            SuspendLayout();
            // 
            // numericUpDownStrength
            // 
            numericUpDownStrength.Location = new Point(148, 45);
            numericUpDownStrength.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numericUpDownStrength.Name = "numericUpDownStrength";
            numericUpDownStrength.Size = new Size(120, 27);
            numericUpDownStrength.TabIndex = 1;
            numericUpDownStrength.ValueChanged += UpdateRemainingPoints;
            // 
            // numericUpDownAgility
            // 
            numericUpDownAgility.Location = new Point(148, 121);
            numericUpDownAgility.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numericUpDownAgility.Name = "numericUpDownAgility";
            numericUpDownAgility.Size = new Size(120, 27);
            numericUpDownAgility.TabIndex = 3;
            numericUpDownAgility.ValueChanged += UpdateRemainingPoints;
            // 
            // numericUpDownIntelligence
            // 
            numericUpDownIntelligence.Location = new Point(148, 199);
            numericUpDownIntelligence.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numericUpDownIntelligence.Name = "numericUpDownIntelligence";
            numericUpDownIntelligence.Size = new Size(120, 27);
            numericUpDownIntelligence.TabIndex = 5;
            numericUpDownIntelligence.ValueChanged += UpdateRemainingPoints;
            // 
            // labelRemaining
            // 
            labelRemaining.AutoSize = true;
            labelRemaining.Location = new Point(148, 247);
            labelRemaining.Name = "labelRemaining";
            labelRemaining.Size = new Size(141, 20);
            labelRemaining.TabIndex = 6;
            labelRemaining.Text = "Осталось очков: 20";
            // 
            // buttonConfirm
            // 
            buttonConfirm.Location = new Point(148, 286);
            buttonConfirm.Name = "buttonConfirm";
            buttonConfirm.Size = new Size(200, 50);
            buttonConfirm.TabIndex = 7;
            buttonConfirm.Text = "Подтвердить характеристики";
            buttonConfirm.Click += buttonConfirm_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(148, 11);
            label1.Name = "label1";
            label1.Size = new Size(43, 20);
            label1.TabIndex = 8;
            label1.Text = "Сила";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(146, 89);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 9;
            label2.Text = "Ловкость";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(146, 160);
            label3.Name = "label3";
            label3.Size = new Size(80, 20);
            label3.TabIndex = 10;
            label3.Text = "Интеллект";
            // 
            // Form2
            // 
            BackgroundImage = Properties.Resources.лол;
            ClientSize = new Size(759, 427);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(numericUpDownStrength);
            Controls.Add(numericUpDownAgility);
            Controls.Add(numericUpDownIntelligence);
            Controls.Add(labelRemaining);
            Controls.Add(buttonConfirm);
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Распределение характеристик";
            ((System.ComponentModel.ISupportInitialize)numericUpDownStrength).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAgility).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownIntelligence).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void UpdateRemainingPoints(object sender, EventArgs e)
        {
            strength = (int)numericUpDownStrength.Value;
            agility = (int)numericUpDownAgility.Value;
            intelligence = (int)numericUpDownIntelligence.Value;

            int used = strength + agility + intelligence;
            int remaining = totalPoints - used;
            labelRemaining.Text = $"Осталось очков: {remaining}";

            if (remaining < 0)
            {
                labelRemaining.ForeColor = Color.Red;
                MessageBox.Show("Слишком много очков! У тебя только 20.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (remaining > 0)
            {
                labelRemaining.ForeColor = Color.Orange;
            }
            else
            {
                labelRemaining.ForeColor = Color.Green;
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            int used = strength + agility + intelligence;
            if (used > totalPoints)
            {
                MessageBox.Show("Слишком много очков! У тебя только 20.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (used < totalPoints)
            {
                var result = MessageBox.Show("У тебя остались нераспределённые очки. Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;
            }

            PlayerStrength = strength;
            ShowDoorTest();
        }

        public int PlayerStrength { get; private set; }

        private void ShowDoorTest()
        {
            this.Hide();
            Form doorForm = new Form();
            doorForm.Text = "Проверка силы";
            doorForm.Size = new Size(400, 200);
            doorForm.StartPosition = FormStartPosition.CenterScreen;

            Label label = new Label
            {
                Text = "Ваша сила: " + PlayerStrength.ToString(),
                Location = new Point(20, 20),
                AutoSize = true
            };

            TextBox inputBox = new TextBox
            {
                Location = new Point(20, 50),
                Width = 100
            };
            inputBox.Text = PlayerStrength.ToString();

            Button testButton = new Button
            {
                Text = "Проверить дверь",
                Location = new Point(200, 50)
            };

            testButton.Click += (s, e) =>
            {
                int userStrength;
                if (!int.TryParse(inputBox.Text, out userStrength))
                {
                    MessageBox.Show("Введите корректное число!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string result = CheckDoorResult(userStrength);
                MessageBox.Show(result, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            doorForm.Controls.AddRange(new Control[] { label, inputBox, testButton });
            doorForm.ShowDialog();
            Application.Exit(); // Завершаем приложение после проверки
        }

        private string CheckDoorResult(int strength)
        {
            if (strength < 0 || strength > 30)
                return "Сила должна быть от 0 до 30.";

            if (strength < 5)
                return "Ты пытаешься выбить дверь плечом… и ломаешь себе ключицу(немощ)";
            else if (strength <= 9)
                return "Дверь отвечает глухим звуком на твою жалкую попытку ее открыть(пу-пу-пу)";
            else if (strength <= 14)
                return "Сильным движением руки, ты отпираешь дверь(хорош)";
            else if (strength <= 19)
                return "Дверь разлетелась в щепки от твоего удара!(чини теперь, миллионер)";
            else
                return "ТЫДЫЩ!!!!! Дверь не просто открывается — её выбивает ударная волна!";
        }
        private Label label1;
        private Label label2;
        private Label label3;

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}