using UnityEngine;

namespace Pong.ScriptableEvents
{
    [CreateAssetMenu(menuName = "PongCustomScriptables/ScriptableEvents/EventsHub", fileName = "EventsHub")]
    public class ScriptableEventsHub : ScriptableObject
    {
        [SerializeField] private ScriptableEvent startGameEvent;
        [SerializeField] private ScriptableEvent continueGameEvent;
        [SerializeField] private ScriptableEventObject scoreGoalEvent;
        [SerializeField] private ScriptableEventObject playerWonEvent;

        public ScriptableEvent StartGameEvent => startGameEvent;
        public ScriptableEvent ContinueGameEvent => continueGameEvent;
        public ScriptableEventObject ScoreGoalEvent => scoreGoalEvent;
        public ScriptableEventObject PlayerWonEvent => playerWonEvent;
    }

}
