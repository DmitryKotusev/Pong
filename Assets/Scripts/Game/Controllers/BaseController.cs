using UnityEngine;

namespace Pong
{
    public abstract class BaseController : MonoBehaviour
    {
        [SerializeField] protected GameSettings gameSettings;
        [SerializeField] protected Platform platform;

        [SerializeField] protected BoxCollider2D upperBorderCollider;
        [SerializeField] protected BoxCollider2D lowerBorderCollider;

        protected void MovePlatform(float deltaY)
        {
            float lowerYBorder = lowerBorderCollider.size.y / 2 + lowerBorderCollider.transform.position.y + platform.transform.localScale.y / 2;
            float upperYBorder = -upperBorderCollider.size.y / 2 + upperBorderCollider.transform.position.y - platform.transform.localScale.y / 2;

            float newY = Mathf.Clamp(platform.transform.position.y + deltaY, lowerYBorder, upperYBorder);

            platform.transform.position = new Vector3(
                platform.transform.position.x,
                newY,
                platform.transform.position.z
                );
        }
    }
}
