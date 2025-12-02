using System;
using System.Drawing;
using System.Windows.Forms;

namespace _24_26_
{
    public class Form2 : Form
    {
        private int totalPoints = 20;
        private NumericUpDown numStrength, numAgility, numIntelligence;
        private Label labelRemaining;
        private Button buttonConfirm;
        private Button buttonCheckDoor;
        private int playerStrength;

        public Form2()
        {
            InitializeComponent();
            UpdateRemaining(null, EventArgs.Empty); // Инициализируем текст оставшихся очков
        }

        private void InitializeComponent()
        {
            labelTitle = new Label();
            lblStr = new Label();
            numStrength = new NumericUpDown();
            lblAgi = new Label();
            numAgility = new NumericUpDown();
            lblInt = new Label();
            numIntelligence = new NumericUpDown();
            labelRemaining = new Label();
            buttonCheckDoor = new Button();
            buttonConfirm = new Button();

            ((System.ComponentModel.ISupportInitialize)numStrength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numAgility).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numIntelligence).BeginInit();
            SuspendLayout();

            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            labelTitle.Location = new Point(150, 20);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(200, 25);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Распределение характеристик";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblStr
            // 
            lblStr.AutoSize = true;
            lblStr.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblStr.Location = new Point(80, 80);
            lblStr.Name = "lblStr";
            lblStr.Size = new Size(52, 20);
            lblStr.TabIndex = 1;
            lblStr.Text = "Сила:";

            // 
            // numStrength
            // 
            numStrength.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            numStrength.Location = new Point(150, 78);
            numStrength.Name = "numStrength";
            numStrength.Size = new Size(80, 26);
            numStrength.TabIndex = 2;
            numStrength.Value = 13;
            numStrength.ValueChanged += UpdateRemaining;

            // 
            // lblAgi
            // 
            lblAgi.AutoSize = true;
            lblAgi.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblAgi.Location = new Point(80, 130);
            lblAgi.Name = "lblAgi";
            lblAgi.Size = new Size(84, 20);
            lblAgi.TabIndex = 3;
            lblAgi.Text = "Ловкость:";

            // 
            // numAgility
            // 
            numAgility.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            numAgility.Location = new Point(170, 128);
            numAgility.Name = "numAgility";
            numAgility.Size = new Size(80, 26);
            numAgility.TabIndex = 4;
            numAgility.Value = 2;
            numAgility.ValueChanged += UpdateRemaining;

            // 
            // lblInt
            // 
            lblInt.AutoSize = true;
            lblInt.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblInt.Location = new Point(80, 180);
            lblInt.Name = "lblInt";
            lblInt.Size = new Size(101, 20);
            lblInt.TabIndex = 5;
            lblInt.Text = "Интеллект:";

            // 
            // numIntelligence
            // 
            numIntelligence.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            numIntelligence.Location = new Point(190, 178);
            numIntelligence.Name = "numIntelligence";
            numIntelligence.Size = new Size(80, 26);
            numIntelligence.TabIndex = 6;
            numIntelligence.Value = 5;
            numIntelligence.ValueChanged += UpdateRemaining;

            // 
            // labelRemaining
            // 
            labelRemaining.AutoSize = true;
            labelRemaining.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelRemaining.Location = new Point(150, 230);
            labelRemaining.Name = "labelRemaining";
            labelRemaining.Size = new Size(140, 20);
            labelRemaining.TabIndex = 7;
            labelRemaining.Text = "Осталось очков: 0";

            // 
            // buttonCheckDoor
            // 
            buttonCheckDoor.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCheckDoor.Location = new Point(80, 280);
            buttonCheckDoor.Name = "buttonCheckDoor";
            buttonCheckDoor.Size = new Size(120, 35);
            buttonCheckDoor.TabIndex = 8;
            buttonCheckDoor.Text = "Проверить";
            buttonCheckDoor.UseVisualStyleBackColor = true;
            buttonCheckDoor.Click += ButtonCheckDoor_Click;

            // 
            // buttonConfirm
            // 
            buttonConfirm.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            buttonConfirm.Location = new Point(250, 280);
            buttonConfirm.Name = "buttonConfirm";
            buttonConfirm.Size = new Size(120, 35);
            buttonConfirm.TabIndex = 9;
            buttonConfirm.Text = "Подтвердить";
            buttonConfirm.UseVisualStyleBackColor = true;
            buttonConfirm.Click += ButtonConfirm_Click;

            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightBlue;
            ClientSize = new Size(482, 353);
            Controls.Add(buttonConfirm);
            Controls.Add(buttonCheckDoor);
            Controls.Add(labelRemaining);
            Controls.Add(numIntelligence);
            Controls.Add(lblInt);
            Controls.Add(numAgility);
            Controls.Add(lblAgi);
            Controls.Add(numStrength);
            Controls.Add(lblStr);
            Controls.Add(labelTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Распределение характеристик";
            ((System.ComponentModel.ISupportInitialize)numStrength).EndInit();
            ((System.ComponentModel.ISupportInitialize)numAgility).EndInit();
            ((System.ComponentModel.ISupportInitialize)numIntelligence).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void UpdateRemaining(object sender, EventArgs e)
        {
            int used = (int)numStrength.Value + (int)numAgility.Value + (int)numIntelligence.Value;
            int remaining = totalPoints - used;
            labelRemaining.Text = $"Осталось очков: {remaining}";

            labelRemaining.ForeColor = remaining switch
            {
                < 0 => Color.Red,
                > 0 => Color.Orange,
                _ => Color.Green
            };
        }

        private void ButtonCheckDoor_Click(object sender, EventArgs e)
        {
            playerStrength = (int)numStrength.Value;
            string doorResult = CheckDoorResult(playerStrength);
            MessageBox.Show(doorResult, "Результат взаимодействия с дверью",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            int used = (int)numStrength.Value + (int)numAgility.Value + (int)numIntelligence.Value;

            if (used > totalPoints)
            {
                MessageBox.Show("Слишком много очков! У тебя только 20.", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (used < totalPoints)
            {
                var result = MessageBox.Show("У тебя остались нераспределённые очки. Продолжить?",
                                           "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;
            }

            playerStrength = (int)numStrength.Value;
            // Проверка двери при подтверждении
            string doorResult = CheckDoorResult(playerStrength);
            MessageBox.Show(doorResult, "Результат взаимодействия с дверью",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Переход к инвентарю
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private string CheckDoorResult(int strength)
        {
            return strength switch
            {
                < 0 => "Сила не может быть отрицательной!",
                < 5 => "Ты пытаешься выбить дверь плечом… и ломаешь себе ключицу",
                <= 9 => "Дверь отвечает глухим звуком на твою жалкую попытку ее открыть",
                <= 14 => "Сильным движением руки, ты отпираешь дверь",
                <= 19 => "Дверь разлетелась в щепки от твоего удара!",
                <= 30 => "ТЫДЫЩ!!!!! Дверь не просто открывается — её выбивает ударная волна!",
                _ => "Ты настолько сильный, что сдвинул саму реальность! Дверь исчезла."
            };
        }

        private Label labelTitle;
        private Label lblStr;
        private Label lblAgi;
        private Label lblInt;
    }
}