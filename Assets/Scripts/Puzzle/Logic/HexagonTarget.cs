using System;
using System.Collections.Generic;
using UnityEngine;

namespace Glasshouse.Puzzles.Logic
{
    [ExecuteAlways]
    public class HexagonTarget : HexagonPowered
    {
        public static event Action<HexagonTarget, int, int> OnChangedPowerForRequirement = delegate { };

        [SerializeField] private int _requiredPower;
        [SerializeField] protected List<HexagonPowered_Active> powerGenerators;
        
        [Header("OverlapCircle Stats")]
        [SerializeField] private float castRadius = 125f;
        [SerializeField] private LayerMask layerMask;

        public int RequiredPower => _requiredPower;

        protected override void Start()
        {
            base.Start();

            //Prevent hexagons to listen unwanted events at startup
            Invoke(nameof(PopulateList), .5f);

            //Update generator power to see if there are active hexagons nearby
            SetPower(0);
        }

        /// <summary>
        /// Draw where the target hexagon will check for generators near itself
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, castRadius);
        }

        private void OnDestroy()
        {
            SetSubscriptionForGenerators(false);
            powerGenerators.Clear();
        }

        private void PopulateList()
        {
            powerGenerators.Clear();
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, castRadius, layerMask);
            if (colliders.Length > 0)
            {
                foreach (Collider2D collider in colliders)
                {
                    HexagonPowered_Active temp = collider.GetComponentInParent<HexagonPowered_Active>();
                    powerGenerators.Add(temp);
                    temp.OnPowerChanged += SetPower;
                }
            }
        }

        private void SetSubscriptionForGenerators(bool isSubscribing)
        {
            foreach (HexagonPowered_Active powerGenerator in powerGenerators)
            {
                if (isSubscribing)
                    powerGenerator.OnPowerChanged += SetPower;
                else
                    powerGenerator.OnPowerChanged -= SetPower;
            }
        }

        protected override void SetPower(int value)
        {
            int currentPower = 0;
            //Count the total power of neghbours from 0
            foreach(HexagonPowered_Active generator in powerGenerators)
            {
                currentPower += generator.PowerValue;
            }
            base.SetPower(currentPower);
            OnChangedPowerForRequirement.Invoke(this, PowerValue, _requiredPower);         
        }
    }
}