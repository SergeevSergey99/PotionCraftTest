using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardEvent : MonoBehaviour
{
    [SerializeField] private string _keyCode;
    [SerializeField] private UnityEvent _event;

    bool _isPressed = false;
    private void Update()
    {
        if (Input.GetAxis(_keyCode) > 0 && !_isPressed)
        {
            _isPressed = true;
            _event.Invoke();
        }
        else if (Input.GetAxis(_keyCode) == 0)
        {
            _isPressed = false;
        }
    }
}
