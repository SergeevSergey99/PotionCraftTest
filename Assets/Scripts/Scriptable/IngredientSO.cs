using UnityEngine;
using UnityEditor;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable/Ingredient")]
    public class IngredientSO : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private IngredientPrefab _prefab;
        [SerializeField] private int _score;
        
        public string Name => _name;
        public Sprite Icon => _prefab.SpriteRenderer.sprite;
        public Vector2[] Points => _prefab.PolygonCollider.points;
        public int Score => _score;

#if UNITY_EDITOR
        public void SetNewData(string newName, int newScore)
        {
            _name = newName;
            _score = newScore;
            //Save changes
            EditorUtility.SetDirty(this);
        }
#endif
    }
}