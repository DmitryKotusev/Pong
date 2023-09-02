using Pong.GameStates;

namespace Pong
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : BaseState;
    }
}