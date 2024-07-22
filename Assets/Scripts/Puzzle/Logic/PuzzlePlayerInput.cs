using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Glasshouse.Puzzles.Logic;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

namespace Glasshouse.Puzzles.Input
{
    public class PuzzlePlayerInput : MonoBehaviour
    {
        public static event Action OnPlayerInputRotate = delegate { };
        public static event Action OnPlayerInputPower = delegate { };

        [SerializeField] private PlayerInputActions playerInputActions;
        private InputAction rotateAction;
        private InputAction powerAction;

        [Tooltip("How many seconds should pass between one input to the puzzle and another (to avoid spamming inputs)")]
        [SerializeField, Range(0f, 1f)] private float inputCooldown = .1f;

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

        private void OnDestroy()
        {
            playerInputActions.Dispose();
        }

        private void EnableInput()
        {
            rotateAction.Enable();
            rotateAction.performed += InvokeOnRotateEvent;
            rotateAction.performed += PauseListeningInput;

            powerAction.Enable();
            powerAction.performed += InvokeOnPowerEvent;
            powerAction.performed += PauseListeningInput;
        }

        private void DisableInput()
        {
            rotateAction.Disable();
            rotateAction.performed -= InvokeOnRotateEvent;
            rotateAction.performed -= PauseListeningInput;


            powerAction.Disable();
            powerAction.performed -= InvokeOnPowerEvent;
            powerAction.performed -= PauseListeningInput;
        }

        private void InvokeOnPowerEvent(InputAction.CallbackContext _)
        {

            OnPlayerInputPower.Invoke();
        }

        private void InvokeOnRotateEvent(InputAction.CallbackContext _)
        {
            OnPlayerInputRotate.Invoke();
        }

        private void PauseListeningInput(InputAction.CallbackContext callbackContext)
        {
            callbackContext.action.Disable();
            StartCoroutine(ReactivateInputAfter(callbackContext.action));
        }

        private IEnumerator ReactivateInputAfter(InputAction inputAction)
        {
            yield return new WaitForSeconds(inputCooldown);
            inputAction.Enable();
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