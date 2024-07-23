using UnityEngine;
using TMPro;
using Glasshouse.Puzzles.Logic;

namespace Glasshouse.Puzzles.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class PowerTextDisplay : MonoBehaviour
    {
        private HexagonPowered poweredHex;
        private TextMeshProUGUI powerText;

        private void Awake()
        {
            powerText = GetComponent<TextMeshProUGUI>();
            poweredHex = GetComponentInParent<HexagonPowered>();
        }

        private void OnEnable()
        {
            poweredHex.OnPowerChanged += UpdatePowerText;
        }

        private void OnDisable()
        {
            poweredHex.OnPowerChanged -= UpdatePowerText;
        }

        private void UpdatePowerText(int newPowerValue)
        {
            if (poweredHex.GetType() == typeof(HexagonTarget))
            {
                //Disable text when correct power level is reached
                HexagonTarget target = (HexagonTarget)poweredHex;
                powerText.enabled = target.RequiredPower != newPowerValue;
                newPowerValue = target.RequiredPower;
            }
            else if (poweredHex.GetType() == typeof(HexagonPowered_Active))
            {
                //Enable text when Hexagon power is > 0
                powerText.enabled = poweredHex.PowerValue > 0;               
            }
            powerText.text = newPowerValue.ToString();
        }
    }
}