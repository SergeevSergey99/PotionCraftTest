using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Dish", menuName = "Scriptable/Dish")]
    public class DishSO : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        
        public string Name => _name;
        public Sprite Icon => _icon;
    }
}