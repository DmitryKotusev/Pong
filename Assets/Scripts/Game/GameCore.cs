using Pong.GameStates;
using Pong.ScriptableEvents;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pong
{
    public class GameCore : MonoBehaviour, IStateSwitcher
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private ScriptableEventsHub eventsHub;
        [Space(10)]
        [SerializeField] private Utilities.StatebleItem visualStateController;

        private BaseState currentState;

        private List<BaseState> statesCollection;

        public void SwitchState<T>() where T : BaseState
        {
            BaseState state = statesCollection.FirstOrDefault(s => s is T);

            currentState?.OnExitState();
            state.OnEnterState();

            currentState = state;
        }

        private void Awake()
        {
            statesCollection = new List<BaseState>()
            {
                new PlayingState(this, gameSettings, visualStateController),
                new PlayMenuState(this, gameSettings, visualStateController),
                new ResultMenuState(this, gameSettings, visualStateController),
            };

            SubscribeToScriptableEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromScriptableEvents();
        }

        private void Start()
        {
            SwitchState<PlayMenuState>();
        }

        private void OnStartGame()
        {
            currentState?.OnStartGame();
        }

        private void OnContinueGame()
        {
            currentState?.OnContinueGame();
        }

        private void SubscribeToScriptableEvents()
        {
            eventsHub.StartGameEvent.ScriptableSignal += OnStartGame;
            eventsHub.ContinueGameEvent.ScriptableSignal += OnContinueGame;
        }

        private void UnsubscribeFromScriptableEvents()
        {
            eventsHub.StartGameEvent.ScriptableSignal -= OnStartGame;
            eventsHub.ContinueGameEvent.ScriptableSignal -= OnContinueGame;
        }
    }
}
