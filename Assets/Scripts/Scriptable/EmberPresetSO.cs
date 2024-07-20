using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "EmberPreset", menuName = "Scriptable/EmberPreset")]
    public class EmberPresetSO : ScriptableObject
    {
        [SerializeField] private float animationDuration;
        [SerializeField] private AnimationCurve brightnessCurve;
        
        public float AnimationDuration => animationDuration;
        public AnimationCurve BrightnessCurve => brightnessCurve;
    }
}
