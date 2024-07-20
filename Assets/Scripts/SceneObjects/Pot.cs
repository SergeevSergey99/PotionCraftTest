using System.Collections.Generic;
using Scriptable;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private List<IngredientSO> _ingredients = new();
    
    public delegate void IngredientsChanged(List<IngredientSO> ingredients);
    public static IngredientsChanged OnIngredientsChanged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ingredient ingredient = collision.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            AddIngredient(ingredient);
        }
    }

    private void AddIngredient(Ingredient ingredient)
    {
        _ingredients.Add(ingredient.IngredientSO);
        
        IngredientsObjectPool.ReturnObject(ingredient);
        
        OnIngredientsChanged?.Invoke(_ingredients);

        if (_ingredients.Count >= 5)
        {
            CookDish();
        }
    }

    private void CookDish()
    {
        PlayerData.AddDish(new DishLog(new List<IngredientSO>(_ingredients)));
        DiscardIngredients();
    }
    public void DiscardIngredients()
    {
        _ingredients.Clear();
        OnIngredientsChanged?.Invoke(_ingredients);
    }
    
    private void OnDestroy()
    {
        OnIngredientsChanged = null;
    }
}