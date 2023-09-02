using UnityEngine;
using UnityEngine.InputSystem;

namespace Pong
{
    public class PlayerController : BaseController
    {
        private Vector2 movement;

        public void OnMove(InputAction.CallbackContext context)
        {
            movement = context.action.ReadValue<Vector2>();
            // Debug.Log($"[PlayerController] OnMove, {movement}");
        }

        private void Update()
        {
            float deltaY = movement.y * gameSettings.PlayerPlatformMovementSpeed * Time.deltaTime;
            MovePlatform(deltaY);
        }
    }
}
