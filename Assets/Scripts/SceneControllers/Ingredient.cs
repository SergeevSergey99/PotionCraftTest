using Scriptable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SceneObjects
{
    [RequireComponent(typeof(PolygonCollider2D))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Ingredient : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private PolygonCollider2D _collider;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _dragSpeed = 10;
        [SerializeField] private float _rotationSpeedRange = 100;

        public IngredientSO ingredientSO { get; private set; }

        public void SetIngredient(IngredientSO ingredient)
        {
            ingredientSO = ingredient;
            _spriteRenderer.sprite = ingredientSO.Icon;
            _collider.points = ingredientSO.Points;
        }

        public void AddRotationSpeed(float speed)
        {
            _rigidbody.angularVelocity = speed;
        }

        public void StartDragging()
        {
            _rigidbody.gravityScale = 0;
            AddRotationSpeed(Random.Range(-_rotationSpeedRange, _rotationSpeedRange));
        }

        public void SetDestination(Vector2 getMouseWorldPosition)
        {
            var direction = getMouseWorldPosition - (Vector2)transform.position;
            _rigidbody.velocity = direction * _dragSpeed;
        }

        public void StopDragging()
        {
            _rigidbody.gravityScale = 1;
        }

        private void OnValidate()
        {
            if (_collider == null)
                _collider = GetComponent<PolygonCollider2D>();

            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody2D>();

            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}