
using UnityEngine;

public class PlayerDataController : MonoBehaviour
{
    private Pot _pot = null;
    public void Discard()
    {
        PlayerData.Discard();
        IngredientsObjectPool.ReturnAllObjects();
        
        if (_pot == null)
            _pot = FindObjectOfType<Pot>();
        
        _pot.DiscardIngredients();
    }
    public void Load()
    {
        PlayerData.Instance.Load();
    }
    
}