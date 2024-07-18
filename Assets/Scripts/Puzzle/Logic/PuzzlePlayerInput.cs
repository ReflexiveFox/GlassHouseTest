using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Glasshouse.Puzzles.Logic;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Glasshouse.Puzzles.Input
{
    public class PuzzlePlayerInput : MonoBehaviour
    {
        public static event Action OnPlayerInputRotate = delegate { };
        public static event Action OnPlayerInputPower = delegate { };

        [SerializeField] private PlayerInputActions playerInputActions;
        private InputAction rotateAction;
        private InputAction powerAction;

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            rotateAction = playerInputActions.Puzzle_AM.Rotate;
            powerAction = playerInputActions.Puzzle_AM.Powerup;
        }

        private void OnEnable()
        {
            EnableInput();
            PuzzleManager.OnPuzzleCompleted += DisableInput;
        }

        private void OnDisable()
        {
            DisableInput();
            PuzzleManager.OnPuzzleCompleted -= DisableInput;
        }


        private void EnableInput()
        {
            rotateAction.Enable();
            rotateAction.performed += InvokeOnRotateEvent;

            powerAction.Enable();
            powerAction.performed += InvokeOnPowerEvent;
        }

        private void DisableInput()
        {
            rotateAction.Disable();
            rotateAction.performed -= InvokeOnRotateEvent;

            powerAction.Disable();
            powerAction.performed -= InvokeOnPowerEvent;
        }

        private void InvokeOnPowerEvent(InputAction.CallbackContext obj)
        {
            OnPlayerInputPower.Invoke();
        }

        private void InvokeOnRotateEvent(InputAction.CallbackContext obj)
        {
            OnPlayerInputRotate.Invoke();
        }

        public static bool RaycastMouseClick(GraphicRaycaster raycaster, GameObject gameObjToCheck)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);
            foreach (RaycastResult result in results)
            {
                if (result.gameObject == gameObjToCheck)
                {
                    return true;
                }
            }
            return false;
        }
    }
}