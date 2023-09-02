using Pong.ScriptableEvents;
using UnityEngine;

namespace Pong.UI
{
    public class StartGameButton : MonoBehaviour
    {
        [SerializeField] private ScriptableEventsHub eventsHub;

        public void OnClick()
        {
            eventsHub?.StartGameEvent?.RaiseEvent();
        }
    }
}
