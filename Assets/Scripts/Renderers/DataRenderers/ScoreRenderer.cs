using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Renderers
{
    public class ScoreRenderer : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _scoreText;

        private string _textPattern;
        private const string PATTERN_TO_REPLACE = "{number}";

        private void Awake()
        {
            _textPattern = _scoreText.text;
        }

        private void OnEnable()
        {
            SetScore(PlayerData.Score);
            PlayerData.OnScoreChanged += SetScore;
        }

        private void OnDisable()
        {
            PlayerData.OnScoreChanged -= SetScore;
        }

        private void SetScore(int value)
        {
            _scoreText.text = _textPattern.Replace(PATTERN_TO_REPLACE, value.ToString());
        }
    }
}
