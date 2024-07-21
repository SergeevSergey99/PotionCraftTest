using UnityEngine;
using UnityEngine.Serialization;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable/Ingredient")]
    public class IngredientSO : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private IngredientPrefab _prefab;
        [SerializeField] private int score;
        
        public string Name => _name;
        public Sprite Icon => _prefab.SpriteRenderer.sprite;
        public Vector2[] Points => _prefab.PolygonCollider.points;
        public int Score => score;
    }
}