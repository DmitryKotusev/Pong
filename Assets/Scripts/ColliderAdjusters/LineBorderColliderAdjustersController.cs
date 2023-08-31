using System.Collections.Generic;
using UnityEngine;

namespace Pong.Utilities
{
    public class LineBorderColliderAdjustersController : MonoBehaviour
    {
        [SerializeField] private List<LineBorderColliderAdjuster> lineBorderColliderAdjusters;

        public void AdjustColliders()
        {
            foreach (var lineBorderColliderAdjuster in lineBorderColliderAdjusters)
            {
                lineBorderColliderAdjuster.AdjustCollider();
            }
        }
    }
}
