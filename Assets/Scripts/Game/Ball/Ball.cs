using Pong.ScriptableEvents;
using System.Linq;
using UnityEngine;

namespace Pong
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private ScriptableEventsHub eventsHub;

        private IPoolHandler<Ball> poolHandler;

        private float speed;
        private Vector3 moveDirection;

        public float Speed => speed;
        public Vector3 MoveDirection => moveDirection;

        public void SetPoolHandler(IPoolHandler<Ball> poolHandler)
        {
            this.poolHandler = poolHandler;
        }

        public void SetRandomMoveDirection()
        {
            moveDirection = Vector3.right;
            int angle = UnityEngine.Random.Range(-45, 45);

            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                angle = UnityEngine.Random.Range(135, 225);
            }

            moveDirection = Quaternion.AngleAxis(angle, Vector3.forward) * moveDirection;
        }

        public void SetStartSpeed()
        {
            speed = gameSettings.BallStartSpeed;
        }

        private void Update()
        {
            Vector3 deltaPosition = moveDirection * speed * Time.deltaTime;

            Vector2 rayOrigin = transform.position;
            Vector2 rayDirection = deltaPosition;
            float rayDistance = deltaPosition.magnitude;

            RaycastHit2D[] hits = Physics2D.RaycastAll(rayOrigin, rayDirection, rayDistance)
                .OrderBy(hit => hit.distance)
                .ToArray();

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.GetComponent<Gate>() != null)
                {
                    poolHandler.ReturnToPool(this);
                    eventsHub.ScoreGoalEvent.RaiseEvent(hit.collider);
                    Debug.Log($"[Ball] Update, hit gate");
                    return;
                }

                if (hit.transform.GetComponent<Border>() != null)
                {
                    Debug.Log($"[Ball] Update, hit Border");

                    Vector2 reflection = rayDirection - 2 * Vector2.Dot(rayDirection, hit.normal) * hit.normal;
                    moveDirection = reflection.normalized;

                    Vector2 reflectionDelta = (Vector2)moveDirection * (rayDistance - hit.distance);
                    transform.position = hit.point + reflectionDelta;

                    return;
                }

                if (hit.transform.GetComponent<Platform>() != null)
                {
                    Platform platform = hit.transform.GetComponent<Platform>();

                    Vector2 primaryReflectionDirection = platform.transform.right;

                    Vector2 deltaHit = hit.point - (Vector2)platform.transform.position;

                    float collisionAngle = Vector2.SignedAngle(primaryReflectionDirection, deltaHit);

                    if (Mathf.Abs(collisionAngle) < 5)
                    {
                        collisionAngle = 0;
                    }

                    collisionAngle = Mathf.Clamp(collisionAngle, -70f, 70f);

                    Vector2 finalReflectionDirection = (Quaternion.AngleAxis(collisionAngle, Vector3.forward) * primaryReflectionDirection).normalized;

                    moveDirection = finalReflectionDirection;
                    speed += gameSettings.BallBounceSpeedIncrease;

                    Vector2 reflectionDelta = (Vector2)moveDirection * (rayDistance - hit.distance);
                    transform.position = hit.point + reflectionDelta;

                    Debug.Log($"[Ball] Update, hit platform");
                    return;
                }

                if (hit.transform.GetComponent<BaseBooster>() != null)
                {
                    BaseBooster booster = hit.transform.GetComponent<BaseBooster>();
                    booster.OnHit();

                    if (booster is CloneBallBooster)
                    {
                        poolHandler.ReturnToPool(this);
                    }

                    Debug.Log($"[Ball] Update, hit booster");
                }
            }

            transform.position += deltaPosition;
        }
    }
}
