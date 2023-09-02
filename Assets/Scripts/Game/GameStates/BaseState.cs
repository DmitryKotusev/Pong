namespace Pong.GameStates
{
    public abstract class BaseState
    {
        protected IStateSwitcher stateSwitcher;
        protected GameSettings gameSettings;
        protected Utilities.StatebleItem visualStateController;

        public BaseState(IStateSwitcher stateSwitcher, GameSettings gameSettings, Utilities.StatebleItem visualStateController)
        {
            this.stateSwitcher = stateSwitcher;
            this.gameSettings = gameSettings;
            this.visualStateController = visualStateController;
        }

        public string StateName => GetType().Name;

        public virtual void OnEnterState()
        {
            visualStateController.SetState(StateName);
        }

        public virtual void OnExitState()
        {
        }

        public virtual void OnStartGame()
        {
        }

        public virtual void OnContinueGame()
        {
        }
    }
}
