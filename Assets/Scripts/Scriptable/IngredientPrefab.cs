using UnityEngine;

namespace Scriptable
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(PolygonCollider2D))]
    public class IngredientPrefab : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private PolygonCollider2D _polygonCollider;
        
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public PolygonCollider2D PolygonCollider => _polygonCollider;
        
        private void OnValidate()
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_polygonCollider == null)
                _polygonCollider = GetComponent<PolygonCollider2D>();
        }
    }
}