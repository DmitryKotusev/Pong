using UnityEngine;

public class CameraSizeAdjuster : MonoBehaviour
{
    [SerializeField] private float targetWidth = 10f;
    [SerializeField] private float targetHeight = 5f;
    [SerializeField] private Camera targetCamera;

    private void Start()
    {
        UpdateOrthographicSize();
    }

    private void UpdateOrthographicSize()
    {
        float aspectRatio = Screen.width * 1.0f / Screen.height;
        float newOrthographicSize = targetWidth / (2 * aspectRatio);

        if (newOrthographicSize < targetHeight / 2)
        {
            targetCamera.orthographicSize = targetHeight / 2;
        }
        else
        {
            targetCamera.orthographicSize = newOrthographicSize;
        }
    }

    private void Update()
    {
        UpdateOrthographicSize();
    }
}
