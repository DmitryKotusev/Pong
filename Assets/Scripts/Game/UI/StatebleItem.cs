using UnityEngine;
using UnityEngine.Events;

namespace Pong.Utilities
{
    [ExecuteInEditMode]
    public class StatebleItem : MonoBehaviour
    {
        public int ActiveState = 0;
        public bool SetActiveStateOnAwake = true;
        public BoolFields RandomOn = new BoolFields();

        [SerializeField] private StateData[] availableStates = new StateData[0];

        private string previousState;

        public void SetStateByIndex(int stateIndex)
        {
            if (availableStates.Length == 0)
            {
                return;
            }

            ActiveState = ActiveState >= availableStates.Length ? 0 :
                ActiveState < 0 ? 0 : ActiveState;
            SetState(availableStates[ActiveState].Name);
        }

        public void SetState(string state)
        {
            int activeState = -1;
            for (int i = 0; i < availableStates.Length; i++)
            {
                if (state == availableStates[i].Name)
                {
                    activeState = i;
                    continue;
                }

                availableStates[i].SetState(false);
            }

            if (activeState != -1)
            {
                if (ActiveState > -1 && ActiveState < availableStates.Length - 1)
                    previousState = availableStates[ActiveState].Name;
                availableStates[activeState].SetState(true);
            }


            ActiveState = activeState;
        }

        public void Reset()
        {
            foreach (StateData state in availableStates)
            {
                state.Reset();
            }
        }

        private void Awake()
        {
            if (SetActiveStateOnAwake)
                SetStateByIndex(ActiveState);
            if (RandomOn.Awake)
                SetRandom();
        }

        private void OnEnable()
        {
            if (RandomOn.Enable)
                SetRandom();
        }

        [ContextMenu("SetRandom")]
        private void SetRandom()
        {
            int random = Random.Range(0, availableStates.Length);
            if (random == ActiveState)
            {
                random++;
                random = Mathf.Clamp(random, 0, availableStates.Length - 1);
            }

            ActiveState = random;
            SetStateByIndex(ActiveState);
        }

        [System.Serializable]
        public class StateData
        {
            public string Name;
            public UnityEvent OnActivate;
            public UnityEvent OnDeactivate;

            private bool isActive = true;
            private bool isInited = false;

            public void SetState(bool state, bool invokeEvents = true)
            {
                if (isActive == state && isInited)
                {
                    return;
                }
                else
                {
                    isActive = state;
                }

                isInited = true;
                if (!invokeEvents)
                {
                    return;
                }

                if (isActive)
                {
                    OnActivate.Invoke();
                }
                else
                {
                    OnDeactivate.Invoke();
                }
            }

            public void Reset()
            {
                isInited = false;
            }
        }

        [System.Serializable]
        public class BoolFields
        {
            public bool Awake = false;
            public bool Enable = false;
        }
    }
}