using System;
using UnityEngine;

namespace Pong.ScriptableEvents
{
    [CreateAssetMenu(menuName = "PongCustomScriptables/ScriptableEvents/Event", fileName = "Event")]
    public class ScriptableEvent : ScriptableObject
    {
        public event Action ScriptableSignal;

        public void RaiseEvent()
        {
            ScriptableSignal?.Invoke();
        }
    }
}
