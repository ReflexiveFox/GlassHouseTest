using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Glasshouse.Puzzles.Logic
{
    /// <summary>
    /// This class controls the status of the puzzle by using a list of all targets to powerup and win the game
    /// </summary>
    public class PuzzleManager : MonoBehaviour
    {
        /// <summary>
        /// Used for SFX player
        /// </summary>
        public static event Action OnPuzzleCompleted = delegate { };

        [Tooltip("This list will track each target and its \"powerReached\" status")]
        [SerializeField] private List<HexagonTarget> targetsActiveStatuses;
        [Tooltip("Check this for populating automatically the list of objectives at startup")]
        [SerializeField] private bool populateTargetsList;

        private List<bool> targetStatuses;

        // Automatically populate the list if activated
        private void Awake()
        {
            if (populateTargetsList)
            {
                targetsActiveStatuses.Clear();
                targetsActiveStatuses = GetComponentsInChildren<HexagonTarget>().ToList();
            }
        }

        // Subscribe to any power value change among the targets in the list to verify if puzzle is solved
        private void Start()
        {
            targetStatuses = new List<bool>();
            foreach(var target in targetsActiveStatuses)
            {
                bool tempFalseStatus = false;
                targetStatuses.Add(tempFalseStatus);
            }
            HexagonTarget.OnChangedPowerForRequirement += UpdateTargetOnList;
        }

        // Unsubscribe to any power value change among the targets in the list to verify if puzzle is solved
        private void OnDestroy()
        {
            HexagonTarget.OnChangedPowerForRequirement -= UpdateTargetOnList;
        }

        // When a target hexagon changes power level, updates its status in the other list of bools
        private void UpdateTargetOnList(HexagonTarget fullChargedHexTarget, int currentPower, int requiredPower)
        {
            if (targetsActiveStatuses.Contains(fullChargedHexTarget))
            {
                int hexTargetIndex = targetsActiveStatuses.IndexOf(fullChargedHexTarget);
                targetStatuses[hexTargetIndex] = currentPower == requiredPower;
                //To avoid spam clicking
                //Invoke(nameof(CheckList),.25f);
                CheckList();
            }
        }
        
        // Check if all hexagon target have the required power to solve the puzzle
        private void CheckList()
        {
            int checkCount = 0;
            foreach(bool status in targetStatuses)
            {
                if (status)
                {
                    checkCount++;
                }
            }
            if(checkCount == targetStatuses.Count)
            {
                // GAME WON
                OnPuzzleCompleted.Invoke();
            }
        }
    }
}