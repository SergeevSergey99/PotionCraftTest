using System.Collections.Generic;
using CodeUtils;
using UnityEditor;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "ComboRules", menuName = "Scriptable/ComboRules")]
    public class ComboRulesSO : SingletonScriptableObject<ComboRulesSO>
    {   
        [System.Serializable]
        public class ComboRule
        {
            public int sameIngredientCount;
            public float multiplier;
        }

        [SerializeField] private List<ComboRule> rules;
        [SerializeField] private float allDifferentComboMultiplier = 2f;
        
        public static float GetComboScore(Dictionary<IngredientSO, int> ingredientCount)
        {
            float score = 0;
            bool allDifferent = true;
            
            // Calculate score
            foreach (var pair in ingredientCount)
            {
                float ingredientScore = pair.Key.Score * pair.Value;
                
                if (pair.Value > 1)
                {
                    allDifferent = false;
                    
                    // Check for combo rules
                    for (int i = 0; i < Instance.rules.Count; i++)
                    {
                        if (pair.Value == Instance.rules[i].sameIngredientCount)
                        {
                            ingredientScore *= Instance.rules[i].multiplier;
                            break;
                        }
                    }
                }
                score += ingredientScore;
            }

            if (allDifferent)
            {
                score *= Instance.allDifferentComboMultiplier;
            }

            return score;
        }
    }
}