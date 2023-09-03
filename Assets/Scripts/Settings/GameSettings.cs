using UnityEngine;

namespace Pong
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "PongCustomScriptables/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [Tooltip("The time the game lasts (in seconds)")]
        [SerializeField] private float gameTime = 120f;

        [Tooltip("Must be of size 4 for every booster type")]
        [SerializeField]
        private float[] boosterTypeSpawnWieghts = new float[]
        {
            1,
            1,
            1,
            1
        };

        [Tooltip("Every boosterSpawnTime period (in seconds) a new booster is spawned")]
        [SerializeField] private float boosterSpawnTime = 20f;

        [Tooltip("Amount of points required to collect to win the game")]
        [SerializeField] private int requiredScoreCount = 2;

        [Space(20)]
        [Header("Balls settings")]
        [SerializeField] private float ballStartSpeed = 2f;
        [SerializeField] private float ballBounceSpeedIncrease = 0.4f;
        [SerializeField] private float ballMaxSpeed = 40f;
        [Tooltip("Time after goal till the next ball is spawned")]
        [SerializeField] private float ballSpawnPauseTime = 2f;

        [Space(20)]
        [Header("Platform settings")]
        [SerializeField] private float playerPlatformMovementSpeed = 5f;
        [SerializeField] private float aiPlatformMovementSpeed = 5f;
        [SerializeField] private float platformSizeChangeStep = 0.5f;
        [SerializeField] private float platformMaxSize = 4f;
        [SerializeField] private float platformMinSize = 0.5f;

        public float GameTime => gameTime;
        public float[] BoosterTypeSpawnWieghts => boosterTypeSpawnWieghts;
        public float BoosterSpawnTime => boosterSpawnTime;
        public int RequiredScoreCount => requiredScoreCount;
        public float BallStartSpeed => ballStartSpeed;
        public float BallBounceSpeedIncrease => ballBounceSpeedIncrease;
        public float BallMaxSpeed => ballMaxSpeed;
        public float BallSpawnPauseTime => ballSpawnPauseTime;
        public float PlayerPlatformMovementSpeed => playerPlatformMovementSpeed;
        public float AiPlatformMovementSpeed => aiPlatformMovementSpeed;
        public float PlatformSizeChangeStep => platformSizeChangeStep;
        public float PlatformMaxSize => platformMaxSize;
        public float PlatformMinSize => platformMinSize;
    }
}
