using UnityEngine;

namespace Keyboard
{
    public abstract class KeyBoardBase : MonoBehaviour
    {
        [SerializeField] private string _keyCode;

        private bool _isPressed = false;

        private void Update()
        {
            if (Input.GetAxis(_keyCode) > 0 && !_isPressed && ExtraClickCondition())
            {
                _isPressed = true;
                OnClick();
            }
            else if (Input.GetAxis(_keyCode) == 0 && _isPressed)
            {
                _isPressed = false;
            }
        }
        
        protected abstract void OnClick();
        protected virtual bool ExtraClickCondition() { return true; }
    }
}