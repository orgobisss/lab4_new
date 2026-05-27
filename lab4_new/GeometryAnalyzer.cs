using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;

namespace lab4_new
{
    public class GeometryAnalyzer
    {
        public GeometryAnalyzer() { }

        // Поиск плоскости SettingPlane или первой плоской грани
        public object FindBurtPlane(Component2 component)
        {
            try
            {
                Feature settingPlane = FindFeatureByName(component, "SettingPlane") as Feature;
                if (settingPlane != null)
                    return settingPlane;

                var planarFaces = FindPlanarFaces(component);
                return planarFaces.Count > 0 ? planarFaces[0] : null;
            }
            catch
            {
                return null;
            }
        }

        // Поиск цилиндра для пальцев
        public object FindPinCylinder(Component2 component)
        {
            try
            {
                var cylindricalFaces = FindCylindricalFaces(component);
                if (cylindricalFaces.Count == 0) return null;

                if (cylindricalFaces.Count == 1)
                    return cylindricalFaces[0];

                return SelectCylinderWithMinRadius(cylindricalFaces);
            }
            catch
            {
                return null;
            }
        }

        private object SelectCylinderWithMinRadius(List<object> cylindricalFaces)
        {
            Face2 best = null;
            double minR = double.MaxValue;

            foreach (object obj in cylindricalFaces)
            {
                Face2 face = obj as Face2;
                if (face == null) continue;

                try
                {
                    Surface surf = face.GetSurface();
                    if (surf != null && surf.IsCylinder())
                    {
                        double[] p = surf.CylinderParams;
                        if (p != null && p.Length >= 7)
                        {
                            double r = Math.Abs(p[6]);
                            if (r < minR)
                            {
                                minR = r;
                                best = face;
                            }
                        }
                    }
                }
                catch { }
            }

            return best ?? cylindricalFaces[0];
        }

        public List<object> FindCylindricalFaces(Component2 component)
        {
            List<object> list = new List<object>();

            try
            {
                Body2 body = component.GetBody();
                if (body == null) return list;

                object[] faces = body.GetFaces() as object[];
                if (faces == null) return list;

                foreach (object obj in faces)
                {
                    Face2 face = obj as Face2;
                    if (face == null) continue;

                    Surface surf = face.GetSurface();
                    if (surf != null && surf.IsCylinder())
                        list.Add(face);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка поиска цилиндров: {ex.Message}");
            }

            return list;
        }

        public List<object> FindPlanarFaces(Component2 component)
        {
            List<object> list = new List<object>();

            try
            {
                Body2 body = component.GetBody();
                if (body == null) return list;

                object[] faces = body.GetFaces() as object[];
                if (faces == null) return list;

                foreach (object obj in faces)
                {
                    Face2 face = obj as Face2;
                    if (face == null) continue;

                    Surface surf = face.GetSurface();
                    if (surf != null && surf.IsPlane())
                        list.Add(face);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка поиска плоскостей: {ex.Message}");
            }

            return list;
        }

        public object FindFeatureByName(Component2 component, string name)
        {
            try
            {
                Feature f = component.FirstFeature();
                while (f != null)
                {
                    if (f.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        return f;

                    f = f.GetNextFeature();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка поиска фичи '{name}': {ex.Message}");
            }

            return null;
        }

        // Универсальный поиск SettingPlane (для сборок)
        public object FindSettingPlaneUniversal(Component2 component)
        {
            try
            {
                object plane = FindSettingPlaneRecursive(component);
                if (plane != null) return plane;

                var planar = FindPlanarFaces(component);
                if (planar.Count > 0) return planar[0];

                ModelDoc2 doc = component.GetModelDoc2();
                if (doc != null && doc.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
                {
                    object children = component.GetChildren();
                    if (children is object[] arr && arr.Length > 0)
                    {
                        Component2 child = arr[0] as Component2;
                        if (child != null)
                        {
                            planar = FindPlanarFaces(child);
                            if (planar.Count > 0) return planar[0];
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

        private object FindSettingPlaneRecursive(Component2 component)
        {
            try
            {
                object plane = FindFeatureByName(component, "SettingPlane");
                if (plane != null) return plane;

                ModelDoc2 doc = component.GetModelDoc2();
                if (doc != null && doc.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
                {
                    object children = component.GetChildren();
                    if (children is object[] arr)
                    {
                        foreach (object obj in arr)
                        {
                            Component2 child = obj as Component2;
                            if (child != null)
                            {
                                plane = FindSettingPlaneRecursive(child);
                                if (plane != null) return plane;
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
