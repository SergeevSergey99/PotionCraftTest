#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Scriptable.Editor
{
    [CustomEditor(typeof(IngredientsJsonImporter))]
    public class IngredientsJsonImporterEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            IngredientsJsonImporter importer = (IngredientsJsonImporter)target;
            
            if (GUILayout.Button("Find all Ingredients"))
            {
                importer.FindAllIngredients();
            }
            EditorGUILayout.Space();
            
            DrawDefaultInspector();
            
            EditorGUILayout.Space();
            if (GUILayout.Button("Load from JSON"))
            {
                importer.LoadFromJson();
            }
        }
    }
}
#endif