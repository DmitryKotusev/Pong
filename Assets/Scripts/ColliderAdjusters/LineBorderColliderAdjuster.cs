using UnityEngine;

namespace Pong.Utilities
{
    public class LineBorderColliderAdjuster : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;

        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private int lineRendererPointIndex1;
        [SerializeField] private int lineRendererPointIndex2;

        public void AdjustCollider()
        {
            if (lineRenderer == null)
            {
                return;
            }

            if (boxCollider == null)
            {
                return;
            }

            if (lineRendererPointIndex1 == lineRendererPointIndex2)
            {
                return;
            }

            if (lineRendererPointIndex1 >= lineRenderer.positionCount
                || lineRendererPointIndex2 >= lineRenderer.positionCount)
            {
                return;
            }

            Vector3 point1 = lineRenderer.transform.TransformPoint(lineRenderer.GetPosition(lineRendererPointIndex1));
            Vector3 point2 = lineRenderer.transform.TransformPoint(lineRenderer.GetPosition(lineRendererPointIndex2));

            Vector3 difference = point2 - point1;

            transform.position = Vector3.Lerp(point1, point2, 0.5f);

            transform.rotation = Quaternion.identity;
            transform.rotation *= Quaternion.FromToRotation(transform.right, difference);

            boxCollider.size = new Vector2
            {
                x = difference.magnitude + lineRenderer.startWidth,
                y = lineRenderer.startWidth
            };
        }
    }
}
