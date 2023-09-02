using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pong
{
    public class BallSpawner : MonoBehaviour, IPoolHandler<Ball>
    {
        [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
        [SerializeField] private Transform ballsPoolContainer;

        [Space(10)]
        [SerializeField] private Ball ballPrefab;

        private List<Ball> ballsPool = new List<Ball>();

        private List<Ball> activeBalls = new List<Ball>();
        public List<Ball> ActiveBalls => activeBalls;

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

        public void SpawnBall()
        {
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
            Ball newBall = GetFromPool();

            if (newBall == null)
            {
                newBall = Instantiate(ballPrefab, ballsPoolContainer);
                activeBalls.Add(newBall);
                newBall.SetPoolHandler(this);
            }


            newBall.transform.position = spawnPosition;
            newBall.SetStartSpeed();
            newBall.SetRandomMoveDirection();
        }
    }
}
