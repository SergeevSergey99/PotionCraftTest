using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Keyboard
{
    public class KeyboardButton : KeyBoardBase
    {
        [SerializeField] private Button _button;

        protected override void OnClick() => _button.onClick.Invoke();
        protected override bool ExtraClickCondition() => _button.IsActive();
    }
}