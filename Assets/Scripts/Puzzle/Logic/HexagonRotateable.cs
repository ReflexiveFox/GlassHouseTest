using System;
using UnityEngine;
using UnityEngine.UI;
using Glasshouse.Puzzles.Input;

namespace Glasshouse.Puzzles.UI
{
    /// <summary>
    /// Component to handle exclusively the rotation of an hexagon
    /// </summary>
    public class HexagonRotateable : MonoBehaviour
    {
        /// <summary>
        /// Used for audio SFX player
        /// </summary>
        public static event Action OnRotatingHex = delegate { };

        [SerializeField] private bool RotateClockwise = false;
        private GraphicRaycaster graphicRaycaster;

        private void Awake()
        {
            graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
            PuzzlePlayerInput.OnPlayerInputRotate += RotateIfClickedOnSprite;
        }

        private void OnDestroy()
        {
            PuzzlePlayerInput.OnPlayerInputRotate -= RotateIfClickedOnSprite;
        }

        private void RotateIfClickedOnSprite()
        {
            if(PuzzlePlayerInput.RaycastMouseClick(graphicRaycaster, gameObject))
            {
                // Rotate the UI element on click
                transform.Rotate(0, 0, RotateClockwise ? -60 : 60);
                OnRotatingHex.Invoke();
            }
        }
    }
}