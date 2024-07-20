using System.IO;
using UnityEditor;
using UnityEngine;

namespace CodeUtils
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
    {
        private static T instance;
        private static readonly string FileAssetName = typeof(T).Name;
        private static readonly string DirectoryAssetsPath = "Assets/Resources";
        private static readonly string FileAssetFullPath = DirectoryAssetsPath + "/" + FileAssetName + ".asset";

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.LoadAll<T>("")[0];

                    if (instance == null)
                    {
                        instance = CreateInstance<T>();
#if UNITY_EDITOR
                        if (!Directory.Exists(DirectoryAssetsPath))
                            Directory.CreateDirectory(DirectoryAssetsPath);

                        AssetDatabase.CreateAsset(instance, FileAssetFullPath);
                        AssetDatabase.SaveAssets();
#endif
                    }
                }

                return instance;
            }
        }
    }
}