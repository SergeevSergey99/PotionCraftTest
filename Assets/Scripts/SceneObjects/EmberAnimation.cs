using System;
using System.Collections;
using System.Collections.Generic;
using Scriptable;
using UnityEngine;
using UnityEngine.Serialization;

public class EmberAnimation : MonoBehaviour
{
    [SerializeField] private EmberPresetSO _emptyPotPreset;
    [SerializeField] private EmberPresetSO _filledPotPreset;
    [SerializeField] private SpriteRenderer _emberSprite;

    private float _currentTime = 0f;
    private bool _isPotEmpty = true;
    private bool _isTransitioning = false;
    private EmberPresetSO currentPreset => _isPotEmpty ? _emptyPotPreset : _filledPotPreset;

    private void OnEnable()
    {
        Pot.OnIngredientsChanged += OnIngredientsChanged;
    }

    private void OnDisable()
    {
        Pot.OnIngredientsChanged -= OnIngredientsChanged;
    }

    private void OnIngredientsChanged(List<IngredientSO> ingredients)
    {
        SwitchPreset(ingredients.Count == 0);
    }

    private void Update()
    {
        if (_isTransitioning)
            return;
        
        _currentTime += Time.deltaTime;
        if (_currentTime > currentPreset.AnimationDuration)
            _currentTime = 0f;

        float t = _currentTime / currentPreset.AnimationDuration;
        float brightness = currentPreset.BrightnessCurve.Evaluate(t);
        
        _emberSprite.color = new Color(1f, 1f, 1f, brightness);
    }

    public void SwitchPreset(bool isPotEmpty)
    {
        _isPotEmpty = isPotEmpty;
        StartCoroutine(SmoothPresetTransition(currentPreset));
    }

    private IEnumerator SmoothPresetTransition(EmberPresetSO newPreset)
    {
        _isTransitioning = true;
        float startBrightness = _emberSprite.color.a;
        float endBrightness = newPreset.BrightnessCurve.Evaluate(0f);
        float duration = Mathf.Min(_emptyPotPreset.AnimationDuration, _filledPotPreset.AnimationDuration);
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            float brightness = Mathf.Lerp(startBrightness, endBrightness, t);
            _emberSprite.color = new Color(1f, 1f, 1f, brightness);
            yield return null;
        }
        _currentTime = 0f;
        _isTransitioning = false;
    }
}
