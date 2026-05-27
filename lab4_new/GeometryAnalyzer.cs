using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;

namespace lab4_new
{
    public class GeometryAnalyzer
    {
        public GeometryAnalyzer() { }

        // Простой метод для поиска плоскости (Burt)
        public object FindBurtPlane(Component2 component)
        {
            try
            {
                // 1. Ищем свойство SettingPlane
                Feature settingPlane = FindFeatureByName(component, "SettingPlane") as Feature;
                if (settingPlane != null)
                    return settingPlane;

                // 2. Если не нашли, берем первую плоскую грань
                var planarFaces = FindPlanarFaces(component);
                return planarFaces.Count > 0 ? planarFaces[0] : null;
            }
            catch
            {
                return null;
            }
        }

        public object FindPinCylinder(Component2 component)
        {
            try
            {
                var cylindricalFaces = FindCylindricalFaces(component);
                if (cylindricalFaces.Count == 0) return null;

                // Если найден только один цилиндр - возвращаем его
                if (cylindricalFaces.Count == 1) return cylindricalFaces[0];

                // Если несколько цилиндров - выбираем цилиндр с МЕНЬШИМ радиусом
                return SelectCylinderWithMinRadius(cylindricalFaces);
            }
            catch
            {
                return null;
            }
        }

        private object SelectCylinderWithMinRadius(List<object> cylindricalFaces)
        {
            Face2 bestCylinder = null;
            double minRadius = double.MaxValue; // Инициализируем очень большим числом

            foreach (object faceObj in cylindricalFaces)
            {
                Face2 face = faceObj as Face2;
                if (face == null) continue;

                try
                {
                    Surface surf = face.GetSurface();
                    if (surf != null && surf.IsCylinder())
                    {
                        // Получаем параметры цилиндра
                        object cylParams = surf.CylinderParams;
                        if (cylParams is double[] paramsArray && paramsArray.Length >= 7)
                        {
                            double radius = Math.Abs(paramsArray[6]); // Радиус

                            // Выбираем цилиндр с НАИМЕНЬШИМ радиусом
                            if (radius < minRadius)
                            {
                                minRadius = radius;
                                bestCylinder = face;
                            }
                        }
                    }
                }
                catch { }
            }

            return bestCylinder ?? cylindricalFaces[0];
        }

        // Метод для поиска цилиндрических граней в компоненте
        public List<object> FindCylindricalFaces(Component2 component)
        {
            List<object> cylindricalFaces = new List<object>();

            try
            {
                // Получаем тело компонента
                Body2 body = component.GetBody();
                if (body == null) return cylindricalFaces;

                // Получаем все грани тела
                object[] faces = body.GetFaces() as object[];
                if (faces == null) return cylindricalFaces;

                foreach (object faceObj in faces)
                {
                    Face2 face = faceObj as Face2;
                    if (face == null) continue;

                    // Получаем поверхность грани
                    Surface surf = face.GetSurface();
                    if (surf == null) continue;

                    // Проверяем, является ли поверхность цилиндрической
                    bool isCylinder = surf.IsCylinder();
                    if (isCylinder)
                    {
                        cylindricalFaces.Add(face);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при поиске цилиндрических граней: {ex.Message}");
            }

            return cylindricalFaces;
        }

        // Метод для поиска плоских граней в компоненте
        public List<object> FindPlanarFaces(Component2 component)
        {
            List<object> planarFaces = new List<object>();

            try
            {
                Body2 body = component.GetBody();
                if (body == null) return planarFaces;

                object[] faces = body.GetFaces() as object[];
                if (faces == null) return planarFaces;

                foreach (object faceObj in faces)
                {
                    Face2 face = faceObj as Face2;
                    if (face == null) continue;

                    Surface surf = face.GetSurface();
                    if (surf == null) continue;

                    // Проверяем, является ли поверхность плоской
                    bool isPlane = surf.IsPlane();
                    if (isPlane)
                    {
                        planarFaces.Add(face);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при поиске плоских граней: {ex.Message}");
            }

            return planarFaces;
        }

        // Поиск элемента конструктивной геометрии по имени (например, SettingPlane)
        public object FindFeatureByName(Component2 component, string featureName)
        {
            try
            {
                Feature feature = component.FirstFeature();
                while (feature != null)
                {
                    if (feature.Name.Equals(featureName, StringComparison.OrdinalIgnoreCase))
                    {
                        return feature;
                    }
                    feature = feature.GetNextFeature();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при поиске свойства '{featureName}': {ex.Message}");
            }

            return null;
        }

        // Универсальный метод для поиска SettingPlane (работает и с деталями, и со сборками)
        public object FindSettingPlaneUniversal(Component2 component)
        {
            try
            {
                // 1. Ищем SettingPlane в компоненте и его дочерних элементах
                object settingPlane = FindSettingPlaneRecursive(component);
                if (settingPlane != null) return settingPlane;

                // 2. Если не нашли, ищем плоскую грань в текущем компоненте
                var planarFaces = FindPlanarFaces(component);
                if (planarFaces.Count > 0) return planarFaces[0];

                // 3. Для сборок: ищем в первом дочернем компоненте
                ModelDoc2 compDoc = component.GetModelDoc2();
                if (compDoc != null && compDoc.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
                {
                    object children = component.GetChildren();
                    if (children is object[] childArray && childArray.Length > 0)
                    {
                        Component2 firstChild = childArray[0] as Component2;
                        if (firstChild != null)
                        {
                            planarFaces = FindPlanarFaces(firstChild);
                            if (planarFaces.Count > 0) return planarFaces[0];
                        }
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        // рекурсивный метод поиска SettingPlane
        private object FindSettingPlaneRecursive(Component2 component)
        {
            try
            {
                // 1. Ищем SettingPlane в текущем компоненте
                object settingPlane = FindFeatureByName(component, "SettingPlane");
                if (settingPlane != null) return settingPlane;

                // 2. Если компонент - сборка, ищем в дочерних
                ModelDoc2 compDoc = component.GetModelDoc2();
                if (compDoc != null && compDoc.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
                {
                    object children = component.GetChildren();
                    if (children is object[] childArray)
                    {
                        foreach (object childObj in childArray)
                        {
                            Component2 childComp = childObj as Component2;
                            if (childComp != null)
                            {
                                settingPlane = FindSettingPlaneRecursive(childComp);
                                if (settingPlane != null) return settingPlane;
                            }
                        }
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
