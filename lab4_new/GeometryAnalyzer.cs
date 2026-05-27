using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;

namespace lab4_new
{
    public class GeometryAnalyzer
    {
        public GeometryAnalyzer() { }

        // Поиск вертикальной плоскости (нормаль перпендикулярна Y)
        public object FindVerticalPlane(Component2 component)
        {
            try
            {
                var verticalPlanes = FindVerticalPlanes(component);
                return verticalPlanes.Count > 0 ? verticalPlanes[0] : null;
            }
            catch
            {
                return null;
            }
        }

        // Поиск горизонтальной плоскости (нормаль параллельна Y)
        public object FindHorizontalPlane(Component2 component)
        {
            try
            {
                var horizontalPlanes = FindHorizontalPlanes(component);
                return horizontalPlanes.Count > 0 ? horizontalPlanes[0] : null;
            }
            catch
            {
                return null;
            }
        }

        // Поиск вертикального цилиндра (ось параллельна Y)
        public object FindVerticalCylinderFace(Component2 component)
        {
            try
            {
                var verticalCylinders = FindVerticalCylinderFaces(component);
                return verticalCylinders.Count > 0 ? verticalCylinders[0] : null;
            }
            catch
            {
                return null;
            }
        }

        // Поиск вертикального круглого ребра (ось параллельна Y)
        public object FindVerticalCylinderEdge(Component2 component)
        {
            try
            {
                var verticalEdges = FindVerticalCylinderEdges(component);
                return verticalEdges.Count > 0 ? verticalEdges[0] : null;
            }
            catch
            {
                return null;
            }
        }

        // Поиск горизонтального цилиндра (ось параллельна XZ)
        public object FindHorizontalCylinderFace(Component2 component)
        {
            try
            {
                var horizontalCylinders = FindHorizontalCylinderFaces(component);
                return horizontalCylinders.Count > 0 ? horizontalCylinders[0] : null;
            }
            catch
            {
                return null;
            }
        }

        // Поиск горизонтального круглого ребра (ось параллельна XZ)
        public object FindHorizontalCylinderEdge(Component2 component)
        {
            try
            {
                var horizontalEdges = FindHorizontalCylinderEdges(component);
                return horizontalEdges.Count > 0 ? horizontalEdges[0] : null;
            }
            catch
            {
                return null;
            }
        }

        // Простой метод для поиска плоскости (по умолчанию)
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

        // Поиск цилиндра с минимальным радиусом
        public object FindPinCylinder(Component2 component)
        {
            try
            {
                var cylindricalFaces = FindCylindricalFaces(component);
                if (cylindricalFaces.Count == 0) return null;

                if (cylindricalFaces.Count == 1) return cylindricalFaces[0];

                return SelectCylinderWithMinRadius(cylindricalFaces);
            }
            catch
            {
                return null;
            }
        }

        // Найти вертикальные плоскости (нормаль перпендикулярна Y)
        private List<object> FindVerticalPlanes(Component2 component)
        {
            List<object> verticalPlanes = new List<object>();

            try
            {
                Body2 body = component.GetBody();
                if (body == null) return verticalPlanes;

                object[] faces = body.GetFaces() as object[];
                if (faces == null) return verticalPlanes;

                foreach (object faceObj in faces)
                {
                    Face2 face = faceObj as Face2;
                    if (face == null) continue;

                    Surface surf = face.GetSurface();
                    if (surf == null || !surf.IsPlane()) continue;

                    object[] planeParams = surf.PlaneParams as object[];
                    if (planeParams != null && planeParams.Length >= 3)
                    {
                        double normalY = Math.Abs((double)planeParams[1]);

                        if (normalY < 0.1)
                        {
                            verticalPlanes.Add(face);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при поиске вертикальных плоскостей: {ex.Message}");
            }

            return verticalPlanes;
        }

        // Найти горизонтальные плоскости (нормаль параллельна Y)
        private List<object> FindHorizontalPlanes(Component2 component)
        {
            List<object> horizontalPlanes = new List<object>();

            try
            {
                Body2 body = component.GetBody();
                if (body == null) return horizontalPlanes;

                object[] faces = body.GetFaces() as object[];
                if (faces == null) return horizontalPlanes;

                foreach (object faceObj in faces)
                {
                    Face2 face = faceObj as Face2;
                    if (face == null) continue;

                    Surface surf = face.GetSurface();
                    if (surf == null || !surf.IsPlane()) continue;

                    object[] planeParams = surf.PlaneParams as object[];
                    if (planeParams != null && planeParams.Length >= 3)
                    {
                        double normalY = Math.Abs((double)planeParams[1]);

                        if (normalY > 0.9)
                        {
                            horizontalPlanes.Add(face);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при поиске горизонтальных плоскостей: {ex.Message}");
            }

            return horizontalPlanes;
        }

        // Найти вертикальные цилиндрические грани (ось параллельна Y)
        private List<object> FindVerticalCylinderFaces(Component2 component)
        {
            List<object> verticalCylinders = new List<object>();

            try
            {
                Body2 body = component.GetBody();
                if (body == null) return verticalCylinders;

                object[] faces = body.GetFaces() as object[];
                if (faces == null) return verticalCylinders;

                foreach (object faceObj in faces)
                {
                    Face2 face = faceObj as Face2;
                    if (face == null) continue;

                    Surface surf = face.GetSurface();
                    if (surf == null || !surf.IsCylinder()) continue;

                    object[] cylParams = surf.CylinderParams as object[];
                    if (cylParams != null && cylParams.Length >= 7)
                    {
                        double axisY = Math.Abs((double)cylParams[4]);

                        if (axisY > 0.9)
                        {
                            verticalCylinders.Add(face);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при поиске вертикальных цилиндров: {ex.Message}");
            }

            return verticalCylinders;
        }

        // Найти вертикальные круглые рёбра (ось параллельна Y)
        private List<object> FindVerticalCylinderEdges(Component2 component)
        {
            List<object> verticalEdges = new List<object>();

            try
            {
                Body2 body = component.GetBody();
                if (body == null) return verticalEdges;

                object[] edges = body.GetEdges() as object[];
                if (edges == null) return verticalEdges;

                foreach (object edgeObj in edges)
                {
                    Edge edge = edgeObj as Edge;
                    if (edge == null) continue;

                    Curve curve = edge.GetCurve();
                    if (curve == null || !curve.IsCircle()) continue;

                    object[] circleParams = curve.CircleParams as object[];
                    if (circleParams != null && circleParams.Length >= 6)
                    {
                        double axisY = Math.Abs((double)circleParams[4]);

                        if (axisY > 0.9)
                        {
                            verticalEdges.Add(edge);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при поиске вертикальных рёбер: {ex.Message}");
            }

            return verticalEdges;
        }

        // Найти горизонтальные цилиндрические грани (ось параллельна XZ)
        private List<object> FindHorizontalCylinderFaces(Component2 component)
        {
            List<object> horizontalCylinders = new List<object>();

            try
            {
                Body2 body = component.GetBody();
                if (body == null) return horizontalCylinders;

                object[] faces = body.GetFaces() as object[];
                if (faces == null) return horizontalCylinders;

                foreach (object faceObj in faces)
                {
                    Face2 face = faceObj as Face2;
                    if (face == null) continue;

                    Surface surf = face.GetSurface();
                    if (surf == null || !surf.IsCylinder()) continue;

                    object[] cylParams = surf.CylinderParams as object[];
                    if (cylParams != null && cylParams.Length >= 7)
                    {
                        double axisY = Math.Abs((double)cylParams[4]);

                        if (axisY < 0.1)
                        {
                            horizontalCylinders.Add(face);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при поиске горизонтальных цилиндров: {ex.Message}");
            }

            return horizontalCylinders;
        }

        // Найти горизонтальные круглые рёбра (ось параллельна XZ)
        private List<object> FindHorizontalCylinderEdges(Component2 component)
        {
            List<object> horizontalEdges = new List<object>();

            try
            {
                Body2 body = component.GetBody();
                if (body == null) return horizontalEdges;

                object[] edges = body.GetEdges() as object[];
                if (edges == null) return horizontalEdges;

                foreach (object edgeObj in edges)
                {
                    Edge edge = edgeObj as Edge;
                    if (edge == null) continue;

                    Curve curve = edge.GetCurve();
                    if (curve == null || !curve.IsCircle()) continue;

                    object[] circleParams = curve.CircleParams as object[];
                    if (circleParams != null && circleParams.Length >= 6)
                    {
                        double axisY = Math.Abs((double)circleParams[4]);

                        if (axisY < 0.1)
                        {
                            horizontalEdges.Add(edge);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при поиске горизонтальных рёбер: {ex.Message}");
            }

            return horizontalEdges;
        }

        // Метод для поиска цилиндрических граней в компоненте
        public List<object> FindCylindricalFaces(Component2 component)
        {
            List<object> cylindricalFaces = new List<object>();

            try
            {
                Body2 body = component.GetBody();
                if (body == null) return cylindricalFaces;

                object[] faces = body.GetFaces() as object[];
                if (faces == null) return cylindricalFaces;

                foreach (object faceObj in faces)
                {
                    Face2 face = faceObj as Face2;
                    if (face == null) continue;

                    Surface surf = face.GetSurface();
                    if (surf == null) continue;

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

        // Поиск элемента конструктивной геометрии по имени
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

        // Универсальный метод для поиска SettingPlane
        public object FindSettingPlaneUniversal(Component2 component)
        {
            try
            {
                object settingPlane = FindSettingPlaneRecursive(component);
                if (settingPlane != null) return settingPlane;

                var planarFaces = FindPlanarFaces(component);
                if (planarFaces.Count > 0) return planarFaces[0];

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

        // Рекурсивный метод поиска SettingPlane
        private object FindSettingPlaneRecursive(Component2 component)
        {
            try
            {
                object settingPlane = FindFeatureByName(component, "SettingPlane");
                if (settingPlane != null) return settingPlane;

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

        // Выбрать цилиндр с минимальным радиусом
        private object SelectCylinderWithMinRadius(List<object> cylindricalFaces)
        {
            Face2 bestCylinder = null;
            double minRadius = double.MaxValue;

            foreach (object faceObj in cylindricalFaces)
            {
                Face2 face = faceObj as Face2;
                if (face == null) continue;

                try
                {
                    Surface surf = face.GetSurface();
                    if (surf != null && surf.IsCylinder())
                    {
                        object cylParams = surf.CylinderParams;
                        if (cylParams is double[] paramsArray && paramsArray.Length >= 7)
                        {
                            double radius = Math.Abs(paramsArray[6]);

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
    }
}