using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class DynamicCollider : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _boxCollider;
    
    private void OnEnable()
    {
        UpdateColliderSize();
    }
    
    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        UpdateColliderSize();
    }

    private void UpdateColliderSize()
    {
        if (_spriteRenderer.sprite == null)
        {
            _boxCollider.size = Vector2.zero;
            return;
        }

        _boxCollider.size = _spriteRenderer.sprite.bounds.size;
    }
    private void OnValidate()
    {
        if (_spriteRenderer == null || _boxCollider == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider = GetComponent<BoxCollider2D>();
        }
        UpdateColliderSize();
    }
}