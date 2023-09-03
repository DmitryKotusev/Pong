using UnityEngine;
using UnityEngine.InputSystem;

namespace Pong
{
    public class PlayerController : BaseController
    {
        private Vector2 movement;

        private float movementInversion = 1;

        public override void ResetController()
        {
            base.ResetController();
            movementInversion = 1;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            movement = context.action.ReadValue<Vector2>();
            // Debug.Log($"[PlayerController] OnMove, {movement}");
        }

        private void Update()
        {
            float deltaY = movementInversion * movement.y * gameSettings.PlayerPlatformMovementSpeed * Time.deltaTime;
            MovePlatform(deltaY);
        }

        private void OnBoosterHit(object boosterObject)
        {
            BaseBooster booster = boosterObject as BaseBooster;

            if (booster == null)
            {
                Debug.LogError("[PlayerController] OnBoosterHit, cast parameter error: could not cast to BaseBooster");
                return;
            }

            if (booster is ReverseControlBooster)
            {
                movementInversion = -movementInversion;
            }
        }

        private void OnEnable()
        {
            SubscribeToScriptableEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromScriptableEvents();
        }

        private void SubscribeToScriptableEvents()
        {
            eventsHub.BoosterHitEvent.ScriptableSignal += OnBoosterHit;
        }

        private void UnsubscribeFromScriptableEvents()
        {
            eventsHub.BoosterHitEvent.ScriptableSignal -= OnBoosterHit;
        }
    }
}
