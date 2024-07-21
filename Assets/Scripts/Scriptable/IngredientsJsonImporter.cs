using System.Collections.Generic;
using System.Linq;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEditor;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "IngredientsJsonImporter", menuName = "Scriptable/IngredientsJsonImporter")]
    public class IngredientsJsonImporter : ScriptableObject
    {
        [SerializeField] private TextAsset _json;
        [SerializeField] private List<IngredientSO> _ingredientsSO;
        
        public void FindAllIngredients()
        {
#if UNITY_EDITOR
            _ingredientsSO.Clear();
            _ingredientsSO = Resources.FindObjectsOfTypeAll<IngredientSO>().ToList();
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
#endif
        }
        public void LoadFromJson()
        {
#if UNITY_EDITOR
            Dictionary<string, IngredientSO> dictionary = new();
            foreach (var ingredient in _ingredientsSO)
            {
                dictionary[ingredient.name] = ingredient;
            }
            
            JObject jsonData = JObject.Parse(_json.text);
            JArray ingredients = (JArray)jsonData["ingredients"];

            foreach (var ingredient in ingredients)
            {
                if (dictionary.TryGetValue((string)ingredient["type"], out IngredientSO ingredientSO))
                {
                    ingredientSO.SetNewData((string)ingredient["name"], (int)ingredient["score"]);
                }
            }
            AssetDatabase.SaveAssets();
#endif
        }
    }
}