using System;
using UnityEngine;

namespace Pong.ScriptableEvents
{
    [CreateAssetMenu(menuName = "PongCustomScriptables/ScriptableEvents/EventWithObjectParameter", fileName = "Event")]
    public class ScriptableEventObject : ScriptableObject
    {
        public event Action<object> ScriptableSignal;

        public void RaiseEvent(object param)
        {
            ScriptableSignal?.Invoke(param);
        }
    }
}
