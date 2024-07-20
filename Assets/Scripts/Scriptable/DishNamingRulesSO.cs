using System.Collections.Generic;
using CodeUtils;
using UnityEditor;
using UnityEngine;

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
        
        [SerializeField] private List<Rule> rules;
        [SerializeField] private DishSO defaultDish;
        
        public static DishSO GetDish(List<IngredientSO> ingredients)
        {
            foreach (var rule in Instance.rules)
            {
                int count = ingredients.FindAll(ingredient => ingredient == rule.ingredientType).Count;

                if (count >= rule.minCount && count <= rule.maxCount)
                {
                    return rule.dish;
                }
            }

            return Instance.defaultDish;
        }
        
    }
}