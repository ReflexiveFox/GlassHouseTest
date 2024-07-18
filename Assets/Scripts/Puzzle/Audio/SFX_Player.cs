using UnityEngine;
using Glasshouse.Puzzles.Logic;
using Glasshouse.Puzzles.UI;

namespace Glasshouse.Puzzles.Audio
{
    public class SFX_Player : MonoBehaviour
    {
        [SerializeField] AudioClip rotateSFX;
        [SerializeField] AudioClip puzzleCompleteSFX;
        [SerializeField] AudioClip powerOnOffSFX;

        AudioSource sfxAudioSource;

        private void Awake()
        {
            sfxAudioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            HexagonDirectional.OnTogglingPower += PlayPowerOnOff;
            HexagonRotateable.OnRotatingHex += PlayRotateSFX;
            PuzzleManager.OnPuzzleCompleted += PlayCompletedPuzzleSFX;
        }

        private void OnDestroy()
        {
            HexagonDirectional.OnTogglingPower -= PlayPowerOnOff;
            HexagonRotateable.OnRotatingHex -= PlayRotateSFX;
            PuzzleManager.OnPuzzleCompleted -= PlayCompletedPuzzleSFX;
        }

        private void PlayCompletedPuzzleSFX()
        {
            sfxAudioSource.PlayOneShot(puzzleCompleteSFX);
        }

        private void PlayRotateSFX()
        {
            sfxAudioSource.PlayOneShot(rotateSFX);
        }

        private void PlayPowerOnOff()
        {
            sfxAudioSource.PlayOneShot(powerOnOffSFX);
        }
    }
}