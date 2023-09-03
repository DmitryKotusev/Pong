using UnityEngine;

namespace Pong.ScriptableEvents
{
    [CreateAssetMenu(menuName = "PongCustomScriptables/ScriptableEvents/EventsHub", fileName = "EventsHub")]
    public class ScriptableEventsHub : ScriptableObject
    {
        [SerializeField] private ScriptableEvent startGameEvent;
        [SerializeField] private ScriptableEvent continueGameEvent;
        [SerializeField] private ScriptableEvent finishGameEvent;
        [SerializeField] private ScriptableEventObject scoreGoalEvent;
        [SerializeField] private ScriptableEventObject playerWonEvent;
        [SerializeField] private ScriptableEventObject boosterHitEvent;

        public ScriptableEvent StartGameEvent => startGameEvent;
        public ScriptableEvent ContinueGameEvent => continueGameEvent;
        public ScriptableEvent FinishGameEvent => finishGameEvent;
        public ScriptableEventObject ScoreGoalEvent => scoreGoalEvent;
        public ScriptableEventObject PlayerWonEvent => playerWonEvent;
        public ScriptableEventObject BoosterHitEvent => boosterHitEvent;
    }

}
