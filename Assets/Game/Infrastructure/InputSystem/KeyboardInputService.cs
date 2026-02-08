using UnityEngine;

namespace Infrastructure.InputSystem
{
    public class KeyboardInputService : IInputService
    {
        public bool JumpPressed =>
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetMouseButtonDown(0);
    }
}