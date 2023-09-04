using System;
using UnityEngine.Events;

namespace Pong
{
    public interface ITimer
    {
        void StartTimer(float time);
        void StopTimer();
        UnityEvent TimeUpUnityEvent { get; }
        event Action TimeUpSystemEvent;
    }
}
