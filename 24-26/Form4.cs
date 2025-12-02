using _24_26_;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonGenerator
{
    public partial class Form4 : Form
    {
        // Константы
        private const int MAP_SIZE = 10;
        private const int CELL_SIZE = 35;
        private const int MAX_LEVEL = 5;

        // Карта и игровые данные
        private int[,] dungeon = new int[MAP_SIZE, MAP_SIZE];
        private int[,] enemyType = new int[MAP_SIZE, MAP_SIZE];
        private int[,] chestOpened = new int[MAP_SIZE, MAP_SIZE];
        private bool[,] visited = new bool[MAP_SIZE, MAP_SIZE];
        private int playerX = 1;
        private int playerY = 1;
        private int currentLevel = 1;

        // Статистика героя
        private int heroHP = 100;
        private int maxHP = 100;
        private int heroStrength = 15;
        private int heroGold = 0;
        private int heroExp = 0;
        private int heroPotions = 0;
        private string heroWeapon = "Меч";
        private string heroArmor = "Кожаная";

        // Счетчики
        private int totalEnemies = 0;
        private int weakEnemies = 0;
        private int normalEnemies = 0;
        private int strongEnemies = 0;
        private int chestsRemaining = 0;
        private int restPointsRemaining = 0;
        private int exploredCells = 0;

        // Элементы управления
        private Panel mapPanel = new Panel();
        private Panel minimapPanel = new Panel();
        private Button btnUp = new Button();
        private Button btnDown = new Button();
        private Button btnLeft = new Button();
        private Button btnRight = new Button();
        private Button btnNewMap = new Button();
        private Button btnUsePotion = new Button();
        private Button btnBack = new Button();
        private Button btnUseInventory = new Button(); // НОВАЯ КНОПКА
        private Label lblLevel = new Label();
        private Label lblPosition = new Label();
        private Label lblHP = new Label();
        private Label lblStrength = new Label();
        private Label lblGold = new Label();
        private Label lblExp = new Label();
        private Label lblPotions = new Label();
        private Label lblWeapon = new Label();
        private Label lblArmor = new Label();
        private Label lblEnemiesRemaining = new Label();
        private Label lblWeak = new Label();
        private Label lblNormal = new Label();
        private Label lblStrong = new Label();
        private Label lblChests = new Label();
        private Label lblRestPoints = new Label();
        private Label lblExplored = new Label();
        private Label lblMinimapTitle = new Label();
        private Label lblInventoryTitle = new Label();
        private ProgressBar hpBar = new ProgressBar();
        private RichTextBox logBox = new RichTextBox();
        private ListBox inventoryList = new ListBox();

        // Списки
        private List<string> inventory = new List<string>();
        private Random random = new Random();

        public Form4()
        {
            InitializeComponent();
            InitializeInterface();
            InitializeGame();
        }

        private void InitializeInterface()
        {
            // Настройка формы
            this.Text = "Подземелье - Уровень 2";
            this.Size = new Size(1400, 950);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(40, 40, 60);
            this.Font = new Font("Segoe UI", 8.5f, FontStyle.Regular);

            // Основная карта
            mapPanel.BackColor = Color.Black;
            mapPanel.BorderStyle = BorderStyle.Fixed3D;
            mapPanel.Location = new Point(20, 50);
            mapPanel.Size = new Size(MAP_SIZE * CELL_SIZE + 4, MAP_SIZE * CELL_SIZE + 4);
            mapPanel.Paint += MapPanel_Paint;

            // Заголовок карты
            Label lblMapTitle = new Label();
            lblMapTitle.Text = "КАРТА ПОДЗЕМЕЛЬЯ";
            lblMapTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblMapTitle.ForeColor = Color.Gold;
            lblMapTitle.Location = new Point(20, 20);
            lblMapTitle.Size = new Size(200, 25);
            this.Controls.Add(lblMapTitle);

            // Миникарта
            minimapPanel.BackColor = Color.Black;
            minimapPanel.BorderStyle = BorderStyle.Fixed3D;
            minimapPanel.Location = new Point(20, 420);
            minimapPanel.Size = new Size(180, 180);
            minimapPanel.Paint += MinimapPanel_Paint;

            // Заголовок миникарты
            lblMinimapTitle.Text = "МИНИКАРТА";
            lblMinimapTitle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblMinimapTitle.ForeColor = Color.LightBlue;
            lblMinimapTitle.Location = new Point(20, 400);
            lblMinimapTitle.Size = new Size(150, 20);

            // Кнопки управления
            btnUp.Text = "↑";
            btnUp.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnUp.BackColor = Color.DarkSlateBlue;
            btnUp.ForeColor = Color.White;
            btnUp.Location = new Point(80, 610);
            btnUp.Size = new Size(60, 35);
            btnUp.Click += BtnUp_Click;

            btnLeft.Text = "←";
            btnLeft.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnLeft.BackColor = Color.DarkSlateBlue;
            btnLeft.ForeColor = Color.White;
            btnLeft.Location = new Point(20, 650);
            btnLeft.Size = new Size(60, 35);
            btnLeft.Click += BtnLeft_Click;

            btnDown.Text = "↓";
            btnDown.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnDown.BackColor = Color.DarkSlateBlue;
            btnDown.ForeColor = Color.White;
            btnDown.Location = new Point(80, 650);
            btnDown.Size = new Size(60, 35);
            btnDown.Click += BtnDown_Click;

            btnRight.Text = "→";
            btnRight.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnRight.BackColor = Color.DarkSlateBlue;
            btnRight.ForeColor = Color.White;
            btnRight.Location = new Point(140, 650);
            btnRight.Size = new Size(60, 35);
            btnRight.Click += BtnRight_Click;

            btnNewMap.Text = "НОВАЯ КАРТА";
            btnNewMap.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnNewMap.BackColor = Color.DarkOliveGreen;
            btnNewMap.ForeColor = Color.White;
            btnNewMap.Location = new Point(20, 700);
            btnNewMap.Size = new Size(180, 35);
            btnNewMap.Click += BtnNewMap_Click;

            // Кнопка возврата в меню
            btnBack.Text = "ВЕРНУТЬСЯ";
            btnBack.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnBack.BackColor = Color.DarkOrange;
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(1140, 10);
            btnBack.Size = new Size(150, 35);
            btnBack.Click += BtnBack_Click;

            // Панель статуса героя
            Panel statusPanel = new Panel();
            statusPanel.BackColor = Color.FromArgb(60, 60, 80);
            statusPanel.BorderStyle = BorderStyle.FixedSingle;
            statusPanel.Location = new Point(400, 50);
            statusPanel.Size = new Size(350, 350);

            Label lblStatusTitle = new Label();
            lblStatusTitle.Text = "СТАТУС ГЕРОЯ";
            lblStatusTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblStatusTitle.ForeColor = Color.LightGreen;
            lblStatusTitle.Location = new Point(10, 10);
            lblStatusTitle.Size = new Size(200, 25);
            statusPanel.Controls.Add(lblStatusTitle);

            // Статус героя
            int yPos = 40;
            lblLevel.Location = new Point(15, yPos);
            lblLevel.Size = new Size(320, 20);
            lblLevel.ForeColor = Color.White;
            lblLevel.Font = new Font("Segoe UI", 9);

            yPos += 25;
            lblPosition.Location = new Point(15, yPos);
            lblPosition.Size = new Size(320, 20);
            lblPosition.ForeColor = Color.White;
            lblPosition.Font = new Font("Segoe UI", 9);

            yPos += 25;
            lblHP.Location = new Point(15, yPos);
            lblHP.Size = new Size(320, 20);
            lblHP.ForeColor = Color.White;
            lblHP.Font = new Font("Segoe UI", 9);

            yPos += 20;
            hpBar.Location = new Point(15, yPos);
            hpBar.Size = new Size(320, 18);
            hpBar.Value = 100;
            hpBar.ForeColor = Color.LimeGreen;

            yPos += 25;
            lblStrength.Location = new Point(15, yPos);
            lblStrength.Size = new Size(320, 20);
            lblStrength.ForeColor = Color.White;
            lblStrength.Font = new Font("Segoe UI", 9);

            yPos += 25;
            lblWeapon.Location = new Point(15, yPos);
            lblWeapon.Size = new Size(320, 20);
            lblWeapon.ForeColor = Color.White;
            lblWeapon.Font = new Font("Segoe UI", 9);

            yPos += 25;
            lblArmor.Location = new Point(15, yPos);
            lblArmor.Size = new Size(320, 20);
            lblArmor.ForeColor = Color.White;
            lblArmor.Font = new Font("Segoe UI", 9);

            // Ресурсы
            yPos += 30;
            Label lblResourcesTitle = new Label();
            lblResourcesTitle.Text = "РЕСУРСЫ";
            lblResourcesTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblResourcesTitle.ForeColor = Color.Gold;
            lblResourcesTitle.Location = new Point(15, yPos);
            lblResourcesTitle.Size = new Size(200, 20);
            statusPanel.Controls.Add(lblResourcesTitle);

            yPos += 25;
            lblGold.Location = new Point(15, yPos);
            lblGold.Size = new Size(320, 20);
            lblGold.ForeColor = Color.Gold;
            lblGold.Font = new Font("Segoe UI", 9);

            yPos += 25;
            lblExp.Location = new Point(15, yPos);
            lblExp.Size = new Size(320, 20);
            lblExp.ForeColor = Color.Cyan;
            lblExp.Font = new Font("Segoe UI", 9);

            yPos += 25;
            lblPotions.Location = new Point(15, yPos);
            lblPotions.Size = new Size(320, 20);
            lblPotions.ForeColor = Color.LightCoral;
            lblPotions.Font = new Font("Segoe UI", 9);

            yPos += 25;
            btnUsePotion.Text = "ИСПОЛЬЗОВАТЬ ЗЕЛЬЕ";
            btnUsePotion.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnUsePotion.BackColor = Color.DarkRed;
            btnUsePotion.ForeColor = Color.White;
            btnUsePotion.Location = new Point(15, yPos);
            btnUsePotion.Size = new Size(320, 30);
            btnUsePotion.Click += BtnUsePotion_Click;

            // Добавляем все в статус панель
            statusPanel.Controls.Add(lblLevel);
            statusPanel.Controls.Add(lblPosition);
            statusPanel.Controls.Add(lblHP);
            statusPanel.Controls.Add(hpBar);
            statusPanel.Controls.Add(lblStrength);
            statusPanel.Controls.Add(lblWeapon);
            statusPanel.Controls.Add(lblArmor);
            statusPanel.Controls.Add(lblGold);
            statusPanel.Controls.Add(lblExp);
            statusPanel.Controls.Add(lblPotions);
            statusPanel.Controls.Add(btnUsePotion);

            // Панель статистики
            Panel statsPanel = new Panel();
            statsPanel.BackColor = Color.FromArgb(60, 60, 80);
            statsPanel.BorderStyle = BorderStyle.FixedSingle;
            statsPanel.Location = new Point(770, 50);
            statsPanel.Size = new Size(350, 350);

            Label lblStatsTitle = new Label();
            lblStatsTitle.Text = "СТАТИСТИКА УРОВНЯ";
            lblStatsTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblStatsTitle.ForeColor = Color.LightBlue;
            lblStatsTitle.Location = new Point(10, 10);
            lblStatsTitle.Size = new Size(250, 25);
            statsPanel.Controls.Add(lblStatsTitle);

            // Статистика
            int statYPos = 40;

            lblEnemiesRemaining.Location = new Point(15, statYPos);
            lblEnemiesRemaining.Size = new Size(320, 20);
            lblEnemiesRemaining.ForeColor = Color.White;
            lblEnemiesRemaining.Font = new Font("Segoe UI", 9);

            statYPos += 25;
            lblWeak.Location = new Point(25, statYPos);
            lblWeak.Size = new Size(310, 20);
            lblWeak.ForeColor = Color.Yellow;
            lblWeak.Font = new Font("Segoe UI", 9);

            statYPos += 22;
            lblNormal.Location = new Point(25, statYPos);
            lblNormal.Size = new Size(310, 20);
            lblNormal.ForeColor = Color.Orange;
            lblNormal.Font = new Font("Segoe UI", 9);

            statYPos += 22;
            lblStrong.Location = new Point(25, statYPos);
            lblStrong.Size = new Size(310, 20);
            lblStrong.ForeColor = Color.Red;
            lblStrong.Font = new Font("Segoe UI", 9);

            statYPos += 25;
            lblChests.Location = new Point(15, statYPos);
            lblChests.Size = new Size(320, 20);
            lblChests.ForeColor = Color.Gold;
            lblChests.Font = new Font("Segoe UI", 9);

            statYPos += 25;
            lblRestPoints.Location = new Point(15, statYPos);
            lblRestPoints.Size = new Size(320, 20);
            lblRestPoints.ForeColor = Color.LightGreen;
            lblRestPoints.Font = new Font("Segoe UI", 9);

            statYPos += 25;
            lblExplored.Location = new Point(15, statYPos);
            lblExplored.Size = new Size(320, 20);
            lblExplored.ForeColor = Color.Cyan;
            lblExplored.Font = new Font("Segoe UI", 9);

            statsPanel.Controls.Add(lblEnemiesRemaining);
            statsPanel.Controls.Add(lblWeak);
            statsPanel.Controls.Add(lblNormal);
            statsPanel.Controls.Add(lblStrong);
            statsPanel.Controls.Add(lblChests);
            statsPanel.Controls.Add(lblRestPoints);
            statsPanel.Controls.Add(lblExplored);

            // Лог событий
            Panel logPanel = new Panel();
            logPanel.BackColor = Color.FromArgb(60, 60, 80);
            logPanel.BorderStyle = BorderStyle.FixedSingle;
            logPanel.Location = new Point(1140, 50);
            logPanel.Size = new Size(230, 350);

            Label lblLogTitle = new Label();
            lblLogTitle.Text = "ЖУРНАЛ";
            lblLogTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblLogTitle.ForeColor = Color.LightGray;
            lblLogTitle.Location = new Point(10, 10);
            lblLogTitle.Size = new Size(200, 25);
            logPanel.Controls.Add(lblLogTitle);

            logBox.BackColor = Color.Black;
            logBox.ForeColor = Color.Lime;
            logBox.Font = new Font("Consolas", 8);
            logBox.Location = new Point(10, 40);
            logBox.Size = new Size(210, 300);
            logBox.ReadOnly = true;
            logPanel.Controls.Add(logBox);

            // Панель инвентаря
            Panel inventoryPanel = new Panel();
            inventoryPanel.BackColor = Color.FromArgb(60, 60, 80);
            inventoryPanel.BorderStyle = BorderStyle.FixedSingle;
            inventoryPanel.Location = new Point(20, 750);
            inventoryPanel.Size = new Size(1350, 160);

            lblInventoryTitle.Text = "ИНВЕНТАРЬ";
            lblInventoryTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblInventoryTitle.ForeColor = Color.LightGoldenrodYellow;
            lblInventoryTitle.Location = new Point(10, 10);
            lblInventoryTitle.Size = new Size(200, 25);
            inventoryPanel.Controls.Add(lblInventoryTitle);

            inventoryList.BackColor = Color.Black;
            inventoryList.ForeColor = Color.White;
            inventoryList.Font = new Font("Segoe UI", 9);
            inventoryList.Location = new Point(10, 40);
            inventoryList.Size = new Size(1220, 110);
            inventoryList.HorizontalScrollbar = true;
            inventoryList.SelectedIndexChanged += InventoryList_SelectedIndexChanged;
            inventoryPanel.Controls.Add(inventoryList);

            // Кнопка использования предмета из инвентаря
            btnUseInventory.Text = "ИСПОЛЬЗОВАТЬ ПРЕДМЕТ";
            btnUseInventory.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnUseInventory.BackColor = Color.Purple;
            btnUseInventory.ForeColor = Color.White;
            btnUseInventory.Location = new Point(1240, 40);
            btnUseInventory.Size = new Size(100, 50);
            btnUseInventory.Click += BtnUseInventory_Click;
            btnUseInventory.Enabled = false;
            inventoryPanel.Controls.Add(btnUseInventory);

            // Добавляем все элементы на форму
            this.Controls.Add(mapPanel);
            this.Controls.Add(minimapPanel);
            this.Controls.Add(lblMinimapTitle);
            this.Controls.Add(btnUp);
            this.Controls.Add(btnLeft);
            this.Controls.Add(btnDown);
            this.Controls.Add(btnRight);
            this.Controls.Add(btnNewMap);
            this.Controls.Add(btnBack); // Добавлена на форму
            this.Controls.Add(statusPanel);
            this.Controls.Add(statsPanel);
            this.Controls.Add(logPanel);
            this.Controls.Add(inventoryPanel);
        }

        private void InitializeGame()
        {
            // Генерация первого уровня
            GenerateDungeon(currentLevel);
            UpdateStatistics();
            AddLog("Добро пожаловать в подземелье! Уровень " + currentLevel);
        }

        private void InventoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Активируем кнопку, если выбран предмет
            btnUseInventory.Enabled = inventoryList.SelectedIndex >= 0;
        }

        private void BtnUseInventory_Click(object sender, EventArgs e)
        {
            if (inventoryList.SelectedIndex < 0 || inventoryList.SelectedIndex >= inventory.Count)
            {
                MessageBox.Show("Выберите предмет из инвентаря!", "Инвентарь",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedItem = inventory[inventoryList.SelectedIndex];

            // Обработка разных типов предметов
            if (selectedItem.Contains("Зелье здоровья"))
            {
                UseHealthPotion();
            }
            else if (selectedItem.Contains("Меч") || selectedItem.Contains("топор") ||
                     selectedItem.Contains("Кинжал") || selectedItem.Contains("Булава") ||
                     selectedItem.Contains("Копье"))
            {
                UseWeapon(selectedItem);
            }
            else if (selectedItem.Contains("Кожаная") || selectedItem.Contains("Стальная") ||
                     selectedItem.Contains("Кольчужная") || selectedItem.Contains("Драконья"))
            {
                UseArmor(selectedItem);
            }
            else
            {
                AddLog($"Неизвестный предмет: {selectedItem}");
            }

            UpdateInventory();
            UpdateStatistics();
        }

        private void UseHealthPotion()
        {
            if (heroPotions > 0)
            {
                heroHP = Math.Min(heroHP + 50, maxHP);
                heroPotions--;
                inventory.Remove("Зелье здоровья");
                AddLog("Использовано зелье здоровья (+50 HP)");
            }
            else
            {
                MessageBox.Show("У тебя не осталось зелий!", "Инвентарь",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UseWeapon(string weapon)
        {
            // Определяем бонус силы от оружия
            int bonus = 0;
            if (weapon.Contains("+"))
            {
                string[] parts = weapon.Split('+');
                if (parts.Length > 1)
                {
                    string bonusStr = parts[1].Replace(" к силе", "").Trim();
                    int.TryParse(bonusStr, out bonus);
                }
            }

            // Устанавливаем новое оружие
            heroWeapon = weapon.Split('+')[0].Trim();
            heroStrength = 15 + bonus; // Базовая сила 15 + бонус

            AddLog($"Экипировано оружие: {weapon}");
        }

        private void UseArmor(string armor)
        {
            heroArmor = armor;
            AddLog($"Экипирована броня: {armor}");
        }

        private void GenerateDungeon(int level)
        {
            // Сброс данных
            for (int x = 0; x < MAP_SIZE; x++)
            {
                for (int y = 0; y < MAP_SIZE; y++)
                {
                    dungeon[x, y] = 0;
                    enemyType[x, y] = 0;
                    chestOpened[x, y] = 0;
                    visited[x, y] = false;
                }
            }

            // Создаем комнаты
            List<Point> floorCells = new List<Point>();
            for (int x = 1; x < MAP_SIZE - 1; x++)
            {
                for (int y = 1; y < MAP_SIZE - 1; y++)
                {
                    if (random.Next(100) < 70)
                    {
                        dungeon[x, y] = 1;
                        floorCells.Add(new Point(x, y));
                    }
                }
            }

            // Добавляем гарантированный путь
            for (int i = 0; i < floorCells.Count - 1; i++)
            {
                if (random.Next(100) < 30)
                {
                    dungeon[floorCells[i].X, floorCells[i].Y] = 1;
                    dungeon[floorCells[i + 1].X, floorCells[i + 1].Y] = 1;
                }
            }

            // Размещаем врагов
            int weakCount = Math.Max(1, 5 - level);
            int normalCount = Math.Min(level + 1, 4);
            int strongCount = Math.Max(0, level - 2);

            PlaceObjects(floorCells, 2, weakCount, 1);
            PlaceObjects(floorCells, 2, normalCount, 2);
            PlaceObjects(floorCells, 2, strongCount, 3);

            // Размещаем сундуки
            PlaceObjects(floorCells, 3, 2 + level / 2, 0);

            // Размещаем места отдыха
            PlaceObjects(floorCells, 4, 1, 0);

            // Размещаем выход
            if (floorCells.Count > 0)
            {
                Point exit = floorCells[floorCells.Count - 1];
                dungeon[exit.X, exit.Y] = 5;
            }

            // Стартовая позиция
            if (floorCells.Count > 0)
            {
                playerX = floorCells[0].X;
                playerY = floorCells[0].Y;
                dungeon[playerX, playerY] = 1;
                visited[playerX, playerY] = true;
            }
            else
            {
                playerX = 1;
                playerY = 1;
                dungeon[playerX, playerY] = 1;
                visited[playerX, playerY] = true;
            }

            UpdateStatistics();
            RefreshMap();
        }

        private void PlaceObjects(List<Point> floorCells, int objectType, int count, int enemyTypeValue)
        {
            for (int i = 0; i < count && floorCells.Count > 0; i++)
            {
                int index = random.Next(floorCells.Count);
                Point cell = floorCells[index];

                if (!(cell.X == playerX && cell.Y == playerY) && dungeon[cell.X, cell.Y] == 1)
                {
                    dungeon[cell.X, cell.Y] = objectType;
                    if (objectType == 2)
                    {
                        enemyType[cell.X, cell.Y] = enemyTypeValue;
                    }
                    floorCells.RemoveAt(index);
                }
            }
        }

        private void MovePlayer(int deltaX, int deltaY)
        {
            int newX = playerX + deltaX;
            int newY = playerY + deltaY;

            if (newX < 0 || newX >= MAP_SIZE || newY < 0 || newY >= MAP_SIZE)
            {
                AddLog("Нельзя выйти за пределы карты!");
                return;
            }

            if (dungeon[newX, newY] == 0)
            {
                AddLog("Тут стена!");
                return;
            }

            if (enemyType[newX, newY] > 0)
            {
                FightEnemy(newX, newY, enemyType[newX, newY]);
                return;
            }

            if (dungeon[newX, newY] == 3)
            {
                OpenChest(newX, newY);
            }

            if (dungeon[newX, newY] == 4)
            {
                UseRestPoint(newX, newY);
            }

            if (dungeon[newX, newY] == 5)
            {
                GoToNextLevel();
                return;
            }

            playerX = newX;
            playerY = newY;
            visited[playerX, playerY] = true;

            UpdateStatistics();
            RefreshMap();
            AddLog($"Перемещение: ({playerX}, {playerY})");
        }

        private void FightEnemy(int enemyX, int enemyY, int enemyTypeValue)
        {
            string enemyName = "";
            int enemyHP = 0;
            int enemyDamageMin = 0, enemyDamageMax = 0;
            int goldReward = 0, expReward = 0;

            switch (enemyTypeValue)
            {
                case 1:
                    enemyName = "Слабый гоблин";
                    enemyHP = 20 + currentLevel * 5;
                    enemyDamageMin = 3 + currentLevel;
                    enemyDamageMax = 7 + currentLevel;
                    goldReward = random.Next(5 + currentLevel * 2, 11 + currentLevel * 3);
                    expReward = 10 + currentLevel * 5;
                    break;
                case 2:
                    enemyName = "Орк-воин";
                    enemyHP = 40 + currentLevel * 8;
                    enemyDamageMin = 5 + currentLevel;
                    enemyDamageMax = 10 + currentLevel * 2;
                    goldReward = random.Next(15 + currentLevel * 3, 26 + currentLevel * 5);
                    expReward = 25 + currentLevel * 8;
                    break;
                case 3:
                    enemyName = "Тролль-берсерк";
                    enemyHP = 60 + currentLevel * 12;
                    enemyDamageMin = 8 + currentLevel * 2;
                    enemyDamageMax = 15 + currentLevel * 3;
                    goldReward = random.Next(30 + currentLevel * 5, 51 + currentLevel * 8);
                    expReward = 50 + currentLevel * 12;
                    break;
            }

            AddLog($"Встреча с {enemyName}!");

            while (heroHP > 0 && enemyHP > 0)
            {
                int heroDamage = heroStrength + random.Next(5, 11) + (heroWeapon == "Меч" ? 5 : 0);
                enemyHP -= heroDamage;
                AddLog($"Вы нанесли {heroDamage} урона");

                if (enemyHP <= 0) break;

                int enemyDamage = random.Next(enemyDamageMin, enemyDamageMax + 1);
                int armorReduction = heroArmor == "Кожаная" ? 2 : heroArmor == "Стальная" ? 5 : 0;
                enemyDamage = Math.Max(1, enemyDamage - armorReduction);
                heroHP -= enemyDamage;
                AddLog($"{enemyName} нанес {enemyDamage} урона");

                if (heroHP <= 0)
                {
                    heroHP = 0;
                    AddLog("Вы пали в бою!");
                    GameOver();
                    return;
                }
            }

            if (enemyHP <= 0)
            {
                dungeon[enemyX, enemyY] = 1;
                enemyType[enemyX, enemyY] = 0;
                heroGold += goldReward;
                heroExp += expReward;

                AddLog($"Враг побеждён! +{goldReward} золота, +{expReward} опыта");

                playerX = enemyX;
                playerY = enemyY;
                visited[playerX, playerY] = true;
            }

            UpdateStatistics();
            RefreshMap();
        }

        private void OpenChest(int chestX, int chestY)
        {
            if (chestOpened[chestX, chestY] == 1)
            {
                AddLog("Сундук уже открыт!");
                return;
            }

            chestOpened[chestX, chestY] = 1;
            dungeon[chestX, chestY] = 1;

            int lootType = random.Next(1, 101);
            string lootMessage = "";

            if (lootType <= 40)
            {
                int gold = random.Next(20, 101) + currentLevel * 10;
                heroGold += gold;
                lootMessage = $"{gold} золота";
            }
            else if (lootType <= 70)
            {
                string[] weapons = { "Меч", "Боевой топор", "Кинжал", "Булава", "Копье" };
                string weapon = weapons[random.Next(weapons.Length)];
                int bonus = random.Next(3, 9);
                heroStrength += bonus;
                lootMessage = $"{weapon} (+{bonus} к силе)";
                inventory.Add(lootMessage);
                UpdateInventory();
            }
            else if (lootType <= 90)
            {
                string[] armors = { "Кожаная", "Стальная", "Кольчужная", "Драконья" };
                string armor = armors[random.Next(armors.Length)];
                lootMessage = $"Броня: {armor}";
                heroArmor = armor;
                inventory.Add(armor);
                UpdateInventory();
            }
            else
            {
                heroPotions++;
                lootMessage = "Зелье здоровья";
                inventory.Add("Зелье здоровья");
                UpdateInventory();
            }

            AddLog($"Вы открыли сундук! Получено: {lootMessage}");
            playerX = chestX;
            playerY = chestY;
            UpdateStatistics();
            RefreshMap();
        }

        private void UseRestPoint(int restX, int restY)
        {
            var result = MessageBox.Show("Найдена комната отдыха! Восстановить HP?", "Отдых",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int healAmount = maxHP / 2;
                heroHP = Math.Min(heroHP + healAmount, maxHP);
                dungeon[restX, restY] = 1;
                AddLog($"Вы отдохнули. Восстановлено {healAmount} HP!");
                playerX = restX;
                playerY = restY;
                UpdateStatistics();
                RefreshMap();
            }
            else
            {
                AddLog("Вы прошли мимо комнаты отдыха.");
                playerX = restX;
                playerY = restY;
            }

            visited[playerX, playerY] = true;
        }

        private void GoToNextLevel()
        {
            if (totalEnemies > 0)
            {
                MessageBox.Show("Враги ещё остались! Разберись с ними.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentLevel >= MAX_LEVEL)
            {
                MessageBox.Show($"Поздравляем! Вы прошли все {MAX_LEVEL} уровней!\n" +
                               $"Итог: {heroGold} золота, {heroExp} опыта",
                               "Победа!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show($"Ты нашёл выход! Перейти на уровень {currentLevel + 1}?",
                "Переход на следующий уровень", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                currentLevel++;
                GenerateDungeon(currentLevel);
                AddLog($"Переход на уровень {currentLevel}");
                UpdateStatistics();
                RefreshMap();
            }
        }

        private void GameOver()
        {
            var result = MessageBox.Show($"Вы погибли на уровне {currentLevel}!\n" +
                                        $"Золото: {heroGold}, Опыт: {heroExp}\n" +
                                        "Начать уровень заново?",
                                        "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                heroHP = maxHP;
                GenerateDungeon(currentLevel);
                UpdateStatistics();
                RefreshMap();
                AddLog("Уровень перезапущен!");
            }
            else
            {
                this.Close();
            }
        }

        private void UpdateStatistics()
        {
            totalEnemies = 0;
            weakEnemies = 0;
            normalEnemies = 0;
            strongEnemies = 0;
            chestsRemaining = 0;
            restPointsRemaining = 0;
            exploredCells = 0;

            for (int x = 0; x < MAP_SIZE; x++)
            {
                for (int y = 0; y < MAP_SIZE; y++)
                {
                    if (visited[x, y]) exploredCells++;

                    if (enemyType[x, y] > 0)
                    {
                        totalEnemies++;
                        switch (enemyType[x, y])
                        {
                            case 1: weakEnemies++; break;
                            case 2: normalEnemies++; break;
                            case 3: strongEnemies++; break;
                        }
                    }

                    if (dungeon[x, y] == 3 && chestOpened[x, y] == 0) chestsRemaining++;
                    if (dungeon[x, y] == 4) restPointsRemaining++;
                }
            }

            lblLevel.Text = $"Уровень: {currentLevel} из {MAX_LEVEL}";
            lblPosition.Text = $"Позиция: ({playerX}, {playerY})";
            lblHP.Text = $"HP: {heroHP}/{maxHP}";
            lblStrength.Text = $"Сила: {heroStrength}";
            lblWeapon.Text = $"Вооружение: {heroWeapon}";
            lblArmor.Text = $"Броня: {heroArmor}";
            lblGold.Text = $"Золото: {heroGold}";
            lblExp.Text = $"Опыт: {heroExp}";
            lblPotions.Text = $"Зелья: {heroPotions}";
            lblEnemiesRemaining.Text = $"Врагов осталось: {totalEnemies}";
            lblWeak.Text = $"Слабых: {weakEnemies}";
            lblNormal.Text = $"Обычных: {normalEnemies}";
            lblStrong.Text = $"Сильных: {strongEnemies}";
            lblChests.Text = $"Сундуков закрытых: {chestsRemaining}";
            lblRestPoints.Text = $"Мест отдыха: {restPointsRemaining}";

            int totalCells = MAP_SIZE * MAP_SIZE;
            int exploredPercent = (exploredCells * 100) / totalCells;
            lblExplored.Text = $"Исследовано: {exploredPercent}%";

            hpBar.Value = (heroHP * 100) / maxHP;
            hpBar.ForeColor = heroHP > 70 ? Color.Green : heroHP > 30 ? Color.Orange : Color.Red;
        }

        private void UpdateInventory()
        {
            inventoryList.Items.Clear();
            foreach (var item in inventory)
            {
                inventoryList.Items.Add(item);
            }
        }

        private void RefreshMap()
        {
            mapPanel.Invalidate();
            minimapPanel.Invalidate();
        }

        private void MapPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);

            for (int x = 0; x < MAP_SIZE; x++)
            {
                for (int y = 0; y < MAP_SIZE; y++)
                {
                    Rectangle rect = new Rectangle(x * CELL_SIZE, y * CELL_SIZE, CELL_SIZE, CELL_SIZE);

                    Color bgColor = Color.DarkSlateGray;
                    string symbol = "";
                    Color symbolColor = Color.White;
                    Font symbolFont = new Font("Arial", 12, FontStyle.Bold);

                    if (x == playerX && y == playerY)
                    {
                        bgColor = Color.LimeGreen;
                        symbol = "😊";
                        symbolFont = new Font("Arial", 10);
                    }
                    else if (enemyType[x, y] > 0)
                    {
                        switch (enemyType[x, y])
                        {
                            case 1:
                                bgColor = Color.Yellow;
                                symbol = "👹";
                                symbolColor = Color.Black;
                                break;
                            case 2:
                                bgColor = Color.Orange;
                                symbol = "🤖";
                                symbolColor = Color.Black;
                                break;
                            case 3:
                                bgColor = Color.Red;
                                symbol = "👿";
                                symbolColor = Color.White;
                                break;
                        }
                    }
                    else if (dungeon[x, y] == 0)
                    {
                        bgColor = Color.DarkSlateGray;
                        symbol = "#";
                        symbolColor = Color.Gray;
                    }
                    else if (dungeon[x, y] == 1)
                    {
                        bgColor = visited[x, y] ? Color.FromArgb(80, 80, 80) : Color.FromArgb(50, 50, 50);
                        symbol = visited[x, y] ? "." : " ";
                    }
                    else if (dungeon[x, y] == 3)
                    {
                        bgColor = chestOpened[x, y] == 0 ? Color.Gold : Color.FromArgb(80, 80, 80);
                        symbol = chestOpened[x, y] == 0 ? "💰" : ".";
                        symbolColor = Color.Black;
                    }
                    else if (dungeon[x, y] == 4)
                    {
                        bgColor = Color.LightGreen;
                        symbol = "🛏️";
                        symbolColor = Color.DarkGreen;
                    }
                    else if (dungeon[x, y] == 5)
                    {
                        bgColor = totalEnemies == 0 ? Color.Cyan : Color.FromArgb(80, 80, 80);
                        symbol = totalEnemies == 0 ? "🚪" : " ";
                        symbolColor = Color.DarkBlue;
                    }

                    using (Brush brush = new SolidBrush(bgColor))
                    {
                        g.FillRectangle(brush, rect);
                    }

                    g.DrawRectangle(Pens.Black, rect);

                    if (!string.IsNullOrEmpty(symbol) && symbol != " ")
                    {
                        SizeF textSize = g.MeasureString(symbol, symbolFont);
                        float xPos = rect.Left + (rect.Width - textSize.Width) / 2;
                        float yPos = rect.Top + (rect.Height - textSize.Height) / 2;

                        using (Brush textBrush = new SolidBrush(symbolColor))
                        {
                            g.DrawString(symbol, symbolFont, textBrush, xPos, yPos);
                        }
                    }
                }
            }

            using (Pen gridPen = new Pen(Color.FromArgb(100, 100, 100), 1))
            {
                for (int x = 0; x <= MAP_SIZE; x++)
                {
                    g.DrawLine(gridPen, x * CELL_SIZE, 0, x * CELL_SIZE, MAP_SIZE * CELL_SIZE);
                }
                for (int y = 0; y <= MAP_SIZE; y++)
                {
                    g.DrawLine(gridPen, 0, y * CELL_SIZE, MAP_SIZE * CELL_SIZE, y * CELL_SIZE);
                }
            }
        }

        private void MinimapPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);

            int miniCellSize = 18;

            for (int x = 0; x < MAP_SIZE; x++)
            {
                for (int y = 0; y < MAP_SIZE; y++)
                {
                    Rectangle rect = new Rectangle(x * miniCellSize, y * miniCellSize, miniCellSize, miniCellSize);

                    Color color = Color.DarkSlateGray;

                    if (x == playerX && y == playerY)
                    {
                        color = Color.LimeGreen;
                    }
                    else if (enemyType[x, y] > 0)
                    {
                        switch (enemyType[x, y])
                        {
                            case 1: color = Color.Yellow; break;
                            case 2: color = Color.Orange; break;
                            case 3: color = Color.Red; break;
                        }
                    }
                    else if (dungeon[x, y] == 1)
                    {
                        color = visited[x, y] ? Color.FromArgb(100, 100, 100) : Color.FromArgb(60, 60, 60);
                    }
                    else if (dungeon[x, y] == 3)
                    {
                        color = chestOpened[x, y] == 0 ? Color.Gold : Color.FromArgb(100, 100, 100);
                    }
                    else if (dungeon[x, y] == 4)
                    {
                        color = Color.LightGreen;
                    }
                    else if (dungeon[x, y] == 5)
                    {
                        color = totalEnemies == 0 ? Color.Cyan : Color.FromArgb(100, 100, 100);
                    }

                    using (Brush brush = new SolidBrush(color))
                    {
                        g.FillRectangle(brush, rect);
                    }
                }
            }
        }

        private void AddLog(string message)
        {
            logBox.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\n");
            logBox.ScrollToCaret();
        }

        private void BtnUp_Click(object sender, EventArgs e) => MovePlayer(0, -1);
        private void BtnDown_Click(object sender, EventArgs e) => MovePlayer(0, 1);
        private void BtnLeft_Click(object sender, EventArgs e) => MovePlayer(-1, 0);
        private void BtnRight_Click(object sender, EventArgs e) => MovePlayer(1, 0);

        private void BtnNewMap_Click(object sender, EventArgs e)
        {
            GenerateDungeon(currentLevel);
            AddLog("Сгенерирована новая карта уровня " + currentLevel);
        }

        private void BtnUsePotion_Click(object sender, EventArgs e)
        {
            UseHealthPotion();
            UpdateStatistics();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
        }
    }
}