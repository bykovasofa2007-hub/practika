using DungeonGenerator;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace _24_26_
{
    public partial class Form3 : Form
    {
        // Элементы формы
        private ListBox listBoxInventory;
        private Label labelTitle;
        private Label labelInventoryTitle;
        private GroupBox groupBoxStats;
        private Label labelTotalItems;
        private Label labelTotalValue;
        private Button buttonAnalyze;
        private Button buttonSellJunk;
        private Label labelWallet;
        private ComboBox comboBoxShop;
        private Button buttonBuy;
        private Label labelShopTitle;
        private GroupBox groupBoxShop;
        private Button buttonGoToForm4;

        // Данные инвентаря
        private InventoryItem[] inventoryItems;
        private int wallet = 0;

        // Структура для предметов
        private struct InventoryItem
        {
            public string Name;
            public string Type;
            public int Price;
            public bool Identified;

            public InventoryItem(string name, string type, int price, bool identified = false)
            {
                Name = name;
                Type = type;
                Price = price;
                Identified = identified;
            }
        }

        public Form3()
        {
            InitializeForm();
            InitializeInventory();
            UpdateDisplay();
        }

        private void InitializeForm()
        {
            // Настройка формы
            this.Text = "Инвентарь героя";
            this.Size = new Size(700, 500);
            this.KeyPreview = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightGray;
            this.Font = new Font("Arial", 10F);

            // Заголовок
            labelTitle = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 16F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(250, 20),
                Text = "ИНВЕНТАРЬ ГЕРОЯ"
            };

            // Заголовок инвентаря
            labelInventoryTitle = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 11F, FontStyle.Bold),
                ForeColor = Color.DarkRed,
                Location = new Point(50, 60),
                Text = "СПИСОК ПРЕДМЕТОВ:"
            };

            // ListBox для инвентаря
            listBoxInventory = new ListBox
            {
                BackColor = Color.White,
                Font = new Font("Arial", 10F),
                Location = new Point(50, 90),
                Size = new Size(280, 194)
            };

            // GroupBox для статистики
            groupBoxStats = new GroupBox
            {
                BackColor = Color.LightYellow,
                Font = new Font("Arial", 11F, FontStyle.Bold),
                Location = new Point(361, 90),
                Size = new Size(300, 150),
                Text = "СТАТИСТИКА"
            };

            labelTotalItems = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 10F),
                Location = new Point(15, 30),
                Text = "Всего предметов: 0"
            };

            labelTotalValue = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 10F),
                Location = new Point(15, 60),
                Text = "Общая стоимость: 0"
            };

            labelWallet = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 10F, FontStyle.Bold),
                ForeColor = Color.Goldenrod,
                Location = new Point(15, 90),
                Text = "Золото в кошельке: 0"
            };

            groupBoxStats.Controls.Add(labelTotalItems);
            groupBoxStats.Controls.Add(labelTotalValue);
            groupBoxStats.Controls.Add(labelWallet);

            // Кнопка анализа
            buttonAnalyze = new Button
            {
                BackColor = Color.LightBlue,
                Font = new Font("Arial", 10F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(50, 310),
                Size = new Size(180, 35),
                Text = "ОПОЗНАТЬ ПРЕДМЕТЫ"
            };
            buttonAnalyze.Click += ButtonAnalyze_Click;

            // Кнопка продажи
            buttonSellJunk = new Button
            {
                BackColor = Color.LightCoral,
                Font = new Font("Arial", 9F, FontStyle.Bold),
                ForeColor = Color.DarkRed,
                Location = new Point(240, 310),
                Size = new Size(150, 35),
                Text = "ПРОДАТЬ ХЛАМ (< 100)"
            };
            buttonSellJunk.Click += ButtonSellJunk_Click;

            // GroupBox для магазина
            groupBoxShop = new GroupBox
            {
                Font = new Font("Arial", 11F, FontStyle.Bold),
                ForeColor = Color.DarkGreen,
                Location = new Point(50, 351),
                Size = new Size(600, 100),
                Text = "МАГАЗИН"
            };

            labelShopTitle = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 10F),
                ForeColor = Color.Black,
                Location = new Point(103, 0),
                Text = "Выберите товар для покупки:"
            };

            comboBoxShop = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Arial", 10F),
                Location = new Point(30, 37),
                Size = new Size(250, 27)
            };
            comboBoxShop.Items.AddRange(new object[] {
                "Меч рыцаря (1000 монет)",
                "Арбалет снайпера (750 монет)",
                "Щит защитника (500 монет)",
                "Кольцо маны (400 монет)",
                "Зелье лечения (100 монет)",
                "Зелье маны (80 монет)",
                "Кожаный доспех (300 монет)",
                "Шлем воина (150 монет)",
                "Лук охотника (200 монет)",
                "Свиток огня (120 монет)"
            });

            buttonBuy = new Button
            {
                BackColor = Color.LightGreen,
                Font = new Font("Arial", 10F, FontStyle.Bold),
                ForeColor = Color.DarkGreen,
                Location = new Point(300, 37),
                Size = new Size(100, 30),
                Text = "КУПИТЬ"
            };
            buttonBuy.Click += ButtonBuy_Click;

            groupBoxShop.Controls.Add(labelShopTitle);
            groupBoxShop.Controls.Add(comboBoxShop);
            groupBoxShop.Controls.Add(buttonBuy);

            // Кнопка перехода на Form4
            buttonGoToForm4 = new Button
            {
                BackColor = Color.MediumPurple,
                Font = new Font("Arial", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(400, 310),
                Size = new Size(250, 35),
                Text = "ПЕРЕЙТИ В ПОДЗЕМЕЛЬЕ"
            };
            buttonGoToForm4.Click += ButtonGoToForm4_Click;

            // Добавляем контролы на форму
            this.Controls.Add(labelTitle);
            this.Controls.Add(labelInventoryTitle);
            this.Controls.Add(listBoxInventory);
            this.Controls.Add(groupBoxStats);
            this.Controls.Add(buttonAnalyze);
            this.Controls.Add(buttonSellJunk);
            this.Controls.Add(buttonGoToForm4);
            this.Controls.Add(groupBoxShop);
        }

        private void InitializeInventory()
        {
            // Изначальный инвентарь - неопознанные предметы
            inventoryItems = new InventoryItem[]
            {
                new InventoryItem("Неопознанный меч", "Оружие", 0, false),
                new InventoryItem("Таинственное кольцо", "Аксессуар", 0, false),
                new InventoryItem("Древний свиток", "Магия", 0, false),
                new InventoryItem("Старая книга", "Книга", 0, false),
                new InventoryItem("Блестящий кристалл", "Ресурс", 0, false),
                new InventoryItem("Ржавый кинжал", "Оружие", 0, false),
                new InventoryItem("Потёртая монета", "Валюта", 0, false),
                new InventoryItem("Сломанный посох", "Оружие", 0, false),
                new InventoryItem("Простой амулет", "Аксессуар", 0, false)
            };
        }

        private void UpdateDisplay()
        {
            // Очищаем и обновляем ListBox
            listBoxInventory.Items.Clear();

            foreach (var item in inventoryItems)
            {
                if (!string.IsNullOrEmpty(item.Name))
                {
                    string displayName = item.Identified ?
                        $"{item.Name} - {item.Price} монет" :
                        $"{item.Name} (неопознан)";
                    listBoxInventory.Items.Add(displayName);
                }
            }

            // Обновляем статистику
            int totalItems = inventoryItems.Count(i => !string.IsNullOrEmpty(i.Name));
            labelTotalItems.Text = $"Всего предметов: {totalItems}";

            int totalValue = inventoryItems.Where(i => i.Identified).Sum(i => i.Price);
            labelTotalValue.Text = $"Общая стоимость: {totalValue}";

            labelWallet.Text = $"Золото в кошельке: {wallet}";
        }

        private void ButtonAnalyze_Click(object sender, EventArgs e)
        {
            // Опознаем предметы и назначаем им цены
            string[] itemNames = {
                "Меч дракона",
                "Кольцо бессмертия",
                "Свиток телепортации",
                "Книга заклинаний",
                "Алмаз вечности",
                "Ржавый кинжал",
                "Древняя монета",
                "Сломанный посох",
                "Простой амулет"
            };

            string[] itemTypes = {
                "Оружие",
                "Аксессуар",
                "Магия",
                "Книга",
                "Ресурс",
                "Оружие",
                "Валюта",
                "Оружие",
                "Аксессуар"
            };

            int[] itemPrices = { 250, 1000, 750, 300, 200, 25, 10, 30, 45 };

            int identifiedCount = 0;

            // Опознаем каждый предмет
            for (int i = 0; i < inventoryItems.Length && i < itemNames.Length; i++)
            {
                if (!inventoryItems[i].Identified)
                {
                    inventoryItems[i] = new InventoryItem(
                        itemNames[i],
                        itemTypes[i],
                        itemPrices[i],
                        true
                    );
                    identifiedCount++;
                }
            }

            UpdateDisplay();

            if (identifiedCount > 0)
            {
                MessageBox.Show($"Опознано {identifiedCount} предметов!\nТеперь можно продать дешевые предметы (< 100 монет).",
                              "Опознание завершено",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Все предметы уже опознаны!",
                              "Информация",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
            }
        }

        private void ButtonSellJunk_Click(object sender, EventArgs e)
        {
            int soldCount = 0;
            int earnedMoney = 0;

            // Создаем временный массив для непроданных предметов
            var remainingItems = inventoryItems.Where(item =>
                string.IsNullOrEmpty(item.Name) ||
                !item.Identified ||
                item.Price >= 100
            ).ToList();

            // Продаем дешевые предметы (< 100 монет)
            foreach (var item in inventoryItems)
            {
                if (!string.IsNullOrEmpty(item.Name) && item.Identified && item.Price < 100)
                {
                    earnedMoney += item.Price;
                    soldCount++;
                }
            }

            // Обновляем инвентарь и кошелек
            inventoryItems = remainingItems.ToArray();
            wallet += earnedMoney;

            UpdateDisplay();

            if (soldCount > 0)
            {
                MessageBox.Show($"Продано {soldCount} дешевых предметов!\nВы заработали: {earnedMoney} монет.",
                              "Продажа завершена",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Не найдено дешевых предметов для продажи.\nПредметы должны быть опознаны и стоить меньше 100 монет.",
                              "Нечего продавать",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
            }
        }

        private void ButtonBuy_Click(object sender, EventArgs e)
        {
            if (comboBoxShop.SelectedItem == null)
            {
                MessageBox.Show("Выберите товар для покупки!",
                              "Ошибка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }

            string selectedItem = comboBoxShop.SelectedItem.ToString();

            // Парсим цену из названия товара
            int priceStart = selectedItem.IndexOf('(') + 1;
            int priceEnd = selectedItem.IndexOf('м'); // Ищем "монет"

            if (priceStart > 0 && priceEnd > priceStart)
            {
                string priceStr = selectedItem.Substring(priceStart, priceEnd - priceStart).Trim();
                if (int.TryParse(priceStr, out int price))
                {
                    if (wallet >= price)
                    {
                        // Покупаем товар
                        wallet -= price;

                        // Добавляем в инвентарь
                        string itemName = selectedItem.Substring(0, priceStart - 2).Trim();
                        string itemType = GetItemType(itemName);

                        Array.Resize(ref inventoryItems, inventoryItems.Length + 1);
                        inventoryItems[inventoryItems.Length - 1] = new InventoryItem(
                            itemName, itemType, price, true
                        );

                        UpdateDisplay();

                        MessageBox.Show($"Поздравляем с покупкой!\nВы приобрели: {itemName}",
                                      "Покупка совершена",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                    }
                    else
                    {
                        int needed = price - wallet;
                        MessageBox.Show($"Не хватает золота!\nНужно: {price} монет\nВ кошельке: {wallet} монет\nНе хватает: {needed} монет",
                                      "Недостаточно средств",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void ButtonGoToForm4_Click(object sender, EventArgs e)
        {
            try
            {
                // Создаем и показываем Form4
                Form4 form4 = new Form4();
                form4.Show(); // Показываем Form4
                this.Hide(); // Скрываем текущую форму

                MessageBox.Show("Добро пожаловать в подземелье!",
                              "Переход",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы подземелья:\n{ex.Message}",
                              "Ошибка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private string GetItemType(string itemName)
        {
            if (itemName.Contains("Меч") || itemName.Contains("Арбалет") || itemName.Contains("Лук"))
                return "Оружие";
            if (itemName.Contains("Зелье"))
                return "Зелье";
            if (itemName.Contains("Щит") || itemName.Contains("доспех") || itemName.Contains("Шлем"))
                return "Броня";
            if (itemName.Contains("Кольцо") || itemName.Contains("Амулет"))
                return "Аксессуар";
            if (itemName.Contains("Свиток"))
                return "Магия";
            return "Разное";
        }
    }
}