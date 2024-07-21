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
            foreach (var rule in Instance._rules)
            {
                int count = 0;
                if (ingredients.TryGetValue(rule.ingredientType, out var value))
                    count = value;

                if (count >= rule.minCount && count <= rule.maxCount)
                {
                    return rule.dish;
                }
            }

            return Instance._defaultDish;
        }
        
    }
}