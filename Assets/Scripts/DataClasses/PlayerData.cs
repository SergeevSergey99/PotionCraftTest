using System.Linq;
using CodeUtils;
using Scriptable;
using UnityEngine;

public class PlayerData : MonoSingleton<PlayerData>
{
    private int _score;
    private DishLog _lastDishLog;
    private DishLog _bestDishLog;
    
    private IngredientSO[] _allIngredients = null;
    public IngredientSO[] AllIngredients
    {
        get
        {
            if (_allIngredients == null)
                _allIngredients = Resources.LoadAll<IngredientSO>("Ingredients");
            return _allIngredients;
        }
    }

    #region events
    public delegate void ScoreChanged(int value);
    public static event ScoreChanged OnScoreChanged;
    
    public delegate void DishLogChanged(DishLog value);
    public static event DishLogChanged OnLastDishLogChanged;
    public static event DishLogChanged OnBestDishLogChanged;
    #endregion
    #region getters and setters
    public static int Score
    {
        get => Instance._score; 
        set
        {
            Instance._score = value;
            OnScoreChanged?.Invoke(Score);
        }
    }
    
    public static void AddDish(DishLog dish)
    {
        LastDish = dish;
        
        if (BestDish == null || dish.Score > BestDish.Score)
            BestDish = dish;
        
        Score += (int) dish.Score;
        Instance.Save();
    }
    public static DishLog LastDish
    {
        get => Instance._lastDishLog; 
        set
        {
            Instance._lastDishLog = value;
            OnLastDishLogChanged?.Invoke(value);
        }
    }

    public static DishLog BestDish
    {
        get => Instance._bestDishLog;
        set
        {
            Instance._bestDishLog = value;
            OnBestDishLogChanged?.Invoke(value);
        }
    }
    #endregion
    
    private const string SCORE_KEY = "Score";
    private const string LAST_DISH_KEY = "LastDish";
    private const string BEST_DISH_KEY = "BestDish";
    
    protected override void DeInit()
    {
        base.DeInit();
        OnScoreChanged = null;
        OnLastDishLogChanged = null;
        OnBestDishLogChanged = null;
    }

    public static void Discard()
    {
        Score = 0;
        LastDish = null;
        BestDish = null;
    }
    public void Save()
    {
        PlayerPrefs.SetInt(SCORE_KEY, Score);
        
        PlayerPrefs.SetString(LAST_DISH_KEY, LastDish == null ? "" 
            : string.Join("&&", LastDish.Ingredients.ConvertAll(ingredient => ingredient.name)));
        
        PlayerPrefs.SetString(BEST_DISH_KEY, BestDish == null ? "" 
            : string.Join("&&", BestDish.Ingredients.ConvertAll(ingredient => ingredient.name)));
        PlayerPrefs.Save();
    }

    public void Load()
    {
        Score = PlayerPrefs.GetInt(SCORE_KEY, 0);
        
        var lastDishIngredients = PlayerPrefs.GetString(LAST_DISH_KEY, "");
        var bestDishIngredients = PlayerPrefs.GetString(BEST_DISH_KEY, "");
        
        LastDish = string.IsNullOrWhiteSpace(lastDishIngredients) ? null 
            : new DishLog(lastDishIngredients.Split("&&").Select(ingredientName => AllIngredients.First(ingredient => ingredient.name == ingredientName)).ToList());
        BestDish = string.IsNullOrWhiteSpace(bestDishIngredients) ? null 
            : new DishLog(bestDishIngredients.Split("&&").Select(ingredientName => AllIngredients.First(ingredient => ingredient.name == ingredientName)).ToList());
    }
    
}