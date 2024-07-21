
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
    [SerializeField] private float _hoverUpdateRate = 0.1f;

    private Ingredient _currentIngredient = null;
    private Ingredient _hoveredIngredient = null;

    private void Start()
    {
        StartCoroutine(CheckHoveredIngredient());
    }

    IEnumerator CheckHoveredIngredient()
    {
        // It is also possible to use extra collider to detect hovered ingredient
        // But it is not possible to detect closest ingredient this way
        // So I decided to use this method
        while (true)
        {
            if (_currentIngredient == null)
            {
                Vector2 mousePosition = GetMouseWorldPosition();
                Ingredient ingredient = GetClosestComponentAtPosition<Ingredient>(mousePosition, _ingredientLayer);
                if (ingredient != _hoveredIngredient)
                {
                    _hoveredIngredient?.CursorExit();

                    _hoveredIngredient = ingredient;

                    _hoveredIngredient?.CursorEnter();
                }
            }

            yield return new WaitForSeconds(_hoverUpdateRate);
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
                _currentIngredient = GetClosestComponentAtPosition<Ingredient>(mousePosition, _ingredientLayer);

                if (_currentIngredient == null)
                {
                    // Otherwise, try to get the ingredient from a container
                    _currentIngredient = GetClosestComponentAtPosition<IngredientContainer>(mousePosition, _ingredientContainerLayer)?.SpawnIngredient();
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

    
    private T GetClosestComponentAtPosition<T>(Vector2 position, LayerMask layerMask) where T : MonoBehaviour
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, _interactionRadius, layerMask);
        
        if (colliders.Length > 0)
        {
            // Search for the closest component
            T closestComponent = null;
            float closestDistance = float.MaxValue;

            for (int i = 0; i < colliders.Length; i++)
            {
                T component = colliders[i].GetComponent<T>();
                if (component != null)
                {
                    float distance = Vector2.Distance(position, component.transform.position);
                    if (distance < closestDistance)
                    {
                        closestComponent = component;
                        closestDistance = distance;
                    }
                }
            }

            return closestComponent;
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