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

        private void buttonSaveSelection1_Click(object sender, EventArgs e)
        {
            builder.SaveFirstStageFaces();
        }

        private void buttonSaveSelection2_Click(object sender, EventArgs e)
        {
            builder.SaveSecondStageFaces();
        }

        // Кнопка для Горизонтального пальца
        private void btnAddHorizontalPin_Click(object sender, EventArgs e)
        {
            if (!File.Exists(pinHorizontalPath))
            {
                MessageBox.Show($"Файл не найден: {pinHorizontalPath}");
                return;
            }

            bool success = builder.AddComponentWithAutoMate(pinHorizontalPath, "PalecHorizontal");
            if (success)
            {
                MessageBox.Show("PalecHorizontal добавлен и сопряжен!");
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

            bool success = builder.AddComponentWithAutoMate(pinVerticalPath, "PalecVertical");
            if (success)
            {
                MessageBox.Show("PalecVertical добавлен и сопряжен!");
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

            bool success = builder.AddComponentWithAutoMate(clampPath, "Clamp");
            if (success)
            {
                MessageBox.Show("Clamp добавлен и сопряжен!");
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

            bool success = builder.AddComponentWithAutoMate(basePath, "Base");
            if (success)
            {
                MessageBox.Show("Base добавлена и сопряжена!");
            }
        }
    }
}