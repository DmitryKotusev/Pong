using UnityEngine;

public class OutsideEditorObjectDisabler : MonoBehaviour
{
    void Start()
    {
#if !UNITY_EDITOR
        gameObject.SetActive(false);
#endif
    }
}
