using Pong.ScriptableEvents;
using UnityEngine;

namespace Pong.UI
{
    public class ContinueButton : MonoBehaviour
    {
        [SerializeField] private ScriptableEventsHub eventsHub;

        public void OnClick()
        {
            eventsHub?.ContinueGameEvent?.RaiseEvent();
        }
    }
}
