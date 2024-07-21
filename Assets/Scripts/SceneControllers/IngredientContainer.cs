using Scriptable;
using UnityEngine;

namespace SceneObjects
{
    public class IngredientContainer : MonoBehaviour
    {
        [SerializeField] private IngredientSO _ingredient;
        public IngredientSO Ingredient => _ingredient;
        
        public Ingredient SpawnIngredient()
        {
            var ingredient = IngredientsObjectPool.GetObject();
            
            if (ingredient == null)
                return null;
            
            ingredient.transform.position = new Vector3(transform.position.x, transform.position.y, ingredient.transform.position.z);
            ingredient.SetIngredient(_ingredient);
            return ingredient;
        }
    }
}