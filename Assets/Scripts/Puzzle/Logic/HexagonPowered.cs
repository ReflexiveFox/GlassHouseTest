using System;
using UnityEngine;

namespace Glasshouse.Puzzles.Logic
{
    public abstract class HexagonPowered : MonoBehaviour
    {
        public event Action<int> OnPowerChanged = delegate { };

        [SerializeField] private int _powerValue;

        protected bool isPowered => PowerValue > 0;

        public int PowerValue
        {
            private set 
            {
                
                int previousValue = _powerValue;
                _powerValue = Mathf.Clamp(value, 0, int.MaxValue);
            }
            get
            {
                return _powerValue;
            }
        }

        protected virtual void Start()
        {
            PowerValue = 0;
        }

        //Increase power by value amount
        protected virtual void SetPower(int value)
        {
            _powerValue = value;
            
            //Update power text display
            OnPowerChanged(PowerValue); 
        }

        protected virtual void PowerUp()
        {
            PowerValue += 1;

            //Update power text display
            OnPowerChanged(PowerValue);
        }

        //Reduce power by 1
        protected virtual void PowerDown()
        {
            PowerValue -= 1;

            //Update power text display
            OnPowerChanged(PowerValue); 
        }
    }
}