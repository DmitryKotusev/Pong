using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pong
{
    public class AIController : BaseController
    {
        [SerializeField] private BallSpawner ballSpawner;

        private void Update()
        {
            List<Ball> activeBalls = ballSpawner.ActiveBalls;

            if (activeBalls.Count == 0)
            {
                return;
            }

            List<KeyValuePair<Ball, float>> ballsWithArrivalTime = activeBalls.ConvertAll(ball =>
            {
                float ballXSpeed = ball.Speed * ball.MoveDirection.x;
                float speedPlatformFrontProjection = Vector2.Dot(platform.transform.right, new Vector2(ballXSpeed, 0));

                Vector2 deltaPosition = ball.transform.position - platform.transform.position;
                float positionPlatformFrontProjection = Vector2.Dot(platform.transform.right, deltaPosition);

                float approximateTimeTillArrival = float.PositiveInfinity;

                if (speedPlatformFrontProjection < 0 && positionPlatformFrontProjection > 0 && Mathf.Abs(ballXSpeed) > Mathf.Epsilon)
                {
                    approximateTimeTillArrival = Mathf.Abs(deltaPosition.x) / Mathf.Abs(ballXSpeed);
                }

                return new KeyValuePair<Ball, float>(ball, approximateTimeTillArrival);
            });

            List<KeyValuePair<Ball, float>> orderedBallsWithArrivalTime = ballsWithArrivalTime.OrderBy(ballData => ballData.Value).ToList();

            float targetPositionY = orderedBallsWithArrivalTime[0].Key.transform.position.y;

            // Foreseeing the future position
            if (orderedBallsWithArrivalTime[0].Value < 10f)
            {
                targetPositionY += orderedBallsWithArrivalTime[0].Key.MoveDirection.y * orderedBallsWithArrivalTime[0].Value * orderedBallsWithArrivalTime[0].Key.Speed;
            }

            float desiredDeltaY = targetPositionY - platform.transform.position.y;
            float deltaYSign = Mathf.Sign(desiredDeltaY);
            float deltaY = deltaYSign * gameSettings.AiPlatformMovementSpeed * Time.deltaTime;

            deltaY = Mathf.Abs(deltaY) > Mathf.Abs(desiredDeltaY) ? desiredDeltaY : deltaY;

            MovePlatform(deltaY);
        }
    }
}
