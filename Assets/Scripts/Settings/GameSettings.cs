using UnityEngine;

namespace Pong
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "PongCustomScriptables/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [Tooltip("The time the game lasts (in seconds)")]
        [SerializeField] private float gameTime = 120f;

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

        [Space(20)]
        [Header("Platform settings")]
        [SerializeField] private float playerPlatformMovementSpeed = 5f;
        [SerializeField] private float aiPlatformMovementSpeed = 5f;

        public float GameTime => gameTime;
        public float[] BoosterTypeSpawnWieghts => boosterTypeSpawnWieghts;
        public float BoosterSpawnTime => boosterSpawnTime;
        public int RequiredScoreCount => requiredScoreCount;
        public float BallStartSpeed => ballStartSpeed;
        public float BallBounceSpeedIncrease => ballBounceSpeedIncrease;
        public float BallMaxSpeed => ballMaxSpeed;
        public float PlayerPlatformMovementSpeed => playerPlatformMovementSpeed;
        public float AiPlatformMovementSpeed => aiPlatformMovementSpeed;
    }
}
