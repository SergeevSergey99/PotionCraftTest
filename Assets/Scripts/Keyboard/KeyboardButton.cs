using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardButton : MonoBehaviour
{
    [SerializeField] private string _keyCode;
    [SerializeField] private Button _button;

    bool _isPressed = false;
    private void Update()
    {
        if (Input.GetAxis(_keyCode) > 0 && _button.IsActive() && !_isPressed)
        {
            _isPressed = true;
            _button.onClick.Invoke();
        }
        else if (Input.GetAxis(_keyCode) == 0)
        {
            _isPressed = false;
        }
    }
}
