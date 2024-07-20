
using TMPro;
using UnityEngine;

namespace Renderers
{
    public class DishesRenderer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _lastDishText;
        [SerializeField] private TMP_Text _bestDishText;

        private string _lastDishTextPattern;
        private string _bestDishTextPattern;
        private const string PATTERN_TO_REPLACE = "{value}";

        private void Awake()
        {
            _lastDishTextPattern = _lastDishText.text;
            _bestDishTextPattern = _bestDishText.text;
        }

        private void OnEnable()
        {
            SetLastDish(PlayerData.LastDish);
            SetBestDish(PlayerData.BestDish);
            PlayerData.OnLastDishLogChanged += SetLastDish;
            PlayerData.OnBestDishLogChanged += SetBestDish;
        }

        private void SetLastDish(DishLog lastDish)
        {
            if (lastDish == null)
            {
                _lastDishText.text = _lastDishTextPattern.Replace(PATTERN_TO_REPLACE, string.Empty);
                return;
            }

            _lastDishText.text = _lastDishTextPattern.Replace(PATTERN_TO_REPLACE, lastDish.ToString());
        }

        private void SetBestDish(DishLog bestDish)
        {
            if (bestDish == null)
            {
                _bestDishText.text = _bestDishTextPattern.Replace(PATTERN_TO_REPLACE, string.Empty);
                return;
            }

            _bestDishText.text = _bestDishTextPattern.Replace(PATTERN_TO_REPLACE, bestDish.ToString());
        }


        private void OnDisable()
        {
            PlayerData.OnLastDishLogChanged -= SetLastDish;
            PlayerData.OnBestDishLogChanged -= SetBestDish;
        }
    }
}