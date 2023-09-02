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

        public float GameTime => gameTime;
        public float[] BoosterTypeSpawnWieghts => (float[])boosterTypeSpawnWieghts.Clone();
        public float BoosterSpawnTime => boosterSpawnTime;
        public int RequiredScoreCount => requiredScoreCount;
    }
}
