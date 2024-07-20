
using System;
using SceneObjects;
using UnityEngine;

public class IngredientInteractionManager : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 0.3f;
    [SerializeField] private LayerMask ingredientLayer;
    [SerializeField] private Camera mainCamera;

    private Ingredient _currentIngredient = null;

    private void LateUpdate()
    {
        // Обработка клика
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = GetMouseWorldPosition();
            // Проверяем наличие ингредиента под курсором
            _currentIngredient = GetIngredientAtPosition(mousePosition);
            _currentIngredient?.StartDragging();
        }
        if (_currentIngredient != null)
        {
            _currentIngredient.SetDestination(GetMouseWorldPosition());
            if (Input.GetMouseButtonUp(0))
            {
                _currentIngredient.StopDragging();
                _currentIngredient = null;
            }
        }
    }

    private Ingredient GetIngredientAtPosition(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, interactionRadius, ingredientLayer);
        
        if (colliders.Length > 0)
        {
            // Находим ближайший ингредиент
            Ingredient closestIngredient = null;
            IngredientContainer closestContainer = null;
            float closestDistance = float.MaxValue;

            foreach (var collider in colliders)
            {
                Ingredient ingredient = collider.GetComponent<Ingredient>();
                if (ingredient != null)
                {
                    float distance = Vector2.Distance(position, ingredient.transform.position);
                    if (distance < closestDistance)
                    {
                        closestIngredient = ingredient;
                        closestDistance = distance;
                    }
                }
                else
                {
                    IngredientContainer container = collider.GetComponent<IngredientContainer>();
                    if (container != null)
                    {
                        float distance = Vector2.Distance(position, container.transform.position);
                        if (distance < closestDistance)
                        {
                            closestContainer = container;
                            closestDistance = distance;
                        }
                    }
                }
            }

            if (closestIngredient == null && closestContainer != null)
            {
                closestIngredient = closestContainer.SpawnIngredient();
            }

            return closestIngredient;
        }

        return null;
    }

    private Vector2 GetMouseWorldPosition()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnValidate()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetMouseWorldPosition(), interactionRadius);
    }
}