using Pong.ScriptableEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class BallSpawner : MonoBehaviour, IPoolHandler<Ball>
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private ScriptableEventsHub eventsHub;

        [Space(10)]
        [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
        [SerializeField] private Transform ballsPoolContainer;

        [Space(10)]
        [SerializeField] private Ball ballPrefab;

        private List<Ball> ballsPool = new List<Ball>();

        private List<Ball> activeBalls = new List<Ball>();

        public List<Ball> ActiveBalls => activeBalls;

        public void SpawnBallWithDelay()
        {
            StartCoroutine(SpawnBallWithDelayCoroutine(gameSettings.BallSpawnPauseTime));
        }

        public void ResetBalls()
        {
            List<Ball> stillActiveBalls = new List<Ball>(ActiveBalls);

            foreach (var ball in stillActiveBalls)
            {
                ReturnToPool(ball);
            }
        }

        public void ReturnToPool(Ball ball)
        {
            ball.gameObject.SetActive(false);
            activeBalls.Remove(ball);
            ballsPool.Add(ball);
        }

        public Ball GetFromPool()
        {
            Ball ball = ballsPool.Count > 0 ? ballsPool[ballsPool.Count - 1] : null;

            if (ball != null)
            {
                ballsPool.RemoveAt(ballsPool.Count - 1);
                activeBalls.Add(ball);
                ball.gameObject.SetActive(true);
            }

            return ball;
        }

        public void SpawnBallAtRandomPosition()
        {
            SpawnBall();
        }

        public void SpawnBallAtSpecificPosition(Vector3 specificSpawnPosition)
        {
            SpawnBall(specificSpawnPosition);
        }

        private void SpawnBall(Vector3? specificSpawnPosition = null)
        {
            Vector3 spawnPosition = specificSpawnPosition.HasValue ? specificSpawnPosition.Value : spawnPoints[Random.Range(0, spawnPoints.Count)].position;
            Ball newBall = GetFromPool();

            if (newBall == null)
            {
                newBall = Instantiate(ballPrefab, ballsPoolContainer);
                newBall.name = $"Ball {ballsPool.Count + activeBalls.Count}";
                activeBalls.Add(newBall);
                newBall.SetPoolHandler(this);
            }

            newBall.transform.position = spawnPosition;
            newBall.SetStartSpeed();
            newBall.SetRandomMoveDirection();
        }

        private IEnumerator SpawnBallWithDelayCoroutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            SpawnBallAtRandomPosition();
        }

        private void OnScoreGoal(object gatesColliderObject)
        {
            if (ActiveBalls.Count == 0)
            {
                SpawnBallWithDelay();
            }
        }

        private void OnBoosterHit(object boosterObject)
        {
            BaseBooster booster = boosterObject as BaseBooster;

            if (booster == null)
            {
                Debug.LogError("[BallSpawner] OnBoosterHit, cast parameter error: could not cast to BaseBooster");
                return;
            }

            if (booster is CloneBallBooster)
            {
                SpawnBallAtSpecificPosition(booster.transform.position);
                SpawnBallAtSpecificPosition(booster.transform.position);
            }
        }

        private void OnEnable()
        {
            SubscribeToScriptableEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromScriptableEvents();
        }

        private void SubscribeToScriptableEvents()
        {
            eventsHub.ScoreGoalEvent.ScriptableSignal += OnScoreGoal;
            eventsHub.BoosterHitEvent.ScriptableSignal += OnBoosterHit;
        }

        private void UnsubscribeFromScriptableEvents()
        {
            eventsHub.ScoreGoalEvent.ScriptableSignal -= OnScoreGoal;
            eventsHub.BoosterHitEvent.ScriptableSignal -= OnBoosterHit;
        }
    }
}
