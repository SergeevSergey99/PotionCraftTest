using System.Collections.Generic;
using SceneObjects;
using Scriptable;
using UnityEngine;

public class GlobalMethods : MonoBehaviour
{
    private Pot _pot = null;

    private void Awake()
    {
        _pot = FindFirstObjectByType<Pot>();
    }
    public void DiscardScene()
    {
        PlayerData.Discard();
        IngredientsObjectPool.ReturnAllObjects();
        _pot?.DiscardIngredients();
    }
    public static void Load()
    {
        PlayerData.Instance.Load();
    }
    
    private List<DishLog> _allDishes = null;
    public void LogAllCombination()
    {
        if (_allDishes == null || _allDishes.Count == 0) 
        {
            _allDishes = new List<DishLog>();
            IngredientSO[] ingredients = PlayerData.Instance.AllIngredients; 
            int combinationLength = 5;
            
            void Backtrack(Dictionary<IngredientSO, int> current, int remainingLength, int startIndex)
            {
                if (remainingLength == 0)
                {
                    _allDishes.Add(new DishLog(current));
                    return;
                }

                for (int i = startIndex; i < ingredients.Length; i++)
                {
                    var ingredient = ingredients[i];
                    if (!current.ContainsKey(ingredient))
                    {
                        current[ingredient] = 0;
                    }
                    current[ingredient]++;
                    Backtrack(current, remainingLength - 1, i);
                    current[ingredient]--;
                    if (current[ingredient] == 0)
                    {
                        current.Remove(ingredient);
                    }
                }
            }

            Backtrack(new Dictionary<IngredientSO, int>(), combinationLength, 0);
            
            _allDishes.Sort((a, b) => b.Score.CompareTo(a.Score));
        }
        // for case of 5 ingredient types and 5 per dish 126 combinations will be generated
        // C(n+k-1, k-1) = C(9, 4) = 126
        string logString = "All Combination:\n"; 
        for (int i = 0; i < _allDishes.Count; i++)
        {
            logString += $"№{i+1} - {_allDishes[i].ToString()} \n";
        }
        Debug.Log(logString);
    }

    public static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}