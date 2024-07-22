using UnityEngine;
using UnityEngine.UI;
using Glasshouse.Puzzles.Logic;

namespace Glasshouse.Puzzles.UI
{
    [RequireComponent(typeof(Image))]
    public class HexagonSpriteManager : MonoBehaviour
    { 
        [Tooltip("Which hexagonal sprites will this tile possibly have?\nFirst is default.")]
        [SerializeField] private Sprite[] possibleHexSprites;

        private Image hexImage;
        private HexagonPowered hexPowered;

        private void Awake()
        {
            hexImage = GetComponent<Image>();
            hexPowered = GetComponent<HexagonPowered>();
        }

        private void Start()
        {
            hexPowered.OnPowerChanged += UpdateGenericHexagonSprite;
            UpdateGenericHexagonSprite(hexPowered.PowerValue);
        }

        private void OnDestroy()
        {
            hexPowered.OnPowerChanged -= UpdateGenericHexagonSprite;
        }

        private void UpdateGenericHexagonSprite(int powerValue)
        {
            // Update the hexagon's sprite based on its type, power state and required power (if present on hexagon)
            if (powerValue > 0)
            {
                //These types of hexagons have 2 statuses: ON / OFF
                if (hexPowered.GetType() == typeof(HexagonDirectional) || hexPowered.GetType() == typeof(HexagonPowered_Active))
                {
                    hexImage.sprite = possibleHexSprites[1];    // Turned ON
                }
                //This hexagon has more than 2 statuses: OFF / NotEnough / ON / TooMuch
                else if(hexPowered.GetType() == typeof(HexagonTarget))
                {
                    HexagonTarget target = (HexagonTarget)hexPowered;
                    if (powerValue < target.RequiredPower)
                    {
                        hexImage.sprite = possibleHexSprites[1];    // Not enough power
                    }
                    else if (powerValue > target.RequiredPower)     
                    {
                        hexImage.sprite = possibleHexSprites[2];    // Too much power
                    }
                    else
                    {
                        hexImage.sprite = possibleHexSprites[3];    // Turned ON
                    }
                }                    
            }
            else
            {
                hexImage.sprite = possibleHexSprites[0];    //Set generic hexagon to OFF status
            }      
        }
    }
}