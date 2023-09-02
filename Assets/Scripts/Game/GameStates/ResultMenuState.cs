using Pong.Utilities;

namespace Pong.GameStates
{
    public class ResultMenuState : BaseState
    {
        public ResultMenuState(IStateSwitcher stateSwitcher, GameSettings gameSettings, StatebleItem visualStateController)
            : base(stateSwitcher, gameSettings, visualStateController)
        {
        }

        public override void OnContinueGame()
        {
            base.OnContinueGame();

            stateSwitcher.SwitchState<PlayMenuState>();
        }
    }
}
