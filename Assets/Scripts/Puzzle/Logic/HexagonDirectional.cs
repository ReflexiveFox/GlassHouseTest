using System;
using UnityEngine;
using Glasshouse.Puzzles.Input;
using Glasshouse.Puzzles.Logic;
using UnityEngine.UI;

namespace Glasshouse.Puzzles.UI
{
    public class HexagonDirectional : HexagonPowered
    {
        /// <summary>
        /// Used for SFX player
        /// </summary>
        public static event Action OnTogglingPower = delegate { };

        private Collider2D triggerCollider;
        private GraphicRaycaster graphicRaycaster;

        private void Awake()
        {
            triggerCollider = GetComponentInChildren<Collider2D>();
            graphicRaycaster = GetComponentInParent<GraphicRaycaster>();

            PuzzlePlayerInput.OnPlayerInputPower += PowerupIfClickedOnSprite;
        }

        private void OnDestroy()
        {
            PuzzlePlayerInput.OnPlayerInputPower -= PowerupIfClickedOnSprite;
        }

        private void PowerupIfClickedOnSprite()
        {
            if (PuzzlePlayerInput.RaycastMouseClick(graphicRaycaster, gameObject))
            {
                // Change Sprite based on power status
                TogglePower();
            }
        }

        private void TogglePower()
        {
            if (isPowered)
                PowerDown();
            else
                PowerUp();

            OnTogglingPower.Invoke();
            triggerCollider.enabled = isPowered;
        }
    }
}