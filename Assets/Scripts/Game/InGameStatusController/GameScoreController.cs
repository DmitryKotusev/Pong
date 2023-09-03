using Pong.ScriptableEvents;
using UnityEngine;

namespace Pong
{
    public class GameScoreController : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private ScriptableEventsHub eventsHub;

        [Space(10)]
        [SerializeField] private BoxCollider2D firstPlayerGateCollider;
        [SerializeField] private BoxCollider2D secondPlayerGateCollider;

        [Space(10)]
        [SerializeField] private TMPro.TMP_Text firstPlayerScoreText;
        [SerializeField] private TMPro.TMP_Text secondPlayerScoreText;

        private int score1;
        private int score2;


        public int Score1
        {
            get => score1;
            set
            {
                score1 = value;
                firstPlayerScoreText.text = score1.ToString();

                if (score1 >= gameSettings.RequiredScoreCount)
                {
                    eventsHub.PlayerWonEvent.RaiseEvent("Player 1 won!");
                }
            }
        }

        public int Score2
        {
            get => score2;
            set
            {
                score2 = value;
                secondPlayerScoreText.text = score2.ToString();

                if (score2 >= gameSettings.RequiredScoreCount)
                {
                    eventsHub.PlayerWonEvent.RaiseEvent("Player 2 won!");
                }
            }
        }

        public void ResetScore()
        {
            Score1 = 0;
            Score2 = 0;
        }

        private void OnScoreGoal(object gatesColliderObject)
        {
            BoxCollider2D gatesCollider = gatesColliderObject as BoxCollider2D;

            if (gatesCollider == null)
            {
                Debug.LogError("[GameStatusController] OnScoreGoal, cast parameter error: could not cast to BoxCollider2D");
                return;
            }

            if (firstPlayerGateCollider == gatesCollider)
            {
                Score2 += 1;
            }
            else if (secondPlayerGateCollider == gatesCollider)
            {
                Score1 += 1;
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
            eventsHub.ScoreGoalEvent.ScriptableSignal += OnScoreGoal;
        }

        private void UnsubscribeFromScriptableEvents()
        {
            eventsHub.ScoreGoalEvent.ScriptableSignal -= OnScoreGoal;
        }
    }
}
