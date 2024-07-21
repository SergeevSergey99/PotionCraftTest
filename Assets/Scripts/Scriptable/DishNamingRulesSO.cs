using System.Collections.Generic;
using CodeUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "DishNamingRules", menuName = "Scriptable/DishNamingRules")]
    public class DishNamingRulesSO : SingletonScriptableObject<DishNamingRulesSO>
    {
        [System.Serializable]
        public class Rule
        {
            public IngredientSO ingredientType;
            public int minCount;
            public int maxCount;
            public DishSO dish;
        }
        
        [SerializeField] private List<Rule> _rules;
        [SerializeField] private DishSO _defaultDish;
        
        public static DishSO GetDish(Dictionary<IngredientSO, int> ingredients)
        {
            for (int i = 0; i < Instance._rules.Count; i++)
            {
                int count = 0;
                if (ingredients.TryGetValue(Instance._rules[i].ingredientType, out var value))
                    count = value;

                if (count >= Instance._rules[i].minCount && count <= Instance._rules[i].maxCount)
                    return Instance._rules[i].dish;
            }

            return Instance._defaultDish;
        }
        
    }
}