using UnityEditor;
using Pong.Utilities;
using UnityEngine;

namespace Pong.Editor
{
    [CustomEditor(typeof(LineBorderColliderAdjustersController))]
    public class LineBorderColliderAdjustersControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LineBorderColliderAdjustersController adjustersController = (LineBorderColliderAdjustersController)target;

            GUILayout.Space(10);

            if (GUILayout.Button("Readjust colliders"))
            {
                adjustersController.AdjustColliders();
            }
        }
    }
}
