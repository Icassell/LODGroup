using System;
using UnityEditor;
using UnityEngine;

namespace Chess.LODGroupIJob
{
    public class CreateSo
    {
        [MenuItem("Zero/EditorTools/CreateAssetByMonoScript")]
        public static void CreateScriptObjectAssetFromSelection()
        {
            if (Selection.activeObject is MonoScript)
                CreateScriptObjectAsset(((MonoScript) Selection.activeObject).GetClass());
            else
                Debug.LogWarning("No ScriptableObject script asset selected");
        }

        public static void CreateScriptObjectAsset(Type type, string defaultPath = null)
        {
            if (type != null && type.IsSubclassOf(typeof(ScriptableObject)))
            {
                var path = EditorUtility.SaveFilePanelInProject("Create ScriptableObject Asset",
                    type.Name, "asset", "Create ScriptableObject Asset", "Assets");

                if (!string.IsNullOrEmpty(path))
                {
                    AssetDatabase.CreateAsset(ScriptableObject.CreateInstance(type), path);
                    Selection.activeObject = AssetDatabase.LoadAssetAtPath(path, type);
                }
            }
            else
            {
                Debug.LogWarning($"Type {(type != null ? type.ToString() : "null")} is not an ScriptableObject's subclass.");
            }
        }
    }
}