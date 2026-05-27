using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace lab4_new
{
    public class Builder
    {
        public SldWorks app;               // Экземпляр приложения Solidworks
        public ModelDoc2 model;            // Активный документ (модель)
        public PartDoc part;               // Документ детали
        public AssemblyDoc assembly;       // Документ сборки
        public SelectionMgr selMng;        // Менеджер выбора элементов 

        private List<object> preSelectedObjects = new List<object>(); // Хранит предварительно выделенные объекты

        public bool init()
        {
            // Открыть SolidWorks либо получить экземпляр открытого приложения
            try
            {
                // Попытка получить существующий экземпляр SolidWorks
                app = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
                Console.WriteLine("Подключен к существующему SolidWorks");
                return true;
            }
            catch
            {
                try
                {
                    // Если не нашли открытый - создаем новый
                    app = new SldWorks();
                    app.FrameState = (int)swWindowState_e.swWindowMaximized;
                    app.Visible = true;
                    Console.WriteLine("Запущен новый экземпляр SolidWorks");
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось подключиться к SolidWorks: {ex.Message}");
                    return false;
                }
            }
        }

        // Метод для открытия сборки с одной деталью
        public ModelDoc2 OpenBaseAssembly(string assemblyPath)
        {
            try
            {
                ModelDoc2 baseAssembly = app.OpenDoc6(assemblyPath,
                    (int)swDocumentTypes_e.swDocASSEMBLY,
                    (int)swOpenDocOptions_e.swOpenDocOptions_Silent,
                    "", 0, 0);

                if (baseAssembly != null)
                {
                    model = baseAssembly;
                    assembly = model as AssemblyDoc;
                    app.ActivateDoc3(baseAssembly.GetTitle(), false, 0, 0);
                }

                return baseAssembly;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия сборки: {ex.Message}");
                return null;
            }
        }

        // Метод для сохранения предварительно выделенных объектов
        public void SavePreSelectedObjects()
        {
            preSelectedObjects.Clear();

            if (app == null)
            {
                MessageBox.Show("SolidWorks не инициализирован");
                return;
            }

            // Получаем активный документ
            ModelDoc2 activeDoc = app.ActiveDoc as ModelDoc2;
            if (activeDoc == null)
            {
                MessageBox.Show("Нет активного документа в SolidWorks");
                return;
            }

            // Получаем менеджер выделения из активного документа
            SelectionMgr selMgr = activeDoc.SelectionManager;
            if (selMgr == null)
            {
                MessageBox.Show("Не удалось получить менеджер выделения");
                return;
            }

            int count = selMgr.GetSelectedObjectCount2(-1); // -1 означает все типы
            Console.WriteLine($"Найдено выделенных объектов: {count}");

            for (int i = 1; i <= count; i++) // Индексация с 1 в SolidWorks
            {
                object obj = selMgr.GetSelectedObject6(i, -1);
                if (obj != null)
                {
                    Console.WriteLine($"Объект {i}: {obj.GetType().Name}");
                    preSelectedObjects.Add(obj);
                }
            }

            MessageBox.Show($"Сохранено {preSelectedObjects.Count} объектов");
        }

        // Метод для получения списка сохраненных предварительно выделенных объектов
        public List<object> GetPreSelectedObjects()
        {
            return preSelectedObjects;
        }

        // Улучшенный метод для добавления детали с автоматическим сопряжением
        public bool AddComponentWithAutoMate(string componentPath,
                                            int expectedMates = 2, // Ожидаемое количество сопряжений
                                            double x = 0, double y = 0, double z = 0)
        {
            try
            {
                if (assembly == null || app == null)
                {
                    MessageBox.Show("Сборка не открыта или SolidWorks не инициализирован");
                    return false;
                }

                var savedElements = preSelectedObjects;

                // Проверяем количество сохраненных элементов
                if (savedElements.Count < expectedMates)
                {
                    MessageBox.Show($"Для этой детали нужно выделить {expectedMates} элемента!\n" +
                                   $"Сейчас сохранено: {savedElements.Count}");
                    return false;
                }

                // 1. Добавляем деталь в сборку
                Component2 newComponent = AddComponentToAssembly(componentPath, x, y, z);
                if (newComponent == null) return false;

                // 2. Определяем тип детали по имени файла
                string fileName = Path.GetFileNameWithoutExtension(componentPath).ToLower();

                // 3. Специальная обработка для зажима
                if (fileName.Contains("clamp") || fileName.Contains("зажим"))
                {
                    // Для зажима используем специальный метод
                    return AddClampWithAutoMate(componentPath, savedElements[0]);
                }

                // 4. Для остальных деталей - универсальная логика
                return AutoMateComponentUniversal(newComponent, savedElements, expectedMates);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return false;
            }
        }

        // Универсальный метод для добавления любого компонента (деталь или сборка)
        public Component2 AddComponentToAssembly(string componentPath, double x = 0, double y = 0, double z = 0)
        {
            try
            {
                if (assembly == null || app == null)
                {
                    MessageBox.Show("Сборка не открыта или SolidWorks не инициализирован");
                    return null;
                }

                // Определяем тип файла по расширению
                string extension = Path.GetExtension(componentPath).ToUpper();
                int docType;

                switch (extension)
                {
                    case ".SLDPRT":
                        docType = (int)swDocumentTypes_e.swDocPART;
                        break;
                    case ".SLDASM":
                        docType = (int)swDocumentTypes_e.swDocASSEMBLY;
                        break;
                    case ".SLDDRW":
                        docType = (int)swDocumentTypes_e.swDocDRAWING;
                        break;
                    default:
                        MessageBox.Show($"Неподдерживаемый формат файла: {extension}");
                        return null;
                }

                // Сохраняем активный документ (текущую сборку)
                ModelDoc2 originalDoc = model;

                // Открываем файл компонента
                ModelDoc2 componentDoc = app.OpenDoc6(
                    componentPath,
                    docType,
                    (int)swOpenDocOptions_e.swOpenDocOptions_Silent,
                    "", 0, 0);

                if (componentDoc == null)
                {
                    MessageBox.Show($"Не удалось открыть файл: {Path.GetFileName(componentPath)}");
                    return null;
                }

                // Возвращаемся к основной сборке
                app.ActivateDoc3(originalDoc.GetTitle(), false, 0, 0);

                // Добавляем компонент в сборку
                Component2 newComponent = null;

                if (docType == (int)swDocumentTypes_e.swDocPART || docType == (int)swDocumentTypes_e.swDocASSEMBLY)
                {
                    newComponent = assembly.AddComponent4(componentPath, "", x, y, z);
                }
                else
                {
                    MessageBox.Show($"Нельзя добавить в сборку файл типа: {extension}");
                }

                // Закрываем документ компонента (он теперь в сборке)
                if (componentDoc != null && componentDoc != originalDoc)
                {
                    app.CloseDoc(componentDoc.GetTitle());
                }

                // Перестраиваем сборку
                if (newComponent != null)
                {
                    model.EditRebuild3();
                }

                //MessageBox.Show($"Компонент добавлен: {Path.GetFileName(componentPath)}");
                return newComponent;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении компонента: {ex.Message}");
                return null;
            }
        }

        // Универсальный метод для любого количества сопряжений
        private bool AutoMateComponentUniversal(Component2 component,
                                               List<object> baseElements,
                                               int expectedMates)
        {
            try
            {
                ModelDoc2 activeDoc = app.IActiveDoc2 as ModelDoc2;
                if (activeDoc == null) return false;

                GeometryAnalyzer analyzer = new GeometryAnalyzer();
                bool allMated = true;

                // Для каждого сохраненного элемента находим соответствующий на компоненте
                for (int i = 0; i < Math.Min(expectedMates, baseElements.Count); i++)
                {
                    object baseElement = baseElements[i];
                    int elementType = GetElementType(baseElement);
                    object mateElement = null;

                    // Находим соответствующий элемент на добавляемой детали
                    switch (elementType)
                    {
                        case 0: // Плоскость
                            mateElement = analyzer.FindBurtPlane(component);
                            break;
                        case 1: // Цилиндр
                        case 2: // Круглое ребро
                            mateElement = analyzer.FindPinCylinder(component);
                            break;
                        default:
                            // По умолчанию ищем плоскую грань
                            mateElement = analyzer.FindBurtPlane(component);
                            break;
                    }

                    if (mateElement != null)
                    {
                        int mateType = (elementType == 1 || elementType == 2) ? 1 : 0;
                        CreateMate(baseElement, mateElement, mateType);
                    }
                    else
                    {
                        allMated = false;
                        Console.WriteLine($"Не удалось найти элемент для сопряжения {i + 1}");
                    }
                }

                activeDoc.EditRebuild3();
                return allMated;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return false;
            }
        }

        // Специальный метод для зажима-сборки
        public bool AddClampWithAutoMate(string clampPath, object baseFace)
        {
            try
            {
                if (assembly == null || app == null)
                {
                    MessageBox.Show("Сборка не открыта");
                    return false;
                }

                // 1. Добавляем зажим-сборку
                Component2 clampComponent = AddComponentToAssembly(clampPath, 0.05, 0, 0);
                if (clampComponent == null) return false;

                ModelDoc2 activeDoc = app.IActiveDoc2 as ModelDoc2;
                GeometryAnalyzer analyzer = new GeometryAnalyzer();

                // 2. Ищем SettingPlane в сборке зажима
                object clampPlane = analyzer.FindSettingPlaneUniversal(clampComponent);

                if (clampPlane == null)
                {
                    MessageBox.Show("Не найден SettingPlane в сборке зажима!\n" +
                                   "Создайте плоскость и назовите её 'SettingPlane'");
                    return false;
                }

                // 3. Устанавливаем сопряжение
                activeDoc.ClearSelection2(true);

                // Выделяем грань базовой детали
                if (baseFace is IEntity baseEntity)
                    baseEntity.Select4(true, null);

                // Выделяем SettingPlane зажима
                if (clampPlane is IEntity clampEntity)
                    clampEntity.Select4(true, null);
                else if (clampPlane is Feature clampFeature)
                    clampFeature.Select2(true, -1);

                // Для зажима обычно используется сопряжение "совпадение"
                assembly.AddMate(0, 0, false, 0, 0);

                activeDoc.EditRebuild3();

                //MessageBox.Show("Зажим-сборка добавлена и сопряжена!");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return false;
            }
        }

        // Улучшенный метод для определения типа элемента
        private int GetElementType(object element)
        {
            // Возвращает:
            // 0 - плоскость
            // 1 - цилиндр
            // 2 - круглое ребро
            // -1 - неизвестно

            if (element is Face2 face)
            {
                try
                {
                    Surface surf = face.GetSurface();
                    if (surf != null)
                    {
                        if (surf.IsCylinder()) return 1;
                        if (surf.IsPlane()) return 0;
                    }
                }
                catch { }
            }
            else if (element is Edge edge)
            {
                // Проверяем, является ли ребро круговым
                try
                {
                    Curve curve = edge.GetCurve();
                    if (curve != null)
                    {
                        // Проверяем, является ли кривая окружностью
                        if (curve.IsCircle())
                        {
                            return 2; // Круглое ребро
                        }
                    }
                }
                catch { }
            }
            else if (element is Feature feature)
            {
                // Для свойств конструктивной геометрии
                // Проверяем по имени или типу
                if (feature.GetTypeName2().Contains("RefPlane"))
                    return 0; // Плоскость
            }

            return -1;
        }

        // Метод для создания одного сопряжения
        private void CreateMate(object element1, object element2, int mateType)
        {
            try
            {
                ModelDoc2 activeDoc = app.IActiveDoc2 as ModelDoc2;
                activeDoc.ClearSelection2(true);

                // Выделяем первый элемент
                if (element1 is IEntity entity1) entity1.Select4(true, null);
                else if (element1 is Feature feature1) feature1.Select2(true, -1);

                // Выделяем второй элемент
                if (element2 is IEntity entity2) entity2.Select4(true, null);
                else if (element2 is Feature feature2) feature2.Select2(true, -1);

                // mateType: 0 - совпадение, 1 - соосность
                int alignType = (mateType == 0) ? 0 : -1;
                assembly.AddMate(mateType, alignType, false, 0, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка создания сопряжения: {ex.Message}");
            }
        }


    }
}
