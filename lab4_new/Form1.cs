using System;
using System.IO;
using System.Windows.Forms;

namespace lab4_new
{
    public partial class Form1 : Form
    {
        private Builder builder = new Builder();

        private string baseAssemblyPath = "D:\\BNTU\\3 курс\\ОАК\\лабы\\lab4\\units\\Сборка.SLDASM";
        private string componentsFolder = "D:\\BNTU\\3 курс\\ОАК\\лабы\\lab4\\units\\";

        private string pinHorizontalPath;
        private string pinVerticalPath;
        private string basePath;
        private string clampPath;

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
            if (!builder.init())
                return;

            builder.OpenBaseAssembly(baseAssemblyPath);
        }

        private void buttonSaveSelection_Click(object sender, EventArgs e)
        {
            builder.SavePreSelectedObjects();
        }

        // Горизонтальный палец
        private void btnAddHorizontalPin_Click(object sender, EventArgs e)
        {
            if (!File.Exists(pinHorizontalPath))
            {
                MessageBox.Show($"Файл не найден: {pinHorizontalPath}");
                return;
            }

            bool success = builder.AddComponentWithAutoMate(pinHorizontalPath, 2, 0.1, 0, 0);
            if (success)
                MessageBox.Show("Горизонтальный палец добавлен и сопряжён!");
        }

        // Вертикальный палец
        private void btnAddVerticalPin_Click(object sender, EventArgs e)
        {
            if (!File.Exists(pinVerticalPath))
            {
                MessageBox.Show($"Файл не найден: {pinVerticalPath}");
                return;
            }

            bool success = builder.AddComponentWithAutoMate(pinVerticalPath, 2, 0.2, 0, 0);
            if (success)
                MessageBox.Show("Вертикальный палец добавлен и сопряжён!");
        }

        // Основание
        private void btnAddBase_Click(object sender, EventArgs e)
        {
            if (!File.Exists(basePath))
            {
                MessageBox.Show($"Файл не найден: {basePath}");
                return;
            }

            if (builder.basePlanesForBase.Count != 3)
            {
                MessageBox.Show("Сначала выделите 3 горизонтальные плоскости на добавленных деталях и нажмите 'Сохранить выделение'");
                return;
            }

            bool success = builder.AddComponentWithAutoMate(basePath, 3, 0.01, 0, 0.02);
            if (success)
                MessageBox.Show("Основание добавлено и сопряжено!");
        }

        // Зажим
        private void btnAddClamp_Click(object sender, EventArgs e)
        {
            if (!File.Exists(clampPath))
            {
                MessageBox.Show($"Файл не найден: {clampPath}");
                return;
            }

            if (builder.bases.ClampTopPlane == null)
            {
                MessageBox.Show("Не найдена верхняя плоскость для зажима.\n" +
                                "Убедитесь, что вы выделили 5 базовых элементов и сохранили их.");
                return;
            }

            bool success = builder.AddComponentWithAutoMate(clampPath, 1, 0.05, 0, 0);
            if (success)
                MessageBox.Show("Зажим добавлен и сопряжён!");
        }
    }
}
