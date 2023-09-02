using Pong.Utilities;

namespace Pong.GameStates
{
    public class PlayingState : BaseState
    {
        public PlayingState(IStateSwitcher stateSwitcher, GameSettings gameSettings, StatebleItem visualStateController)
            : base(stateSwitcher, gameSettings, visualStateController)
        {
        }
    }
}
