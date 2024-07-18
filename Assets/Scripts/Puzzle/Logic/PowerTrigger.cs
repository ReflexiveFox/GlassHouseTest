using UnityEngine;
using Glasshouse.Puzzles.UI;

namespace Glasshouse.Puzzles.Logic
{
    /// <summary>
    /// Used to set the collider when the related hexagon is powered up or down
    /// </summary>
    public class PowerTrigger : MonoBehaviour
    {
        private HexagonDirectional hexagonDirectional;
        private Collider2D powerTrigger;

        private void Awake()
        {
            powerTrigger = GetComponent<Collider2D>();
            hexagonDirectional = GetComponentInParent<HexagonDirectional>();
        }

        private void OnEnable()
        {
            hexagonDirectional.OnPowerChanged += SetTriggerStatus;
        }

        private void OnDisable()
        {
            hexagonDirectional.OnPowerChanged -= SetTriggerStatus;
        }

        private void SetTriggerStatus(int newPowerValue)
        {
            powerTrigger.enabled = newPowerValue > 0;
        }
    }
}