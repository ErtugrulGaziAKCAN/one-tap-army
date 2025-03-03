using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace QuickTools.Scripts.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private AudioClip[] Clips;
        [SerializeField] private float Pitch = 1f;
        [SerializeField] private float Volume = 1f;
        [SerializeField] private bool LoopAudio;
        [SerializeField, EnableIf("@!RandomizePitch")]
        private bool IncreasingPitch;

        [SerializeField, EnableIf("@!IncreasingPitch")]
        private bool RandomizePitch;

        [SerializeField, Indent, ShowIf(nameof(RandomizePitch))]
        private Vector2 PitchMinMax;

        [SerializeField, Indent, ShowIf(nameof(IncreasingPitch))]
        private float MaxPitch;

        [SerializeField, Indent, ShowIf(nameof(IncreasingPitch))]
        private float PitchIncreasePerPlay;

        [SerializeField, Indent, ShowIf(nameof(IncreasingPitch))]
        private float PitchResetCooldown;
        [SerializeField] private UnityEvent OnAudioPlayed;
        [SerializeField] private bool SetPlayingInterval;
        [SerializeField, ShowIf(nameof(SetPlayingInterval))] private float PlayingInterval;

//------Private Variables-------//
        private AudioClip Clip => Clips[Random.Range(0, Clips.Length)];
        private float _pitchTimer;
        private float _originalPitch;
        private bool _canPlay = true;
        private Tween _intervalTween;

#region UNITY_METHODS

        private void Start()
        {
            _originalPitch = Pitch;
        }

        private void Update()
        {
            _pitchTimer += Time.deltaTime;
            if (_pitchTimer >= PitchResetCooldown)
            {
                _pitchTimer = 0;
                Pitch = _originalPitch;
            }
        }

#endregion


#region PUBLIC_METHODS

        [Button]
        public void Play()
        {
            if (!_canPlay)
                return;
            SetInterval();
            OnAudioPlayed?.Invoke();
            if (RandomizePitch)
            {
                Pitch = Random.Range(PitchMinMax.x, PitchMinMax.y);
            }

            if (IncreasingPitch)
            {
                Pitch += PitchIncreasePerPlay;
                if (Pitch > MaxPitch)
                {
                    Pitch = MaxPitch;
                }

                _pitchTimer = 0;
            }

            AudioManager.PlayClip(Clip, Volume, Pitch, LoopAudio);
        }

        public void Play(float pitch)
        {
            if (!_canPlay)
                return;
            OnAudioPlayed?.Invoke();
            SetInterval();
            AudioManager.PlayClip(Clip, Volume, pitch);
        }

        public void Play(float vol, float pitch)
        {
            if (!_canPlay)
                return;
            OnAudioPlayed?.Invoke();
            SetInterval();
            AudioManager.PlayClip(Clip, vol, pitch);
        }

        public void ResetPitch()
        {
            Pitch = _originalPitch;
        }

#endregion


#region PRIVATE_METHODS

        private void SetInterval()
        {
            if (!SetPlayingInterval)
                return;
            _canPlay = false;
            _intervalTween.Kill();
            _intervalTween = DOVirtual.DelayedCall(PlayingInterval, () => _canPlay = true);
        }

#endregion
    }
}