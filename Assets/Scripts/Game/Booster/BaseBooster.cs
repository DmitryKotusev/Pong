using Pong.ScriptableEvents;
using UnityEngine;

namespace Pong
{
    public abstract class BaseBooster : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private ScriptableEventsHub eventsHub;

        private IPoolHandler<BaseBooster> poolHandler;

        public void SetPoolHandler(IPoolHandler<BaseBooster> poolHandler)
        {
            this.poolHandler = poolHandler;
        }

        public void OnHit()
        {
            poolHandler.ReturnToPool(this);

            eventsHub.BoosterHitEvent.RaiseEvent(this);
        }
    }
}
