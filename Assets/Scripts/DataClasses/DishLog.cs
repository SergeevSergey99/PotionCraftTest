using System.Collections.Generic;
using Scriptable;

public class DishLog
{
    public readonly DishSO Dish;
    public readonly List<IngredientSO> Ingredients = new();
    public readonly float Score;
    
    private Dictionary<IngredientSO, int> ingredientCount = new();

    public DishLog(Dictionary<IngredientSO, int> ingredients)
    {
        ingredientCount = new(ingredients);
        foreach (var pair in ingredients)
        {
            for (int i = 0; i < pair.Value; i++)
            {
                Ingredients.Add(pair.Key);
            }
        }
        Dish = DishNamingRulesSO.GetDish(ingredientCount);
        Score = ComboRulesSO.GetComboScore(ingredientCount);
    }
    public DishLog(List<IngredientSO> ingredients)
    {
        Ingredients = ingredients;
        ingredientCount = new();
        foreach (var ingredient in Ingredients)
        {
            if (ingredientCount.ContainsKey(ingredient))
            {
                ingredientCount[ingredient]++;
            }
            else
            {
                ingredientCount[ingredient] = 1;
            }
        }
        Dish = DishNamingRulesSO.GetDish(ingredientCount);
        Score = ComboRulesSO.GetComboScore(ingredientCount);
    }
    
    public override string ToString()
    {
        string result = Dish.Name + " (";
        
        int sum = 0;
        foreach (var pair in ingredientCount)
        {
            result += $"{pair.Value} {pair.Key.Name}";
            sum += pair.Value;
            if (sum < Ingredients.Count)
                result += ", ";
        }
        result += $") [{Score}]";
        return result;
    }

}