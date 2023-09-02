using UnityEngine;

namespace Pong.ScriptableEvents
{
    [CreateAssetMenu(menuName = "PongCustomScriptables/ScriptableEvents/EventsHub", fileName = "EventsHub")]
    public class ScriptableEventsHub : ScriptableObject
    {
        [SerializeField] private ScriptableEvent startGameEvent;
        [SerializeField] private ScriptableEvent continueGameEvent;

        public ScriptableEvent StartGameEvent => startGameEvent;
        public ScriptableEvent ContinueGameEvent => continueGameEvent;
    }

}
