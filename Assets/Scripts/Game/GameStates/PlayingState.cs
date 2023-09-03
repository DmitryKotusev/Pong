using Pong.Utilities;

namespace Pong.GameStates
{
    public class PlayingState : BaseState
    {
        private TMPro.TMP_Text finalWinMessageText;

        public PlayingState(TMPro.TMP_Text finalWinMessageText, IStateSwitcher stateSwitcher, GameSettings gameSettings, StatebleItem visualStateController)
            : base(stateSwitcher, gameSettings, visualStateController)
        {
            this.finalWinMessageText = finalWinMessageText;
        }

        public override void OnPlayerWon(object messageObject)
        {
            base.OnPlayerWon(messageObject);

            string message = messageObject as string;

            if (message == null)
            {
                UnityEngine.Debug.LogError("[PlayingState] OnPlayerWon, cast parameter error: could not cast to string");
                return;
            }

            string displayMessage = $"{message} Press continue to try again!";
            finalWinMessageText.text = displayMessage;

            stateSwitcher.SwitchState<ResultMenuState>();
        }
    }
}
