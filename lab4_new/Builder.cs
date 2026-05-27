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
        public SldWorks app;
        public ModelDoc2 model;
        public PartDoc part;
        public AssemblyDoc assembly;
        public SelectionMgr selMng;

        private List<object> firstStageFaces = new List<object>();
        private List<object> secondStageFaces = new List<object>();
        private Dictionary<string, List<object>> distributedFaces = new Dictionary<string, List<object>>();

        public bool init()
        {
            try
            {
                app = (SldWorks)Marshal.GetActiveObject("SolidWorks.Application");
                Console.WriteLine("Подключен к существующему SolidWorks");
                return true;
            }
            catch
            {
                try
                {
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

        public void SaveFirstStageFaces()
        {
            firstStageFaces.Clear();
            distributedFaces.Clear();

            if (app == null)
            {
                MessageBox.Show("SolidWorks не инициализирован");
                return;
            }

            ModelDoc2 activeDoc = app.ActiveDoc as ModelDoc2;
            if (activeDoc == null)
            {
                MessageBox.Show("Нет активного документа в SolidWorks");
                return;
            }

            SelectionMgr selMgr = activeDoc.SelectionManager;
            if (selMgr == null)
            {
                MessageBox.Show("Не удалось получить менеджер выделения");
                return;
            }

            int count = selMgr.GetSelectedObjectCount2(-1);
            Console.WriteLine($"\n=== ЭТАП 1: Выделение элементов ===");
            Console.WriteLine($"Найдено выделенных объектов: {count}");

            if (count != 5)
            {
                MessageBox.Show($"Ошибка: необходимо выделить ровно 5 элементов!\nВыделено: {count}");
                return;
            }

            for (int i = 1; i <= count; i++)
            {
                int selType = selMgr.GetSelectedObjectType3(i, -1);
                string typeName = GetSelectionTypeName(selType);

                Console.WriteLine($"  Элемент {i}: selType={selType} ({typeName})");

                object obj = null;

                // Получаем объект в зависимости от типа выделения
                if (selType == (int)swSelectType_e.swSelFACES)
                {
                    obj = selMgr.GetSelectedObject2(i, -1) as Face2;
                }
                else if (selType == (int)swSelectType_e.swSelEDGES)
                {
                    obj = selMgr.GetSelectedObject2(i, -1) as Edge;
                }
                else
                {
                    obj = selMgr.GetSelectedObject2(i, -1);
                }

                if (obj != null)
                {
                    firstStageFaces.Add(obj);
                    Console.WriteLine($"    Объект успешно получен: {obj.GetType().Name}");
                }
                else
                {
                    Console.WriteLine($"    ОШИБКА: Не удалось получить объект");
                }
            }

            if (DistributeFaces(firstStageFaces))
            {
                MessageBox.Show($"✓ Сохранено и распределено {firstStageFaces.Count} элементов");
            }
            else
            {
                MessageBox.Show("✗ Ошибка при распределении элементов!");
                firstStageFaces.Clear();
                distributedFaces.Clear();
            }
        }

        public void SaveSecondStageFaces()
        {
            secondStageFaces.Clear();

            if (app == null)
            {
                MessageBox.Show("SolidWorks не инициализирован");
                return;
            }

            ModelDoc2 activeDoc = app.ActiveDoc as ModelDoc2;
            if (activeDoc == null)
            {
                MessageBox.Show("Нет активного документа в SolidWorks");
                return;
            }

            SelectionMgr selMgr = activeDoc.SelectionManager;
            if (selMgr == null)
            {
                MessageBox.Show("Не удалось получить менеджер выделения");
                return;
            }

            int count = selMgr.GetSelectedObjectCount2(-1);
            Console.WriteLine($"\n=== ЭТАП 2: Выделение граней ===");
            Console.WriteLine($"Найдено выделенных объектов: {count}");

            if (count != 3)
            {
                MessageBox.Show($"Ошибка: необходимо выделить ровно 3 грани!\nВыделено: {count}");
                return;
            }

            for (int i = 1; i <= count; i++)
            {
                object obj = selMgr.GetSelectedObject2(i, -1) as Face2;
                if (obj != null)
                {
                    Console.WriteLine($"  Грань {i}: {obj.GetType().Name}");
                    secondStageFaces.Add(obj);
                }
            }

            MessageBox.Show($"✓ Сохранено {secondStageFaces.Count} граней второго этапа");
        }

        private bool DistributeFaces(List<object> faces)
        {
            try
            {
                Console.WriteLine($"\n=== Анализ выделенных элементов ===");

                distributedFaces["PalecHorizontal"] = new List<object>();
                distributedFaces["PalecVertical"] = new List<object>();
                distributedFaces["Clamp"] = new List<object>();

                for (int i = 0; i < faces.Count; i++)
                {
                    object elem = faces[i];
                    ElementType elemType = ElementType.Unknown;
                    string debugInfo = "";

                    try
                    {
                        elemType = AnalyzeElementType(elem);
                        debugInfo = $"Тип: {elemType}";
                    }
                    catch (Exception ex)
                    {
                        debugInfo = $"Ошибка анализа: {ex.Message}";
                    }

                    Console.WriteLine($"Элемент {i + 1}: {debugInfo}");

                    // PalecHorizontal: вертикальная плоскость + вертикальное ребро
                    if (elemType == ElementType.VerticalPlane && distributedFaces["PalecHorizontal"].Count < 1)
                    {
                        distributedFaces["PalecHorizontal"].Add(elem);
                        Console.WriteLine("  ✓ → PalecHorizontal (вертикальная плоскость)");
                    }
                    else if (elemType == ElementType.VerticalCylinderEdge && distributedFaces["PalecHorizontal"].Count == 1)
                    {
                        distributedFaces["PalecHorizontal"].Add(elem);
                        Console.WriteLine("  ✓ → PalecHorizontal (вертикальное ребро)");
                    }

                    // PalecVertical: горизонтальная плоскость + горизонтальное ребро
                    else if (elemType == ElementType.HorizontalPlane && distributedFaces["PalecVertical"].Count < 1)
                    {
                        distributedFaces["PalecVertical"].Add(elem);
                        Console.WriteLine("  ✓ → PalecVertical (горизонтальная плоскость)");
                    }
                    else if (elemType == ElementType.HorizontalCylinderEdge && distributedFaces["PalecVertical"].Count == 1)
                    {
                        distributedFaces["PalecVertical"].Add(elem);
                        Console.WriteLine("  ✓ → PalecVertical (горизонтальное ребро)");
                    }

                    // Clamp: горизонтальная плоскость
                    else if (elemType == ElementType.HorizontalPlane && distributedFaces["Clamp"].Count < 1 && distributedFaces["PalecVertical"].Count == 2)
                    {
                        distributedFaces["Clamp"].Add(elem);
                        Console.WriteLine("  ✓ → Clamp (горизонтальная плоскость)");
                    }
                    else
                    {
                        Console.WriteLine($"  ✗ → Не подошло (slots: PH={distributedFaces["PalecHorizontal"].Count}/2, PV={distributedFaces["PalecVertical"].Count}/2, C={distributedFaces["Clamp"].Count}/1)");
                    }
                }

                Console.WriteLine($"\n=== Результат распределения ===");
                Console.WriteLine($"PalecHorizontal: {distributedFaces["PalecHorizontal"].Count}/2");
                Console.WriteLine($"PalecVertical: {distributedFaces["PalecVertical"].Count}/2");
                Console.WriteLine($"Clamp: {distributedFaces["Clamp"].Count}/1");

                bool success = distributedFaces["PalecHorizontal"].Count == 2 &&
                              distributedFaces["PalecVertical"].Count == 2 &&
                              distributedFaces["Clamp"].Count == 1;

                if (success)
                {
                    Console.WriteLine("\n✓✓✓ Элементы успешно распределены ✓✓✓");
                }
                else
                {
                    Console.WriteLine("\n✗✗✗ Ошибка: не все слоты заполнены ✗✗✗");
                }

                return success;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ КРИТИЧЕСКАЯ ОШИБКА: {ex.Message}\n{ex.StackTrace}");
                MessageBox.Show($"Критическая ошибка:\n{ex.Message}");
                return false;
            }
        }

        private ElementType AnalyzeElementType(object elem)
        {
            try
            {
                // Пробуем привести к Face2 (грань)
                Face2 face = elem as Face2;
                if (face != null)
                {
                    Surface surf = face.GetSurface();
                    if (surf == null)
                    {
                        Console.WriteLine("    DEBUG: Surface is null для Face2");
                        return ElementType.Unknown;
                    }

                    // Проверяем плоскость
                    if (surf.IsPlane())
                    {
                        object[] planeParams = surf.PlaneParams as object[];
                        if (planeParams != null && planeParams.Length >= 3)
                        {
                            double normalY = Math.Abs((double)planeParams[1]);
                            Console.WriteLine($"    [Face2 Plane] Normal Y: {normalY:F3}");

                            if (normalY > 0.9)
                                return ElementType.HorizontalPlane;
                            else
                                return ElementType.VerticalPlane;
                        }
                    }

                    // Проверяем цилиндр
                    if (surf.IsCylinder())
                    {
                        object[] cylParams = surf.CylinderParams as object[];
                        if (cylParams != null && cylParams.Length >= 7)
                        {
                            double axisY = Math.Abs((double)cylParams[4]);
                            Console.WriteLine($"    [Face2 Cylinder] Axis Y: {axisY:F3}");

                            if (axisY > 0.9)
                                return ElementType.VerticalCylinderEdge;
                            else
                                return ElementType.HorizontalCylinderEdge;
                        }
                    }
                }

                // Пробуем привести к Edge (ребро)
                Edge edge = elem as Edge;
                if (edge != null)
                {
                    Curve curve = edge.GetCurve();
                    if (curve != null && curve.IsCircle())
                    {
                        object[] circleParams = curve.CircleParams as object[];
                        if (circleParams != null && circleParams.Length >= 6)
                        {
                            double axisY = Math.Abs((double)circleParams[4]);
                            Console.WriteLine($"    [Edge Circle] Axis Y: {axisY:F3}");

                            if (axisY > 0.9)
                                return ElementType.VerticalCylinderEdge;
                            else
                                return ElementType.HorizontalCylinderEdge;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"    [Edge] Не является окружностью");
                    }
                }

                Console.WriteLine($"    [Unknown] Элемент типа {elem.GetType().Name} не распознан");
                return ElementType.Unknown;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    [Exception] {ex.Message}");
                return ElementType.Unknown;
            }
        }

        private string GetSelectionTypeName(int selType)
        {
            switch (selType)
            {
                case (int)swSelectType_e.swSelFACES: return "FACE";
                case (int)swSelectType_e.swSelEDGES: return "EDGE";
                case (int)swSelectType_e.swSelVERTICES: return "VERTEX";
                case (int)swSelectType_e.swSelCOMPONENTS: return "COMPONENT";
                case (int)swSelectType_e.swSelBODYFEATURES: return "BODYFEATURE";
                default: return $"UNKNOWN({selType})";
            }
        }

        public bool AddComponentWithAutoMate(string componentPath, string componentType,
                                           double x = 0, double y = 0, double z = 0)
        {
            try
            {
                if (assembly == null || app == null)
                {
                    MessageBox.Show("Сборка не открыта или SolidWorks не инициализирован");
                    return false;
                }

                if (distributedFaces.Count == 0)
                {
                    MessageBox.Show("Сначала выполните Этап 1: сохраните и распределите 5 элементов!");
                    return false;
                }

                if (!distributedFaces.ContainsKey(componentType))
                {
                    MessageBox.Show($"Неизвестный тип компонента: {componentType}");
                    return false;
                }

                var basesForComponent = distributedFaces[componentType];
                if (basesForComponent.Count == 0)
                {
                    MessageBox.Show($"Для компонента '{componentType}' не найдены подходящие элементы!");
                    return false;
                }

                Component2 newComponent = AddComponentToAssembly(componentPath, x, y, z);
                if (newComponent == null) return false;

                return AutoMateComponentWithElements(newComponent, basesForComponent);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return false;
            }
        }

        private bool AutoMateComponentWithElements(Component2 component, List<object> baseElements)
        {
            try
            {
                ModelDoc2 activeDoc = app.IActiveDoc2 as ModelDoc2;
                if (activeDoc == null) return false;

                GeometryAnalyzer analyzer = new GeometryAnalyzer();
                bool allMated = true;

                for (int i = 0; i < baseElements.Count; i++)
                {
                    object baseElement = baseElements[i];
                    ElementType elementType = AnalyzeElementType(baseElement);
                    object mateElement = null;

                    Console.WriteLine($"\nСопряжение {i + 1}: {elementType}");

                    switch (elementType)
                    {
                        case ElementType.VerticalPlane:
                            mateElement = analyzer.FindVerticalPlane(component);
                            break;
                        case ElementType.HorizontalPlane:
                            mateElement = analyzer.FindHorizontalPlane(component);
                            break;
                        case ElementType.VerticalCylinderEdge:
                            mateElement = analyzer.FindVerticalCylinderFace(component);
                            if (mateElement == null)
                                mateElement = analyzer.FindVerticalCylinderEdge(component);
                            break;
                        case ElementType.HorizontalCylinderEdge:
                            mateElement = analyzer.FindHorizontalCylinderFace(component);
                            if (mateElement == null)
                                mateElement = analyzer.FindHorizontalCylinderEdge(component);
                            break;
                        default:
                            mateElement = analyzer.FindBurtPlane(component);
                            break;
                    }

                    if (mateElement != null)
                    {
                        int mateType = (elementType == ElementType.VerticalCylinderEdge ||
                                       elementType == ElementType.HorizontalCylinderEdge) ? 1 : 0;
                        CreateMate(baseElement, mateElement, mateType);
                        Console.WriteLine($"  ✓ Сопряжение создано");
                    }
                    else
                    {
                        allMated = false;
                        Console.WriteLine($"  ✗ Не удалось найти элемент");
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

        public Component2 AddComponentToAssembly(string componentPath, double x = 0, double y = 0, double z = 0)
        {
            try
            {
                if (assembly == null || app == null)
                {
                    MessageBox.Show("Сборка не открыта или SolidWorks не инициализирован");
                    return null;
                }

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
                    default:
                        MessageBox.Show($"Неподдерживаемый формат: {extension}");
                        return null;
                }

                ModelDoc2 originalDoc = model;
                ModelDoc2 componentDoc = app.OpenDoc6(componentPath, docType,
                    (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", 0, 0);

                if (componentDoc == null)
                {
                    MessageBox.Show($"Не удалось открыть: {Path.GetFileName(componentPath)}");
                    return null;
                }

                app.ActivateDoc3(originalDoc.GetTitle(), false, 0, 0);

                Component2 newComponent = assembly.AddComponent4(componentPath, "", x, y, z);

                if (componentDoc != null && componentDoc != originalDoc)
                {
                    app.CloseDoc(componentDoc.GetTitle());
                }

                if (newComponent != null)
                {
                    model.EditRebuild3();
                }

                return newComponent;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении компонента: {ex.Message}");
                return null;
            }
        }

        private void CreateMate(object element1, object element2, int mateType)
        {
            try
            {
                ModelDoc2 activeDoc = app.IActiveDoc2 as ModelDoc2;
                activeDoc.ClearSelection2(true);

                if (element1 is IEntity entity1)
                {
                    entity1.Select4(true, null);
                }

                if (element2 is IEntity entity2)
                {
                    entity2.Select4(true, null);
                }

                int alignType = (mateType == 0) ? 0 : -1;
                assembly.AddMate(mateType, alignType, false, 0, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сопряжения: {ex.Message}");
            }
        }
    }

    public enum ElementType
    {
        Unknown,
        VerticalPlane,
        HorizontalPlane,
        VerticalCylinderEdge,
        HorizontalCylinderEdge
    }
}