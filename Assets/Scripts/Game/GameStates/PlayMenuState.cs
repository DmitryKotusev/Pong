using Pong.Utilities;

namespace Pong.GameStates
{
    public class PlayMenuState : BaseState
    {
        public PlayMenuState(IStateSwitcher stateSwitcher, GameSettings gameSettings, StatebleItem visualStateController)
            : base(stateSwitcher, gameSettings, visualStateController)
        {
        }

        public override void OnStartGame()
        {
            base.OnStartGame();

            stateSwitcher.SwitchState<PlayingState>();
        }
    }
}
