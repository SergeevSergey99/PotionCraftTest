using UnityEngine;
using UnityEngine.Serialization;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable/Ingredient")]
    public class IngredientSO : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int score;
        
        public string Name => _name;
        public Sprite Icon => _icon;
        public int Score => score;
    }
}