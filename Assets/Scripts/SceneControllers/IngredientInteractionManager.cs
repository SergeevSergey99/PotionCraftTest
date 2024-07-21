
using System;
using System.Collections;
using SceneObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class IngredientInteractionManager : MonoBehaviour
{
    [SerializeField] private float _interactionRadius = 0.3f;
    [SerializeField] private LayerMask _ingredientLayer;
    [SerializeField] private LayerMask _ingredientContainerLayer;
    [SerializeField] private Camera _mainCamera;

    private Ingredient _currentIngredient = null;
    private Ingredient _hoveredIngredient = null;

    private void Start()
    {
        StartCoroutine(CheckHoveredIngredient());
    }

    IEnumerator CheckHoveredIngredient()
    {
        while (true)
        {
            if (_currentIngredient == null)
            {
                Vector2 mousePosition = GetMouseWorldPosition();
                Ingredient ingredient = GetIngredientAtPosition(mousePosition);
                if (ingredient != _hoveredIngredient)
                {
                    _hoveredIngredient?.CursorExit();

                    _hoveredIngredient = ingredient;

                    _hoveredIngredient?.CursorEnter();
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
    private void Update()
    {
        // Check if the player is trying to drag an ingredient
        if (Input.GetMouseButtonDown(0))
        {
            if (_hoveredIngredient != null)
            {
                _currentIngredient = _hoveredIngredient;
                _hoveredIngredient = null;
            }
            else
            {
                Vector2 mousePosition = GetMouseWorldPosition();
                // Try to get the ingredient at the mouse position
                _currentIngredient = GetIngredientAtPosition(mousePosition);

                if (_currentIngredient == null)
                {
                    // Otherwise, try to get the ingredient from a container
                    _currentIngredient = GetIngredientFromContainerAtPosition(mousePosition);
                }

            }
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, _interactionRadius, _ingredientLayer);
        
        if (colliders.Length > 0)
        {
            // Search for the closest ingredient
            Ingredient closestIngredient = null;
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
            }

            return closestIngredient;
        }

        return null;
    }
    private Ingredient GetIngredientFromContainerAtPosition(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, _interactionRadius, _ingredientContainerLayer);
        
        if (colliders.Length > 0)
        {
            // Search for the closest container
            IngredientContainer closestContainer = null;
            float closestDistance = float.MaxValue;

            foreach (var collider in colliders)
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

            if (closestContainer != null)
                return closestContainer.SpawnIngredient();
        }
        return null;
    }

    private Vector2 GetMouseWorldPosition()
    {
        return _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnValidate()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetMouseWorldPosition(), _interactionRadius);
    }
}