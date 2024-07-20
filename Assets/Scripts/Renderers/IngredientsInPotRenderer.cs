using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Renderers
{
    public class IngredientsInPotRenderer : MonoBehaviour
    {
        [SerializeField] private Transform _ingredientsContainer;
        [SerializeField] private List<Image> _ingredientImages;

        [SerializeField] private Sprite _defaultSprite;
        private void OnEnable()
        {
            Pot.OnIngredientsChanged += RenderIngredients;
        }
        
        private void OnDisable()
        {
            Pot.OnIngredientsChanged -= RenderIngredients;
        }
        
        private void RenderIngredients(List<Scriptable.IngredientSO> ingredients)
        {
            for (int i = 0; i < _ingredientImages.Count; i++)
            {
                if (i < ingredients.Count)
                {
                    _ingredientImages[i].sprite = ingredients[i].Icon;
                }
                else
                {
                    _ingredientImages[i].sprite = _defaultSprite;
                }
            }
        }
        private void OnValidate()
        {
            if (_ingredientsContainer == null)
            {
                _ingredientsContainer = transform;
            }
            
            if (_ingredientImages == null)
            {
                _ingredientImages = new List<Image>();
            }
            
            if (_ingredientImages.Count == 0)
            {
                foreach (Transform child in _ingredientsContainer)
                {
                    Image image = child.GetComponent<Image>();
                    if (image != null)
                    {
                        _ingredientImages.Add(image);
                    }
                }
            }
        }
    }
}