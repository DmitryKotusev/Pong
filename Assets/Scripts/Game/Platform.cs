using Pong.ScriptableEvents;
using UnityEngine;

namespace Pong
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private ScriptableEventsHub eventsHub;

        private void OnBoosterHit(object boosterObject)
        {
            BaseBooster booster = boosterObject as BaseBooster;

            if (booster == null)
            {
                Debug.LogError("[Platform] OnBoosterHit, cast parameter error: could not cast to BaseBooster");
                return;
            }

            if (booster is IncreasePlatformSizeBooster)
            {
                float newSizeY = Mathf.Clamp(transform.localScale.y + gameSettings.PlatformSizeChangeStep, gameSettings.PlatformMinSize, gameSettings.PlatformMaxSize);
                transform.localScale = new Vector3(
                    transform.localScale.x,
                    newSizeY,
                    transform.localScale.z
                    );
            }
            else if (booster is DecreasePlatformSizeBooster)
            {
                float newSizeY = Mathf.Clamp(transform.localScale.y - gameSettings.PlatformSizeChangeStep, gameSettings.PlatformMinSize, gameSettings.PlatformMaxSize);
                transform.localScale = new Vector3(
                    transform.localScale.x,
                    newSizeY,
                    transform.localScale.z
                    );
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
