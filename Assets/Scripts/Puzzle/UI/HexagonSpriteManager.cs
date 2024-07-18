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
            hexImage.sprite = possibleHexSprites[0];
            if (hexPowered.GetType() == typeof(HexagonDirectional) || hexPowered.GetType() == typeof(HexagonPowered_Active))
            {
                hexPowered.OnPowerChanged += UpdateSprite;
            }
            else if (hexPowered.GetType() == typeof(HexagonTarget))
            {
                HexagonTarget.OnChangedPowerForRequirement += UpdateSprite;
            }
        }

        private void OnDestroy()
        {
            if (hexPowered.GetType() == typeof(HexagonDirectional))
            {
                hexPowered.OnPowerChanged -= UpdateSprite;
            }
            else if (hexPowered.GetType() == typeof(HexagonTarget))
            {
                HexagonTarget.OnChangedPowerForRequirement -= UpdateSprite;
            }
        }

        private void UpdateSprite(HexagonTarget target, int powerValue, int requiredPower)
        {
            //Check if it is the same target
            if (target != hexPowered)
                return;
            // Update the sprite for target hexagon based on power state and required power
            // 0 = No power, 1 = Not enough, 2 = Too much, 3 = Correct
            if (powerValue > 0)
            {
                if (powerValue < requiredPower)
                {
                    hexImage.sprite = possibleHexSprites[1];
                }
                else if (powerValue > requiredPower)
                {
                    hexImage.sprite = possibleHexSprites[2];
                }
                else
                {
                    hexImage.sprite = possibleHexSprites[3];
                }
            }
            else
            {
                hexImage.sprite = possibleHexSprites[0];
            }
        }

        private void UpdateSprite(int powerValue)
        {
            // Update the sprite for single direction hexagon based on power state
            hexImage.sprite = possibleHexSprites[powerValue > 0 ? 1 : 0];
        }
    }
}