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
        public AssemblyDoc assembly;

        private List<object> preSelectedObjects = new List<object>();

        // Хранилище классифицированных баз
        public class BaseSelection
        {
            public object HorizontalPlane;
            public object VerticalPlane;
            public object HorizontalCylinder;
            public object VerticalCircleEdge;
            public object ClampTopPlane;
        }

        public BaseSelection bases = new BaseSelection();

        // Второй набор выделений — для основания
        public List<object> basePlanesForBase = new List<object>();

        public bool init()
        {
            try
            {
                app = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
                return true;
            }
            catch
            {
                try
                {
                    app = new SldWorks();
                    app.Visible = true;
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
                ModelDoc2 baseAssembly = app.OpenDoc6(
                    assemblyPath,
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

        // Сохранение выделений
        public void SavePreSelectedObjects()
        {
            preSelectedObjects.Clear();

            ModelDoc2 activeDoc = app.ActiveDoc as ModelDoc2;
            if (activeDoc == null) return;

            SelectionMgr selMgr = activeDoc.SelectionManager;
            if (selMgr == null) return;

            int count = selMgr.GetSelectedObjectCount2(-1);

            for (int i = 1; i <= count; i++)
            {
                object obj = selMgr.GetSelectedObject6(i, -1);
                if (obj != null)
                    preSelectedObjects.Add(obj);
            }

            MessageBox.Show($"Сохранено {preSelectedObjects.Count} объектов");

            // Если это первое сохранение (5 элементов)
            if (preSelectedObjects.Count == 5)
            {
                ClassifyBaseSelections();
                MessageBox.Show("Базы успешно классифицированы!");
            }

            // Если это второе сохранение (3 плоскости для основания)
            if (preSelectedObjects.Count == 3)
            {
                basePlanesForBase = new List<object>(preSelectedObjects);
                MessageBox.Show("Плоскости для основания сохранены!");
            }
        }

        // Классификация 5 выделений
        private void ClassifyBaseSelections()
        {
            GeometryAnalyzer analyzer = new GeometryAnalyzer();

            foreach (var obj in preSelectedObjects)
            {
                if (obj is Face2 face)
                {
                    Surface surf = face.GetSurface();

                    if (surf.IsPlane())
                    {
                        double[] n = face.Normal;

                        // Горизонтальная плоскость (нормаль по Z)
                        if (Math.Abs(n[2]) > 0.9)
                        {
                            if (bases.HorizontalPlane == null)
                                bases.HorizontalPlane = face;

                            if (bases.ClampTopPlane == null)
                                bases.ClampTopPlane = face;
                        }

                        // Вертикальная плоскость
                        if (Math.Abs(n[2]) < 0.1)
                        {
                            if (bases.VerticalPlane == null)
                                bases.VerticalPlane = face;
                        }
                    }

                    if (surf.IsCylinder())
                    {
                        double[] cyl = surf.CylinderParams;
                        double nx = cyl[3], ny = cyl[4], nz = cyl[5];

                        // Горизонтальный цилиндр
                        if (Math.Abs(nz) < 0.1)
                        {
                            if (bases.HorizontalCylinder == null)
                                bases.HorizontalCylinder = face;
                        }
                    }
                }

                if (obj is Edge edge)
                {
                    Curve curve = edge.GetCurve();
                    if (curve.IsCircle())
                    {
                        double[] p = curve.CircleParams;

                        // Ось || Y
                        if (Math.Abs(p[4]) > 0.9)
                        {
                            if (bases.VerticalCircleEdge == null)
                                bases.VerticalCircleEdge = edge;
                        }
                    }
                }
            }
        }

        public List<object> GetBasesForHorizontalPin()
        {
            return new List<object>()
            {
                bases.VerticalPlane,
                bases.HorizontalCylinder
            };
        }

        public List<object> GetBasesForVerticalPin()
        {
            return new List<object>()
            {
                bases.HorizontalPlane,
                bases.VerticalCircleEdge
            };
        }

        public List<object> GetBasesForClamp()
        {
            return new List<object>()
            {
                bases.ClampTopPlane
            };
        }

        public List<object> GetBasesForBase()
        {
            return basePlanesForBase;
        }

        // Добавление компонента
        public bool AddComponentWithAutoMate(string componentPath,
                                             int expectedMates,
                                             double x, double y, double z)
        {
            string file = Path.GetFileName(componentPath).ToLower();

            List<object> savedElements = null;

            if (file.Contains("horizontal"))
                savedElements = GetBasesForHorizontalPin();

            else if (file.Contains("vertical"))
                savedElements = GetBasesForVerticalPin();

            else if (file.Contains("clamp"))
                savedElements = GetBasesForClamp();

            else if (file.Contains("base"))
                savedElements = GetBasesForBase();

            if (savedElements == null)
            {
                MessageBox.Show("Не удалось определить базы для детали");
                return false;
            }

            Component2 newComponent = AddComponentToAssembly(componentPath, x, y, z);
            if (newComponent == null) return false;

            return AutoMateComponentUniversal(newComponent, savedElements, expectedMates);
        }

        public Component2 AddComponentToAssembly(string componentPath, double x, double y, double z)
        {
            try
            {
                string ext = Path.GetExtension(componentPath).ToUpper();
                int docType = ext == ".SLDASM" ? 2 : 1;

                ModelDoc2 original = model;

                ModelDoc2 compDoc = app.OpenDoc6(componentPath, docType,
                    (int)swOpenDocOptions_e.swOpenDocOptions_Silent,
                    "", 0, 0);

                app.ActivateDoc3(original.GetTitle(), false, 0, 0);

                Component2 comp = assembly.AddComponent4(componentPath, "", x, y, z);

                if (compDoc != null && compDoc != original)
                    app.CloseDoc(compDoc.GetTitle());

                model.EditRebuild3();
                return comp;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления компонента: {ex.Message}");
                return null;
            }
        }

        private bool AutoMateComponentUniversal(Component2 component,
                                                List<object> baseElements,
                                                int expectedMates)
        {
            GeometryAnalyzer analyzer = new GeometryAnalyzer();
            ModelDoc2 doc = app.IActiveDoc2;

            for (int i = 0; i < expectedMates; i++)
            {
                object baseElem = baseElements[i];
                object mateElem = null;

                int type = GetElementType(baseElem);

                if (type == 0)
                    mateElem = analyzer.FindBurtPlane(component);

                if (type == 1 || type == 2)
                    mateElem = analyzer.FindPinCylinder(component);

                if (mateElem != null)
                    CreateMate(baseElem, mateElem, type == 0 ? 0 : 1);
            }

            doc.EditRebuild3();
            return true;
        }

        private int GetElementType(object element)
        {
            if (element is Face2 face)
            {
                Surface s = face.GetSurface();
                if (s.IsPlane()) return 0;
                if (s.IsCylinder()) return 1;
            }

            if (element is Edge edge)
            {
                Curve c = edge.GetCurve();
                if (c.IsCircle()) return 2;
            }

            return -1;
        }

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
