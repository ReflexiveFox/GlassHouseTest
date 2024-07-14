using UnityEngine;

namespace Glasshouse
{
    public class HexagonInteraction : MonoBehaviour
    {
        private Hexagon hexagon;

        void Start()
        {
            hexagon = GetComponent<Hexagon>();
        }

        void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0)) // Left click
            {
                hexagon.Rotate(false);
            }
            else if (Input.GetMouseButtonDown(1)) // Right click
            {
                if (hexagon is SingleDirectionHexagon singleHexagon)
                {
                    singleHexagon.TogglePower();
                }
            }
        }
    }


}