using UnityEngine;

namespace Game.Generic.InputManager
{
    readonly struct MouseClickData
    {
        public readonly bool IsClicked;

        public MouseClickData(bool isClicked)
        {
            IsClicked = isClicked;
        }
    }
}
