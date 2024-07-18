using System.Collections;
using UnityEngine;
using Glasshouse.Puzzles.Logic;

namespace Glasshouse.UI
{
    /// <summary>
    /// Shakes the gameObject it is attached to
    /// </summary>
    public class UI_Shake : MonoBehaviour
    {
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private float magnitude = 0.1f;

        private RectTransform rectTransform;
        private Vector3 originalPosition;


        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            PuzzleManager.OnPuzzleCompleted += Shake;
            originalPosition = rectTransform.anchoredPosition;
        }

        private void OnDestroy()
        {
            PuzzleManager.OnPuzzleCompleted -= Shake;
        }

        private void Shake()
        {
            StartCoroutine(ShakeCoroutine());
        }

        private IEnumerator ShakeCoroutine()
        {
            float elapsed = 0.0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                rectTransform.anchoredPosition = originalPosition + new Vector3(x, y, originalPosition.z);

                elapsed += Time.deltaTime;

                yield return null;
            }

            rectTransform.anchoredPosition = originalPosition;
        }
    }
}