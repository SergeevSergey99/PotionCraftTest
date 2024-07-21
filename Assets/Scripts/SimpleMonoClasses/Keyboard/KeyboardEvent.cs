using UnityEngine;
using UnityEngine.Events;

namespace Keyboard
{
    public class KeyboardEvent : KeyBoardBase
    {
        [SerializeField] private UnityEvent _event;

        protected override void OnClick() => _event.Invoke();
    }
}
