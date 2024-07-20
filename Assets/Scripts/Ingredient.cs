
using System;
using Scriptable;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(DynamicCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Ingredient : MonoBehaviour
{
    [SerializeField] private DynamicCollider _dynamicCollider;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _dragSpeed = 10;

    private IngredientSO _ingredient;
    
    public IngredientSO IngredientSO => _ingredient;

    public void SetIngredient(IngredientSO ingredient)
    {
        _ingredient = ingredient;
        _dynamicCollider.SetSprite(_ingredient.Icon);
    }
    public void AddRotationSpeed(float speed)
    {
        _rigidbody.angularVelocity = speed;
    }
    public void StartDragging()
    {
        _rigidbody.gravityScale = 0;
        AddRotationSpeed(Random.Range(-100, 100));
    }

    private void OnValidate()
    {
        if (_dynamicCollider == null)
        {
            _dynamicCollider = GetComponent<DynamicCollider>();
        }
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    }

    public void SetDestination(Vector2 getMouseWorldPosition)
    {
        var direction = getMouseWorldPosition - (Vector2) transform.position;
        
        _rigidbody.velocity = direction * _dragSpeed;
    }

    public void StopDragging()
    {
        _rigidbody.gravityScale = 1;
    }
}