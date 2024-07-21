using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "EmberPreset", menuName = "Scriptable/EmberPreset")]
    public class EmberPresetSO : ScriptableObject
    {
        [SerializeField] private float _animationDuration;
        [SerializeField] private AnimationCurve _brightnessCurve;
        
        public float AnimationDuration => _animationDuration;
        public AnimationCurve BrightnessCurve => _brightnessCurve;
    }
}
