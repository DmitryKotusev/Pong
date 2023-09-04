using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Pong
{
    public class Timer : MonoBehaviour, ITimer
    {
        [SerializeField] private TMPro.TMP_Text timeText;
        [SerializeField] private UnityEvent timeUpUnityEvent;

        public UnityEvent TimeUpUnityEvent => timeUpUnityEvent;
        public event Action TimeUpSystemEvent;

        private float LeftTime
        {
            get => leftTime;
            set
            {
                leftTime = value;

                if (Mathf.Abs(leftTime) < Mathf.Epsilon)
                {
                    leftTime = 0;
                }

                timeText.text = leftTime.ToString("F0");
                timeText.gameObject.SetActive(leftTime > 0);

                enabled = leftTime > 0;

                if (leftTime <= 0)
                {
                    TimeUpUnityEvent?.Invoke();
                    TimeUpSystemEvent?.Invoke();
                }
            }
        }

        private float leftTime;

        public void StartTimer(float time)
        {
            if (time > 0)
            {
                LeftTime = time;
                StartCoroutine(Tick());
            }
        }

        public void StopTimer()
        {
            StopAllCoroutines();
        }

        private IEnumerator Tick()
        {
            while (true)
            {
                LeftTime = Mathf.Clamp(LeftTime - Time.deltaTime, 0, float.PositiveInfinity);
                yield return null;
            }
        }

        private void OnEnable()
        {
            if (LeftTime <= 0)
            {
                enabled = false;
            }

            timeText.gameObject.SetActive(leftTime > 0);
        }
    }
}
