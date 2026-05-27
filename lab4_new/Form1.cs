using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4_new
{
    public partial class Form1 : Form
    {
        private Builder builder = new Builder();
        private string baseAssemblyPath = "D:\\BNTU\\3 курс\\ОАК\\лабы\\lab4_new\\units\\Сборка.SLDASM";
        private string componentsFolder = "D:\\BNTU\\3 курс\\ОАК\\лабы\\lab4_new\\units\\";

        // Конкретные пути к деталям
        private string pinHorizontalPath;   // Горизонтальный палец
        private string pinVerticalPath;     // Вертикальный палец  
        private string basePath;            // Основание
        private string clampPath;           // Зажим

        public Form1()
        {
            InitializeComponent();
            InitializePaths();
        }

        private void InitializePaths()
        {
            pinHorizontalPath = Path.Combine(componentsFolder, "PalecHorizontal.SLDPRT");
            pinVerticalPath = Path.Combine(componentsFolder, "PalecVertical.SLDPRT");
            basePath = Path.Combine(componentsFolder, "base.SLDPRT");
            clampPath = Path.Combine(componentsFolder, "Clamp.SLDASM");
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            // 1. Инициализация SolidWorks
            if (!builder.init())
                return;

            // 2. Открытие базовой сборки с одной деталью
            builder.OpenBaseAssembly(baseAssemblyPath);
        }

        private void buttonSaveSelection_Click(object sender, EventArgs e)
        {
            builder.SavePreSelectedObjects();
        }

        // Кнопка для Горизонтального пальца
        private void btnAddHorizontalPin_Click(object sender, EventArgs e)
        {
            if (!File.Exists(pinHorizontalPath))
            {
                MessageBox.Show($"Файл не найден: {pinHorizontalPath}");
                return;
            }

            bool success = builder.AddComponentWithAutoMate(pinHorizontalPath, 2, 0.1, 0, 0);
            if (success)
            {
                MessageBox.Show("Горизонтальный палец добавлен и сопряжен!");
            }
        }

        // Кнопка для Вертикального пальца
        private void btnAddVerticalPin_Click(object sender, EventArgs e)
        {
            if (!File.Exists(pinVerticalPath))
            {
                MessageBox.Show($"Файл не найден: {pinVerticalPath}");
                return;
            }

            bool success = builder.AddComponentWithAutoMate(pinVerticalPath, 2, 0.2, 0, 0);
            if (success)
            {
                MessageBox.Show("Вертикальный палец добавлен и сопряжен!");
            }
        }

        // Кнопка для Основания
        private void btnAddBase_Click(object sender, EventArgs e)
        {
            if (!File.Exists(basePath))
            {
                MessageBox.Show($"Файл не найден: {basePath}");
                return;
            }

            // Для основания нужно только плоское сопряжение
            var savedElements = builder.GetPreSelectedObjects();
            if (savedElements.Count >= 1) // Нужна только плоскость
            {
                bool success = builder.AddComponentWithAutoMate(basePath, 2, 0.01, 0, 0.02);
                if (success)
                {
                    MessageBox.Show("Основание добавлена и сопряжена!");
                }
            }
            else
            {
                MessageBox.Show("Сначала выделите плоскость на базовой детали!");
            }
        }

        // Кнопка для Зажима
        private void btnAddClamp_Click(object sender, EventArgs e)
        {
            if (!File.Exists(clampPath))
            {
                MessageBox.Show($"Файл не найден: {clampPath}");
                return;
            }

            // Для зажима нужно только 1 выделенный элемент
            var savedElements = builder.GetPreSelectedObjects();

            if (savedElements.Count == 0)
            {
                MessageBox.Show("Сначала выделите на базовой детали:\n" +
                               "Грань, к которой будет прижиматься зажим\n" +
                               "Затем нажмите 'Сохранить выделение'");
                return;
            }

            // Для зажима указываем expectedMates = 1
            bool success = builder.AddClampWithAutoMate(clampPath, savedElements[0]);

            if (success)
            {
                MessageBox.Show("Зажим добавлен и сопряжен!");
            }
        }
    }
}
